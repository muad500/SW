using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Audio;
public class GameManager : MonoBehaviour
{
    
    
    [SerializeField]private Button restartButton;
    public Text scoreText;
    public GameObject gamePlayScreen;
    public int score;
    public Text endScore;
    public Text BestPlayerScoreTxt;
    private static int BestScore;

    public float rotationSpeed = 0.01f;
    public Material cubeMapMaterial;

    public bool isGameActive = false;
    public GameObject titleScreen;
    public GameObject TaptoplayScreen;
    public GameObject gameOverScreen;
    public GameObject SettingsScreen;

    public float timer, refresh, avgFramerate;
    public string display = "{0} FPS";
    public TextMeshProUGUI fpsRate;
    private Player playerScript;



    private void Awake()
    {
        Menu();
        gameOverScreen.SetActive(false);
        gamePlayScreen.SetActive(false);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        LoadGameRank();
    }
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        SetBestPlayer();
        SettingsScreen.SetActive(false);
    }
    

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void PlayButton()
    {
        titleScreen.SetActive(false);
        TaptoplayScreen.SetActive(true);
    }

    public void StartGame()
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        TaptoplayScreen.SetActive(false);
        Time.timeScale = 1;
        gamePlayScreen.SetActive(true);
    }
    public void Menu()
    {
        titleScreen.SetActive(true);
        SettingsScreen.SetActive(false);
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0; // Freeze physics calculations
    }
    public void Settings()
    {
        SettingsScreen.SetActive(true);
        titleScreen.SetActive(false);
    }
    public void gameoverTitle()
    {
        gameOverScreen.SetActive(true);
        CheckBestPlayer();
        endScore.text = "Score:" + score;
        gamePlayScreen.SetActive(false);
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;// freeze physics calculations
    }
    public void Exit()
    {
        Application.Quit();
    }
   

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        score += 1;
        playerScript.windSoundEffect.Play();
        PlayerDataHandle.Instance.score = score;
        scoreText.text =  "" + score;
    }
    void Update()
    {
        cubeMapMaterial.SetFloat("_Rotation", Time.time * rotationSpeed);

        //for FPS text
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;
        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        fpsRate.text = string.Format(display, avgFramerate.ToString());
    }
    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            BestScore = data.HighiestScore;
        }
    }
   private void CheckBestPlayer()
    {
        int CurrentScore = PlayerDataHandle.Instance.score;

        if (CurrentScore > BestScore)
        {
            BestScore = CurrentScore;
            BestPlayerScoreTxt.text = $"Best Score: {BestScore}";
            SaveGameRank(BestScore);
        }
    }
    private void SetBestPlayer()
    {
        if (BestScore == 0)
        {
            BestPlayerScoreTxt.text = "";
        }
        else
        {
            BestPlayerScoreTxt.text = $"Best Score: {BestScore}";
        }

    }
    public void SaveGameRank(int bestPlayerScore)
    {
        SaveData data = new SaveData();
        data.HighiestScore = bestPlayerScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Add a public method to reset the high score
    public void ResetHighScore()
    {
        BestScore = 0; // Reset the high score to zero
        BestPlayerScoreTxt.text = ""; // Clear the high score text
        SaveGameRank(BestScore); // Save the updated high score to your save file
    }

    // Attach this method to a button click event or call it when needed
    public void OnResetHighScoreButtonClick()
    {
        ResetHighScore(); // Call the reset method when the reset button is clicked
    }
    

    [System.Serializable]
    class SaveData
    {
        public int HighiestScore;
    }
}
