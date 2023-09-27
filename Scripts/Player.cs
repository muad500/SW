using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    public AudioSource windSoundEffect;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider SFXvolumeSlider;

    public bool gameOver = false;
    public bool gameoverText = false;
    
    public GameManager gameManagerScript;
    private bool jumpkeywaspressed;
    private Rigidbody rigidbodyComponent;
    float jumpPower = 400f;
    public bool isJumping = false;
    public GameObject player;


    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAnim = GetComponent<Animator>();

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        else
        {
            Load();
        }


        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 0.4f);
        }
        else
        {
            LoadSFXVolume();
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) && player.transform.position.y < 6f)
        {
            playerAnim.SetTrigger("Tap_trig");
            jumpkeywaspressed = true;
            Time.fixedDeltaTime = 0.02f;
        }
    }

    void FixedUpdate()
    {
        if (jumpkeywaspressed && gameOver == false)
        {
            Debug.Log("Space Key was pressed Down");
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.Acceleration);
            jumpkeywaspressed = false;
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //isGrounded = true;
        if (other.gameObject.CompareTag("obstacle"))
        {
            gameOver = true;
            gameoverText = true;
            gameManagerScript.gameoverTitle();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    public void changeSFX()
    {
        windSoundEffect.volume = SFXvolumeSlider.value;
        SaveSFXVolume();
    }
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    private void LoadSFXVolume()
    {
        SFXvolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
    private void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", SFXvolumeSlider.value);
    }
}
