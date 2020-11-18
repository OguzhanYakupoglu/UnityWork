using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject treeBody;
    [SerializeField]
    private GameObject arm, armLeft;
    [SerializeField]
    private Transform treeBodyGroup, armGroup;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text inGameScoreTxt;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private Sprite[] bodySprite = new Sprite[2];
    
    public CharacterControl characterControl;
    public TimeManager timeManager;
    public MenuScript menuScript;
    public int randomArm;   
    public int score;
    bool armControl=true;


    private void Start()
    {
        audioSource.PlayOneShot(music, PlayerPrefs.GetFloat("Sound"));
        characterControl = FindObjectOfType<CharacterControl>();
        timeManager = FindObjectOfType<TimeManager>();
        menuScript = FindObjectOfType<MenuScript>();

        for (int i = -2; i < 8; i++)
        {
            Instantiate(treeBody, new Vector2(0, i), Quaternion.identity, treeBodyGroup);
            treeBody.GetComponent<SpriteRenderer>().sprite = bodySprite[Random.Range(0, 2)];
        }
    }

    public void BodySpawn()
    {
        Instantiate(treeBody, new Vector2(0, 7f), Quaternion.identity, treeBodyGroup);
        treeBody.GetComponent<SpriteRenderer>().sprite = bodySprite[Random.Range(0, 2)];
    }
    public void CutBody()
    {
        Destroy(GameObject.FindWithTag("Body"));
    }
    public void ArmSpawn()
    {
        randomArm = Random.Range(0, 101);
        if (randomArm <= 35 && armControl)
        {
            Instantiate(arm, new Vector2(1, 7), Quaternion.identity, armGroup);
            armControl = false;
        }
        else if (randomArm >= 65 && armControl)
        {
            Instantiate(armLeft, new Vector2(-1, 7), Quaternion.identity, armGroup);
            armControl = false;
        }
        else
        {
            armControl = true;
        }
    }

    public void positionDown()
    {
        for (int i = 0; i < 10; i++)
        {
            treeBodyGroup.GetChild(i).transform.Translate(0, -1, 0);
        }
        for (int i = 0; i < armGroup.childCount; i++)
        {
            armGroup.GetChild(i).transform.Translate(0, -1, 0);
            if (armGroup.GetChild(i).transform.position.y <= -3)
            {
                Destroy(GameObject.FindWithTag("Arm"));
            }
        }
    }

    public void GameOverControl()
    {
        timeManager.slider.GetComponent<Slider>().value = 0;
        inGameScoreTxt.enabled = false;
        gameOverPanel.SetActive(true);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void Score()
    {
        score++;
        inGameScoreTxt.GetComponent<Text>().text = score.ToString();
    }
}
