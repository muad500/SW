using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI HighScoreTxt;

    //Static variables for holding the best player data
    private static int BestScore;

    private void SetBestPlayer()
    {
        if (BestScore == 0)
        {
            HighScoreTxt.text = "";
        }
        else
        {
            HighScoreTxt.text = $"Best Score - : {BestScore}";
        }

    }

    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            BestScore = data.HighiestScore;
            SetBestPlayer();
        }
    }
    
    [System.Serializable]
    class SaveData
    {
        public int HighiestScore;
    }
   
}
