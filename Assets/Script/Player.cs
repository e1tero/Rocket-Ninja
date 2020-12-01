using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject firstMainWaypoint;
    public GameObject lossHealthPosition;
    public GameObject secondMainWaypoint;
    public GameObject blood;
    public GameObject electric;
    public GameObject electricFirstPos;
    public GameObject electricSecondPos;
    public GameObject firstWaypoint;
    public GameObject lossHealth;
    public GameObject fireEffects;
    public GameObject secondWaypoint;
    public SaveData saveData;
    public Camera cam;
    public int points; 
    private bool check;
    private bool click;
    public float speed;
    private bool down;
    public int health;
    public bool immortality;
    private bool immunity;
    public bool speedOn;
    public int immortalityScore;
    private Animator anim;
    private Color color;


    void Start()
    {
        anim = GetComponent<Animator>();
        immortality = false;
        immunity = false;
        speedOn = false;
        health = 3;
    }
    
    
    void LateUpdate()
    {
        if (speedOn)
            speed = 11;
        else speed = 7;
        
        if (!check && down == false)
        {
            transform.position =
                    Vector3.MoveTowards(transform.position, firstMainWaypoint.transform.position, speed * Time.deltaTime);
        }
        
        if (check && down == false)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, secondMainWaypoint.transform.position, speed * Time.deltaTime);
        }
        
        if (transform.position.x == firstMainWaypoint.transform.position.x)
        {
            anim.SetTrigger("run");
            click = true;
            down = true;
            transform.position = Vector3.MoveTowards(transform.position, firstWaypoint.transform.position,
                0.2f * Time.deltaTime);
        }

        else if (transform.position.x == secondMainWaypoint.transform.position.x)
        {
            anim.SetTrigger("run");
            click = true;
            down = true;
            transform.position = Vector3.MoveTowards(transform.position, secondWaypoint.transform.position,
                0.2f * Time.deltaTime);
        }
    }

    public void Change()
    {
        if (click)
        {
            anim.SetTrigger("jump");
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1,transform.localScale.z);
            down = false;
            check = !check;
            click = false;
        }
    }
    
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Let" && !immortality && !immunity)
        {
            immortalityScore = 0;
            immunity = true;
            StartCoroutine(ImmunityTime());
            Instantiate(@lossHealth, lossHealthPosition.transform.position, Quaternion.identity);
            Instantiate(@blood, transform.position, Quaternion.identity);
            health--;
            Death();
        }
        
        else if (collider.gameObject.tag == "Health")
        {
            Destroy(collider.gameObject);
            if (health < 3)
            {
                health++;
            }
        }
        
        else if (collider.gameObject.tag == "Speed")
        {
            cam.GetComponent<Animator>().SetTrigger("shake");
            Destroy(collider.gameObject);
            Instantiate(@electric, electricFirstPos.transform.position, Quaternion.identity);
            Instantiate(@electric, electricSecondPos.transform.position, Quaternion.identity);
            speedOn = true;
            StartCoroutine(SpeedTime());
        }
        
        else if (collider.gameObject.tag == "Bonus")
        {
            Destroy(collider.gameObject);
            immortalityScore++;
            if (immortalityScore == 3)
            {
                cam.GetComponent<Animator>().SetTrigger("shake");
                immortality = true;
                fireEffects.SetActive(true);
                StartCoroutine(ImmortalityTime());
                immortalityScore = 0; 
            }
        }
    }

    public void Death()
    {
        if (health <= 0 && !immortality)
        {
            saveData.currentPoints = points;
            if (saveData.currentPoints > saveData.recordPoints)
                saveData.recordPoints = saveData.currentPoints;
            SceneManager.LoadScene("DeathScene");
        }
    }

    IEnumerator ImmunityTime()
    {
        yield return new WaitForSecondsRealtime(3f);
        immunity = false;
    }
    IEnumerator ImmortalityTime()
    {
        yield return new WaitForSecondsRealtime(6f);
        immortality = false;
        fireEffects.SetActive(false);
    }
    
    IEnumerator SpeedTime()
    {
        yield return new WaitForSeconds(8f);
        speedOn = false;
    }
    
}
