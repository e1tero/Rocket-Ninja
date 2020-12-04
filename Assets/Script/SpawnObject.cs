using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [Header("Boost")]
    public GameObject let;
    public GameObject bonus;
    public GameObject speedBoost;
    public GameObject healthBoost;
    
    public GameObject bonusSpawnPosition;
    public Player player;
    public GameObject healthSpeedPosition;
    public GameObject firstWaypoint;
    public GameObject secondWaypoint;
    public int random;
    public int randomBonusTime;
    public int randomSpeedTime;
    public int randomHealthTime;

    public float spawnTimeLet;
    
    
    void Update()
    {
        Debug.Log(randomBonusTime);

        if (player.points > 20)
        {
            spawnTimeLet = 0.7f;
        }

        if (player.points > 30)
        {
            spawnTimeLet = 0.5f;
        }
        
        if (player.points > 40)
        {
            spawnTimeLet = 0.4f;
        }
    }
    
    void Start()
    {
        spawnTimeLet = 1.3f;
        Invoke("SpawnBonus",3f);
        Invoke("SpawnHealth",5f);
        SpawnLet();
        Invoke("SpawnSpeed",7f);
    }

    void SpawnLet()
    {
        random = Random.Range(-1, 2);
        if (random == 0)
            Instantiate(@let, firstWaypoint.transform.position, Quaternion.identity);
        else if (random == 1)
            Instantiate(@let, secondWaypoint.transform.position, Quaternion.identity);

        StartCoroutine(SpawnTime());
    }

    void SpawnSpeed()
    {
        Instantiate(@speedBoost, healthSpeedPosition.transform.position, Quaternion.identity);
        StartCoroutine(SpeedTime());
    }

    void SpawnHealth()
    {
        Instantiate(@healthBoost, healthSpeedPosition.transform.position, Quaternion.identity);
        StartCoroutine(HealthTime());
    }
    void SpawnBonus()
    {
        Instantiate(@bonus, bonusSpawnPosition.transform.position, Quaternion.identity);
        StartCoroutine(BonusTime());
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSecondsRealtime(spawnTimeLet);
        SpawnLet();
    }
    
    IEnumerator HealthTime()
    {
        randomHealthTime = Random.Range(8, 15);
        yield return new WaitForSecondsRealtime(randomHealthTime);
        SpawnHealth();
    }

    IEnumerator BonusTime()
    {
        randomBonusTime = Random.Range(3, 6);
        yield return new WaitForSecondsRealtime(randomBonusTime);
        SpawnBonus();
    }
    
    IEnumerator SpeedTime()
    {
        randomSpeedTime = Random.Range(12, 17);
        yield return new WaitForSecondsRealtime(randomSpeedTime);
        SpawnSpeed();
    }
}
