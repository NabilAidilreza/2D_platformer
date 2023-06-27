using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursedSwordZeldris : Goblin
{
    public Slider BossBar;
    public GameObject UIBar;
    private Transform player;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minTime;
    public float maxTime;
    private float Timer;
    public Transform WayPoint;
    private Animator CursedSwordAnim;
    public GameObject Beam;
    public Transform BeamPoint;
    public GameObject Bug;
    public Transform B1;
    public Transform B2;
    public GameObject Fall;
    public Transform F1;
    public Transform F2;
    public Transform F3;
    public Transform F4;
    public GameObject Spam;
    public Transform SP1;
    public Transform SP2;
    public float startspawnertime;
    private float spawnertime;
    private float Bugtime;
    private float Falltime;
    private float Beamtime;
    private bool Woken;
    public GameObject PortalItem;
    public Transform PortalPos;
    public Transform RestPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        oldPosition = player.position.x;
        currhealth = health;
        WayPoint.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Timer = Random.Range(minTime, maxTime);
        Beamtime = startspawnertime - 0.2f;
        spawnertime = startspawnertime - 0.4f;
        Falltime = startspawnertime;
        CursedSwordAnim = GetComponent<Animator>();
        CursedSwordAnim.SetBool("Woken", false);
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
        //MaxHealth
        if (currhealth > health)
        {
            currhealth = health;
        }
        BossBar.value = currhealth;
        //Flip
        if (transform.position.x < oldPosition) // he's looking right
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (transform.position.x > oldPosition) // he's looking left
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = player.position.x;

        //Movement
        if (Woken)
        {
            if (CursedSwordAnim.GetBool("Rest") == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, WayPoint.position, speed * Time.deltaTime);
            }
        }
        if (Timer <= 0 || transform.position == WayPoint.position)
        {
            WayPoint.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Timer = Random.Range(minTime, maxTime);
        }
        else
        {
            Timer -= Time.deltaTime;
        }
        if (currhealth <= 0)
        {
            //KillEnemy
            Woken = false;
            UIBar.SetActive(false);
            //CrazedAnim.SetBool("Dead", true);
            StartCoroutine(Death());
            //Instantiate(PortalItem, PortalPos.position, Quaternion.identity);

        }
        IEnumerator Death()
        {
            yield return new WaitForSeconds(2);
            Instantiate(Burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        //Abilities
        if (CursedSwordAnim.GetBool("BuggerAttack") == true)
        {
            if (Bugtime <= 0)
            {
                Instantiate(Bug, B1.position, Quaternion.identity);
                Instantiate(Bug, B2.position, Quaternion.identity);
                Bugtime = startspawnertime;
            }
            else
            {
                Bugtime -= Time.deltaTime;
            }
        }

        if (CursedSwordAnim.GetBool("BeamerAttack") == true)
        {
            if(Beamtime <= 0)
            {
                Instantiate(Beam, BeamPoint.position, Quaternion.identity);
                Beamtime = startspawnertime;
            }
            else
            {
                Beamtime -= Time.deltaTime;
            }
            
        }
        if (CursedSwordAnim.GetBool("SpammerAttack") == true)
        {
            if (spawnertime <= 0)
            {
                Instantiate(Spam, SP1.position, Quaternion.identity);
                Instantiate(Spam, SP2.position, Quaternion.identity);
                spawnertime = startspawnertime - 0.4f;
            }
            else
            {
                spawnertime -= Time.deltaTime;
            }
        }
        if (CursedSwordAnim.GetBool("FallerAttack") == true)
        {
            if (Falltime <= 0)
            {
                Instantiate(Fall, F1.position, Quaternion.identity);
                Instantiate(Fall, F2.position, Quaternion.identity);
                Instantiate(Fall, F3.position, Quaternion.identity);
                Instantiate(Fall, F4.position, Quaternion.identity);
                Falltime = startspawnertime;
            }
            else
            {
                Falltime -= Time.deltaTime;
            }
        }
        if (CursedSwordAnim.GetBool("Rest") == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, RestPoint.position, speed * Time.deltaTime);
            currhealth += 0.1f;
        }
        //Move
        DistanceToP = Vector2.Distance(transform.position, player.position);
        if (DistanceToP < Attackrange)
        {
            CursedSwordAnim.SetBool("Woken", true);
            Woken = true;
        }
    }
}

