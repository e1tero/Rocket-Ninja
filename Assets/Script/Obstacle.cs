using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Player player;
    public float speed = 0.09f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    
    
    void LateUpdate()
    {
        transform.Translate(Vector2.down * player.letSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish" && !player.death)
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
