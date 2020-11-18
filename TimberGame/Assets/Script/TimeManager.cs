using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    public GameObject slider;

    public float leftTime = 1f;
    public bool isTimeStart=false;
    CharacterControl characterControl;
    GameManager gameManager;

    void Start()
    {
        characterControl = FindObjectOfType<CharacterControl>();
        gameManager = FindObjectOfType<GameManager>();
        gameObject.GetComponent<Slider>().value = leftTime;
    }
    void Update()
    {
        if (isTimeStart==true)
        {
            if (gameObject.GetComponent<Slider>().value <= 0)
            {
                gameManager.GameOverControl();
            }
            gameObject.GetComponent<Slider>().value -= 0.15f * Time.deltaTime;
        }    
    }
    public void TimeUp()
    {
        gameObject.GetComponent<Slider>().value += 10f * Time.deltaTime;
    }
}
