using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GapTrigger : MonoBehaviour
{
    public GameManager gameManagerScript;
    private float deletePosition = -10f;

    public TextMeshProUGUI scoreText;
    private bool passedGap = false;
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !passedGap)
        {
            passedGap = true;
            Debug.Log("Player passed through the gap!");
            gameManagerScript.UpdateScore();
        }
    }
    public void FixedUpdate()
    {
        if (transform.position.x < deletePosition && gameObject.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
