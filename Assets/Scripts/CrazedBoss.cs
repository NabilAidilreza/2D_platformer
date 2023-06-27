using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrazedBoss : Goblin
{
    private Animator CrazedAnim;
    public Slider BossBar;
    public GameObject UIBar;
    private bool Woken;
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;
    public GameObject Pos4;
    public GameObject Pos5;
    public GameObject Pos6;
    public GameObject Pos7;
    public GameObject Pos8;
    public GameObject S1;
    public GameObject S2;
    public GameObject BloodBall;
    public GameObject BloodBall1;
    public GameObject Star;
    public GameObject PortalItem;
    public Transform PortalPos;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        name = "CrazedGoblinPriest";
        currhealth = health;
        oldPosition = transform.position.x;
        CrazedAnim = GetComponent<Animator>();
        CrazedAnim.SetBool("Woken", false);
        Woken = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        //FLip
        if (transform.position.x > oldPosition) // he's looking right
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        //Abilities
        if(CrazedAnim.GetBool("Duplica") == true)
        {
            Instantiate(BloodBall, Pos1.transform.position, Quaternion.identity);
            Instantiate(BloodBall1, Pos2.transform.position, Quaternion.identity);
            Instantiate(BloodBall, Pos3.transform.position, Quaternion.identity);
            Instantiate(BloodBall1, Pos4.transform.position, Quaternion.identity);
            Instantiate(BloodBall, Pos5.transform.position, Quaternion.identity);
            Instantiate(BloodBall1, Pos6.transform.position, Quaternion.identity);
            Instantiate(BloodBall, Pos7.transform.position, Quaternion.identity);
            Instantiate(BloodBall1, Pos8.transform.position, Quaternion.identity);
            CrazedAnim.SetBool("Duplica", false);
        }
        if(CrazedAnim.GetBool("Star") == true)
        {
            Instantiate(Star, S1.transform.position, Quaternion.identity);
            Instantiate(Star, S2.transform.position, Quaternion.identity);
            CrazedAnim.SetBool("Star", false);
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
            //CrazedAnim.SetBool("Dead", true);
            StartCoroutine(Death());
            Instantiate(PortalItem, PortalPos.position, Quaternion.identity);

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
        }
        else if (DistanceToP < Attackrange)
        {
            CrazedAnim.SetBool("Woken", true);
            Woken = true;
        }
    }
}
