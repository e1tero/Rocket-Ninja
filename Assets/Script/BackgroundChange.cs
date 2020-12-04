using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundChange : MonoBehaviour
{
    public SaveData saveData;
    public Sprite[] sprite;
    public GameObject fireWork1;
    public GameObject fireWork2;
    public GameObject fireWork3;
    public GameObject fireWork4;
    public Text recordText;
    public Text recordScore;
    public GameObject recordSound;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            saveData.recordPoints = PlayerPrefs.GetInt("record");
            var random = Random.Range(0, 4);
            GameObject.Find("BACKGROUND").GetComponent<SpriteRenderer>().sprite = sprite[random];
            saveData.backgroundSprite = GameObject.Find("BACKGROUND").GetComponent<SpriteRenderer>().sprite;
        }
        else if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            GameObject.Find("BACKGROUND").GetComponent<SpriteRenderer>().sprite = saveData.backgroundSprite;
        }
        
        else if (SceneManager.GetActiveScene().name == "DeathScene")
        {
            PlayerPrefs.SetInt("record", saveData.recordPoints);
            GameObject.Find("BACKGROUND").GetComponent<SpriteRenderer>().sprite = saveData.backgroundSprite;
            if (saveData.currentPoints == saveData.recordPoints)
            {
                StartCoroutine(ShowFireWork1());
                Instantiate(recordSound, transform.position, Quaternion.identity);
                recordScore.color = Color.red;
                recordText.color = Color.red;
            }
        }
    }

    IEnumerator ShowFireWork1()
    {
        yield return new WaitForSeconds(1f);
        fireWork1.SetActive(true);
        StartCoroutine(ShowFireWork2());
    }
    
    IEnumerator ShowFireWork2()
    {
        yield return new WaitForSeconds(1f);
        fireWork2.SetActive(true);
        StartCoroutine(ShowFireWork3());
    }
    
    IEnumerator ShowFireWork3()
    {
        yield return new WaitForSeconds(1f);
        fireWork3.SetActive(true);
        StartCoroutine(ShowFireWork4());
    }
    
    IEnumerator ShowFireWork4()
    {
        yield return new WaitForSeconds(1f);
        fireWork4.SetActive(true);
    }
    
}
