using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerControl : MonoBehaviour
{
    public GameManager gameManager;
    public GameOverControl gameOverControl;
    public MenuControl menuControl;

    private float jumpPower = 1750;
    [SerializeField]
    private Transform leftSpike;
    [SerializeField]
    private Transform rightSpike;
    [SerializeField]
    private Transform menuPanel;
    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Text bestScoreGO;
    [SerializeField]
    private Text bestScoreMenu;

    public float direction;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameOverControl = FindObjectOfType<GameOverControl>();
        menuControl = FindObjectOfType<MenuControl>();

        direction = 3f;
    }

    private void FixedUpdate()
    {
        GetComponent<Transform>().Translate(direction*Time.deltaTime,0f,0f); //Haraket
    }

    public void jumpClick() //ekrana tıklandığında
    {
        if (Time.timeScale == 0) //pause'u kapatır.
        {
            Time.timeScale = 1;
            menuPanel.gameObject.SetActive(false);
            scoreTxt.gameObject.SetActive(true);
        }
        gameManager.audioSource.PlayOneShot(gameManager.jumpSound, menuControl.soundVolume); //zıplama sesi
        GetComponent<Animator>().SetTrigger("Jump"); //animasyon
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower); //yukarı yöne uygulanacak kuvvet
    }
    private void OnCollisionEnter2D(Collision2D collision) //Karakter temas kontrolü.
    {
        if (collision.gameObject.name == "botWall" || collision.gameObject.name == "topWall" || collision.gameObject.tag == "Spike") //üst, alt ve yan spike'lara çarpınca ölme.
        {
            gameOverControl.GameOverMethod();
        }

        if (collision.gameObject.name == "rightWall") //Sağa çarparsa.
        {
            direction *= -1f; //ters yöne dönme
            gameManager.ScoreControl();
            GetComponent<SpriteRenderer>().flipX = true; //player objesini ters çevirir
            gameManager.LeftSpikeSpawn();
            foreach (Transform child in rightSpike.transform) //diğer duvardaki tüm spike'ları destroy eder.
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        
        if (collision.gameObject.name == "leftWall")
        {
            direction *= -1f;
            gameManager.ScoreControl();
            GetComponent<SpriteRenderer>().flipX = false;
            gameManager.RightSpikeSpawn();
            foreach (Transform child in leftSpike.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
