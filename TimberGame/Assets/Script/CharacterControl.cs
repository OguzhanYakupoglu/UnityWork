using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public GameManager gameManager;
    public TimeManager timeManager;

    [SerializeField]
    private GameObject tapPanel;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip attack;

    public bool isRight=true;
    public bool isClicked = false;

    private void Start()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
        timeManager = Object.FindObjectOfType<TimeManager>();
    }
    public void LeftButtonClick()
    {
        if (isClicked == false)
        {
            timeManager.isTimeStart = true;
            isClicked = true;
            tapPanel.SetActive(false);
            GetComponent<Animator>().SetTrigger("Attack");
            timeManager.TimeUp();
            gameManager.CutBody();
            gameManager.BodySpawn();
            gameManager.ArmSpawn();
            gameManager.positionDown();
            gameManager.Score();
            audioSource.PlayOneShot(attack, PlayerPrefs.GetFloat("Sound")/10);

            if (isRight)
            {
                this.GetComponent<Transform>().Translate(-3, 0, 0);
                this.GetComponent<SpriteRenderer>().flipX = false;
                isRight = false;
            }
            if (GameObject.FindWithTag("Arm").transform.position.y == -3 && GameObject.FindWithTag("Arm").transform.position.x == -1)
            {
                gameManager.GameOverControl();
            }
        } 
    }
    public void RightButtonClick()
    {
        if (isClicked == false)
        {
            timeManager.isTimeStart = true;
            isClicked = true;
            tapPanel.SetActive(false);
            GetComponent<Animator>().SetTrigger("Attack");
            timeManager.TimeUp();
            gameManager.CutBody();
            gameManager.BodySpawn();
            gameManager.ArmSpawn();
            gameManager.positionDown();
            gameManager.Score();
            audioSource.PlayOneShot(attack, PlayerPrefs.GetFloat("Sound")/10);

            if (!isRight)
            {
                this.GetComponent<Transform>().Translate(3, 0, 0);
                this.GetComponent<SpriteRenderer>().flipX = true;
                isRight = true;
            }
            if (GameObject.FindWithTag("Arm").transform.position.y == -3 && GameObject.FindWithTag("Arm").transform.position.x == 1)
            {
                gameManager.GameOverControl();
            }
        }  
    }
    public void clickUp()
    {
        isClicked = false;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arm")
        {
            gameManager.GameOverControl();
        }
    }
}
