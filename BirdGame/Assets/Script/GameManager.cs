using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MenuControl menuControl;

    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private GameObject spike;
    [SerializeField]
    private Transform leftSpike;
    [SerializeField]
    private Transform rightSpike;
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    private GameObject[] imageColors = new GameObject[5];
    [SerializeField]
    private GameObject[] objectColors = new GameObject[14];

    public AudioClip jumpSound, scoreSound;
    public int score;
    public int numberOfSpike;
    public int bestScore;
    public int maxSpike, minSpike;
    public int randomValue;
    public int randomColorR, randomColorG, randomColorB;

    void Start()
    {
        menuControl = FindObjectOfType<MenuControl>();
        Time.timeScale = 0; //oyun başladığında pause moduna alır.
        ColorMaker(); // başladığında random renk metotu 1 kere çalışır.
    }
    void Update()
    {
    }
    public void ColorMaker() //her 10 skor'da bir random renk atar
    {
        if (score % 10 == 0)
        {
            randomColorR = Random.Range(100, 256);
            randomColorG = Random.Range(100, 256);
            randomColorB = Random.Range(100, 256);
            for (int i = 0; i < imageColors.Length; i++)
            {
                imageColors[i].GetComponent<Image>().color = new Color(randomColorR / 255f, randomColorG / 255f, randomColorB / 255f);
            }
            for (int i = 0; i < objectColors.Length; i++)
            {
                objectColors[i].GetComponent<SpriteRenderer>().color = new Color(randomColorR / 255f, randomColorG / 255f, randomColorB / 255f);
            }

        }
    }
    public void Difficulty() //zorluk mode 30'a kadar olan her 10 skor'da yükselir.
    {
        if (score >= 30) //expert
        {
            minSpike = 4; //spawn edilecek minimum spike sayısı
            maxSpike = 7; //spawn edilecek maksimum spike sayısı
        }
        else if(score >=20) //hard
        {
            minSpike = 3;
            maxSpike = 6;
        }
        else if (score >= 10) //medium
        {
            minSpike = 2;
            maxSpike = 5;
        }
        else //easy
        {
            minSpike = 1;
            maxSpike = 4;
        }
        randomValue = Random.Range(minSpike, maxSpike); //zorluktan gelen min ve max değerlere göre rastgele spike sayısı
    }

    public void LeftSpikeSpawn() //Solda spike spawn eder.
    {
        Difficulty(); //zorluk değerlerini çektiğimiz metod.
        ColorMaker();
        spike.GetComponent<Transform>().localScale = new Vector2(0.2f, 0.2f); //spike'ı ters çevir
        Instantiate(spike, new Vector2(-2.3f, Random.Range(3.50f, -3.50f)), Quaternion.identity, leftSpike); //referans spike spawn edilir.
        if (leftSpike.GetChild(0).GetComponent<Transform>().position.y <= 0) //referans spike yükseklikte ortanın altında ise
        {
            for (float i = 1; i < randomValue; i++)// gelen rastgele değere göre spike spawn'ı.
            {
                Instantiate(spike, new Vector2(-2.3f, leftSpike.GetChild(0).GetComponent<Transform>().position.y + i), Quaternion.identity, leftSpike);
            }
        }
        if (leftSpike.GetChild(0).GetComponent<Transform>().position.y > 0) //referans spike yükseklikte ortanın üstünde ise
        {
            for (float i = 1; i < randomValue; i++)
            {
                Instantiate(spike, new Vector2(-2.3f, leftSpike.GetChild(0).GetComponent<Transform>().position.y - i), Quaternion.identity, leftSpike);
            }
        }
        for (int i = 0; i < leftSpike.childCount; i++) //fazla yüksekte ya da alçakta olan spike'ları siler.
        {
            if (leftSpike.GetChild(i).GetComponent<Transform>().position.y > 3.5 || leftSpike.GetChild(i).GetComponent<Transform>().position.y < -3.5)
            {
                Destroy(leftSpike.GetChild(i).gameObject);
            }
        }
    }

    public void RightSpikeSpawn() //Sağda spike spawn eder.
    {
        Difficulty();
        ColorMaker();
        spike.GetComponent<Transform>().localScale = new Vector2(-0.2f, 0.2f);
        Instantiate(spike, new Vector2(2.3f, Random.Range(2.3f, -3.5f)), Quaternion.identity, rightSpike);
        if (rightSpike.GetChild(0).GetComponent<Transform>().position.y <=0)
        {
            for (float i = 1; i < randomValue; i++)
            {
                Instantiate(spike, new Vector2(2.3f, rightSpike.GetChild(0).GetComponent<Transform>().position.y + i), Quaternion.identity, rightSpike);
            }
        }
        if (rightSpike.GetChild(0).GetComponent<Transform>().position.y > 0)
        {
            for (float i = 1; i < randomValue; i++)
            {
                Instantiate(spike, new Vector2(2.3f, rightSpike.GetChild(0).GetComponent<Transform>().position.y - i), Quaternion.identity, rightSpike);
            }
        }
        for (int i = 0; i < rightSpike.childCount; i++)
        {
            if (rightSpike.GetChild(i).GetComponent<Transform>().position.y > 3.5 || rightSpike.GetChild(i).GetComponent<Transform>().position.y < -3.5)
            {
                Destroy(rightSpike.GetChild(i).gameObject);
            }
        }
    }

    public void ScoreControl() //skor sesi, arttırma ve yazdırma metodu.
    {
        audioSource.PlayOneShot(scoreSound,menuControl.soundVolume);
        score += 1;
        scoreTxt.text = score.ToString();
    }
    public void BestScoreControl() //en yüksek skor kontrolü ve kaydı.
    {
        if (score > bestScore)
        {
            bestScore = score;
            if (PlayerPrefs.GetInt("bestScore") < bestScore)
            {
                PlayerPrefs.SetInt("bestScore", bestScore);
            }
        }
    }
}
