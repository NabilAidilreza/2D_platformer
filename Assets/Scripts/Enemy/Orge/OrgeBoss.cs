using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrgeBoss : Goblin
{
    private Animator CyclopsAnim;
    public Slider BossBar;
    public GameObject UIBar;
    private bool Woken;
    private float InRange;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        name = "Cyclops";
        currhealth = health;
        oldPosition = transform.position.x;
        CyclopsAnim = GetComponent<Animator>();
        CyclopsAnim.SetBool("Woken", false);
        Woken = false;
        InRange = 70;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (Woken)
        {
            UIBar.SetActive(true);
            
        }
        else
        {
            UIBar.SetActive(false);           
        }
        BossBar.value = currhealth;
        //MaxHealth
        if (currhealth > health)
        {
            currhealth = health;
        }
        //Debug.Log(name + " HEALTH NOW: " + currhealth);

        //FLip
        if (transform.position.x > oldPosition) // he's looking right
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (transform.position.x < oldPosition) // he's looking left
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        oldPosition = transform.position.x;
        //CheckDead
        if (currhealth <= 0)
        {
            //KillEnemy
            Woken = false;
            UIBar.SetActive(false);
            CyclopsAnim.SetBool("Dead", true);
            StartCoroutine(Death());
            
        }
        IEnumerator Death()
        {
            yield return new WaitForSeconds(2);
            Instantiate(Burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }       
        //Move
        DistanceToP = Vector2.Distance(transform.position, target.position);
        if (DistanceToP > Attackrange)
        {
            
            CyclopsAnim.SetBool("Near", false);
            

        }
        else if (DistanceToP < Attackrange)
        {

            CyclopsAnim.SetBool("Near", true);      
            CyclopsAnim.SetBool("Woken", true);
            Woken = true;           
        }
        if(DistanceToP > InRange && Woken == true)
        {
            UIBar.SetActive(false);
        }
       
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("PlayerDamage", damage);
        }

    }
}
