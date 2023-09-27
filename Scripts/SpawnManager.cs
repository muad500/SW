using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefabs;
    private Player playerScript;
    // Start is called before the first frame update
    private void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        //Physics.gravity *= gravityModifier;
        InvokeRepeating("SpawnObjects", 3, 1.48f);
    }
    
    
    void SpawnObjects()
    {
        if(playerScript.gameOver == false)
        {
            Vector3 spawnLocation = new Vector3(8, Random.Range(2f, -5), -1.85f); 

            Instantiate(prefabs, spawnLocation, prefabs.transform.rotation);
        }
        
    }
}
