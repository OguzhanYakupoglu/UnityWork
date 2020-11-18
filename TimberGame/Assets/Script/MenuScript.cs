using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    private GameObject SoundText;
    
    public float soundVolume;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        gameManager = FindObjectOfType<GameManager>();
        if (PlayerPrefs.GetFloat("Sound")>0f)
        {
            SoundText.GetComponent<Text>().text = "SOUND ON";
        }
        if (PlayerPrefs.GetFloat("Sound") <= 0f)
        {
            SoundText.GetComponent<Text>().text = "SOUND OFF";
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Sound()
    {
        if (PlayerPrefs.GetFloat("Sound")<=0f)
        {
            SoundText.GetComponent<Text>().text = "SOUND ON";
            soundVolume = 1f;
            PlayerPrefs.SetFloat("Sound", soundVolume);
        }
        else if (PlayerPrefs.GetFloat("Sound") > 0f)
        {
            SoundText.GetComponent<Text>().text = "SOUND OFF";
            soundVolume = 0f;
            PlayerPrefs.SetFloat("Sound", soundVolume);
        }
    }
}
