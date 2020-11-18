using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuControl : MonoBehaviour
{
    public GameManager gameManager;
    public GameOverControl gameOverControl;

    [SerializeField]
    private Text bestScoreMenu;
    [SerializeField]
    private GameObject soundBtnImage;
    [SerializeField]
    private Sprite[] soundType = new Sprite[2];

    public float soundVolume;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameOverControl = FindObjectOfType<GameOverControl>();
        gameManager.BestScoreControl();
        bestScoreMenu.text += PlayerPrefs.GetInt("bestScore").ToString();
        //
        if (PlayerPrefs.GetFloat("Sound") == 1f) //oyun başladığında en son kullanılan ses aktiflik durumu
        {
            //On
            soundBtnImage.GetComponent<Image>().sprite = soundType[0];
            soundVolume = 1f;
            PlayerPrefs.SetFloat("Sound", soundVolume);
        }
        if (PlayerPrefs.GetFloat("Sound") == 0f)
        {
            //OFF
            soundBtnImage.GetComponent<Image>().sprite = soundType[1];
            soundVolume = 0f;
            PlayerPrefs.SetFloat("Sound", soundVolume);
        }
    }
    public void ReplayBtn() //sahneyi tekrar yükleyen buton'un metodu.
    {
        SceneManager.LoadScene("GameScene");
    }
    public void SoundBtn() //ses butonu.
    {
        if (PlayerPrefs.GetFloat("Sound") == 0f)
        {
            //On
            soundBtnImage.GetComponent<Image>().sprite = soundType[0];
            soundVolume = 1f;
            PlayerPrefs.SetFloat("Sound", soundVolume);
        }
        else if (PlayerPrefs.GetFloat("Sound") == 1f)
        {
            //OFF
            soundBtnImage.GetComponent<Image>().sprite = soundType[1];
            soundVolume = 0f;
            PlayerPrefs.SetFloat("Sound", soundVolume);
        }
    }

}
