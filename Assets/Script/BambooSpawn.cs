using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooSpawn : MonoBehaviour
{
    public GameObject bamboo;
    
    private GameObject spawnPos;

    public void Start()
    {
        spawnPos = GameObject.Find("BAMBOO SPAWN");
    }

    public void LateUpdate()
    {
        transform.Translate(Vector2.down * 0.07f);
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Trigger")
        {
            Instantiate(bamboo, spawnPos.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
