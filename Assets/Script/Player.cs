using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Sounds")] 
    public GameObject damageSound;
    public GameObject heartTakeSound;
    public GameObject bonusTakeSound;
    public GameObject heartIncreaseSound;
    public GameObject loseGameSound;
    public GameObject jumpSound;
    public GameObject turboSound;
    
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
    public GameObject[] hearts;
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
    private Rigidbody rb;
    public bool death;
    public float letSpeed;


    void Start()
    {
        InvokeRepeating("AddLetSpeed", 3f,3f);
        letSpeed = 0.09f;
        rb = GetComponent<Rigidbody>();
        death = false;
        anim = GetComponent<Animator>();
        immortality = false;
        immunity = false;
        speedOn = false;
        health = 3;
    }

    void Update()
    {
        switch (health)
        {
            case 1:
                hearts[0].SetActive(true);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                break;
            case 2:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(false);
                break;
            case 3:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                break;
            case 0:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                death = true;
                break;
        }
    }
    
    void LateUpdate()
    {

        if (!check && down == false && !death)
        {
            transform.position =
                    Vector3.MoveTowards(transform.position, firstMainWaypoint.transform.position, speed * Time.deltaTime);
        }
        
        if (check && down == false && !death)
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

    public void AddLetSpeed()
    {
        letSpeed += 0.005f;
    }

    public void Change()
    {
        if (click && !death)
        {
            Instantiate(jumpSound, transform.position, Quaternion.identity);
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
            Instantiate(damageSound, transform.position, Quaternion.identity);
            StartCoroutine(HeartLostSound());
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
                Instantiate(heartTakeSound, transform.position, Quaternion.identity);
                health++;
            }
        }
        
        else if (collider.gameObject.tag == "Speed")
        {
            Instantiate(bonusTakeSound, transform.position, Quaternion.identity);
            cam.GetComponent<Animator>().SetTrigger("shake");
            Destroy(collider.gameObject);
            Instantiate(@electric, electricFirstPos.transform.position, Quaternion.identity);
            Instantiate(@electric, electricSecondPos.transform.position, Quaternion.identity);
            points += 10;
        }
        
        else if (collider.gameObject.tag == "Bonus")
        {
            Instantiate(bonusTakeSound, transform.position, Quaternion.identity);
            Destroy(collider.gameObject);
            immortalityScore++;
            if (immortalityScore == 3)
            {
                Instantiate(turboSound, transform.position, Quaternion.identity);
                cam.GetComponent<Animator>().SetTrigger("shake");
                immortality = true;
                letSpeed += 0.2f;
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
            death = true;
            Instantiate(loseGameSound, transform.position, Quaternion.identity);
            rb.isKinematic = false;
            saveData.currentPoints = points;
            if (saveData.currentPoints > saveData.recordPoints)
                saveData.recordPoints = saveData.currentPoints;

            StartCoroutine(DeathTime());
        }
    }

    IEnumerator HeartLostSound()
    {
        yield return new WaitForSeconds(0.6f);
        Instantiate(heartIncreaseSound, transform.position, Quaternion.identity);
    }
    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(2.3f);
        SceneManager.LoadScene("DeathScene");
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
        letSpeed -= 0.2f;
        fireEffects.SetActive(false);
    }
}
