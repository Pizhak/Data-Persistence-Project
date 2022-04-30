using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI maxScoreText;

    public string playerName;
    public int score;
    [SerializeField] private TMP_InputField inputPlayerName;

    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        maxScoreText = TextMeshProUGUI.FindObjectOfType<TextMeshProUGUI>();
        LoadData();
    }

    [System.Serializable]
    class SaveScore
    {
        public string PlayerName;
        public int Score;
    }

    public void SaveData()
    {
        SaveScore data = new SaveScore();
        //data.TeamColor = teamColor;
        
        data.PlayerName = playerName;
        data.Score = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);

        ChangeMaxScore(playerName);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveScore data = JsonUtility.FromJson<SaveScore>(json);

            score = data.Score;
            ChangeMaxScore(data.PlayerName);
        }
    }

    public void ChangeMaxScore(string name)
    {
        maxScoreText.text = "Best Score: " + name + " : " + score;
    }

    public void StartGame()
    {
        playerName = inputPlayerName.text;

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
