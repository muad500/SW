using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private Player playerScript;
    public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerScript.gameOver)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        
    }
}
