using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Player player;
    public float speed;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player.immortality)
        {
            speed = 0.2f;
        }

        if (player.points > 10 && !player.immortality)
            speed = 0.09f;
        
        if (player.points > 20 && !player.immortality)
            speed = 0.11f;
        
        if (player.points > 30 && !player.immortality)
            speed = 0.13f;
        
        if (player.points > 40 && !player.immortality)
            speed = 0.16f;
       
        if (player.points > 60 && !player.immortality)
            speed = 0.2f;
        
    }
    
    void LateUpdate()
    {
        transform.Translate(Vector2.down * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            player.points++;
            StartCoroutine(DestroyTime());
        }
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this);
    }
}
