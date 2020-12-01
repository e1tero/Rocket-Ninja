using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    public Player player;
    public Text points;
    public Text health;
    public SaveData saveData;
    public Text currentPoints;
    public Text record;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            health.text = player.health.ToString();
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
        SceneManager.LoadScene("SampleScene");
    }
}
