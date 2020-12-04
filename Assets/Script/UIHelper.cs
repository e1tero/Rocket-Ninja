using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    [Header("Sounds")] 
    public GameObject buttonSound;
    public GameObject backgroundSound;
    
    public Player player;
    public Text points;
    public Text health;
    public SaveData saveData;
    public Text currentPoints;
    public Text record;
    private bool soundOn = true;
    public GameObject soundOnIcon;
    public GameObject soundOffIcon;
    public BackgroundChange backgroundChange;
    
    public void Awake ()
    {
        backgroundSound = GameObject.Find("BackgroundSound");
    }

    public void Start()
    {
        DontDestroyOnLoad(backgroundSound);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            points.text = player.points.ToString();
        }

        else if (SceneManager.GetActiveScene().name == "DeathScene")
        {
            record.text = saveData.recordPoints.ToString();
            currentPoints.text = saveData.currentPoints.ToString();
        }
    }
    
    public void StartGame()
    {
        Instantiate(buttonSound, transform.position, Quaternion.identity);
        if (SceneManager.GetActiveScene().name == "DeathScene")
        {
            var random = Random.Range(0, 4);
            saveData.backgroundSprite = backgroundChange.sprite[random];
            SceneManager.LoadScene("SampleScene");
        }
        else SceneManager.LoadScene("SampleScene");
    }

    public void SoundOn()
    {
        if (soundOn)
        {
            Instantiate(buttonSound, transform.position, Quaternion.identity);
            backgroundSound.GetComponent<AudioSource>().enabled = false;
            soundOnIcon.SetActive(false);
            soundOffIcon.SetActive(true);
            soundOn = false;
        }
        else
        {
            Instantiate(buttonSound, transform.position, Quaternion.identity);
            backgroundSound.GetComponent<AudioSource>().enabled = true;
            soundOnIcon.SetActive(true);
            soundOffIcon.SetActive(false);
            soundOn = true;
        }
    }
    

    public void RateUs()
    {
        Instantiate(buttonSound, transform.position, Quaternion.identity);
        Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
    }
}
