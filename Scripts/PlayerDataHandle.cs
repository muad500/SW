using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHandle : MonoBehaviour
{
    public static PlayerDataHandle Instance;
    public int score;

    private void Awake()
    {
        //we dont actually need this if statement because when we are in the Main Scene we cant return to the Start Menu Scene where the Player Data Handle Object already exists.
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
