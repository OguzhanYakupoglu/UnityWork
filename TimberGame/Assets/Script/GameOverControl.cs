using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverControl : MonoBehaviour
{
    public GameManager gameManager;
    public CharacterControl characterControl;

    [SerializeField]
    private Text scoreTxt, bestScoreTxt;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip deathClip;

    public int bestScore;

    private void Start()
    {
        audioSource.PlayOneShot(deathClip, PlayerPrefs.GetFloat("Sound"));
        gameManager = FindObjectOfType<GameManager>();
        characterControl = FindObjectOfType<CharacterControl>();
        characterControl.GetComponent<Animator>().SetTrigger("Death");
        scoreTxt.GetComponent<Text>().text = gameManager.score.ToString();

        if (gameManager.score > bestScore)
        {
            bestScore = gameManager.score;
            if (PlayerPrefs.GetInt("bestScore")<bestScore)
            {
                PlayerPrefs.SetInt("bestScore", bestScore);

            }
            bestScoreTxt.text = PlayerPrefs.GetInt("bestScore").ToString();
        }
    }

    public void PlayAgainBtn()
    {
        gameManager.LoadGameScene();
    }

    public void MainMenuBtn()
    {
        gameManager.LoadMenuScene();
    }
}
