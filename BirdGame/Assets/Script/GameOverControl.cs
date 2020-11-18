using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverControl : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    private Transform gameOverPanel;
    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Text gameOverScoreTxt;
    [SerializeField]
    private Text bestScoreGO;

    public int bestScore;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void GameOverMethod() //karakter öldüğünde çalışan metod.
    {
        gameOverPanel.gameObject.SetActive(true); //bitiş ekranı açılır.
        scoreTxt.gameObject.SetActive(false);
        gameOverScoreTxt.GetComponent<Text>().text += gameManager.score.ToString();
        Destroy(GameObject.FindWithTag("Player")); //Karakter silinir.
        gameManager.BestScoreControl();
        bestScoreGO.text += PlayerPrefs.GetInt("bestScore").ToString();
    }
}
