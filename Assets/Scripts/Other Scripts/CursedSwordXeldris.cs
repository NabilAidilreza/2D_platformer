using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursedSwordXeldris : Goblin
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
    public float Timer;
    public Transform WayPoint;
    private Animator CursedSwordAnim;
    public GameObject CursedArrow;
    public Transform ArrowPoint;
    public GameObject Beam;
    public Transform BeamPoint;
    public GameObject Arrow;
    public Transform ShootPoint;
    public GameObject Minion;
    public Transform Sp1;
    public Transform Sp2;
    public Transform Sp3;
    public float startbowtime;
    private float bowtime;
    public float startarrowtime;
    private float arrowtime;
    public float startspawnertime;
    private float spawnertime;
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
        bowtime = startbowtime;
        arrowtime = startarrowtime;
        spawnertime = startspawnertime;
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
        if(Timer <= 0 || transform.position == WayPoint.position)
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
            Instantiate(PortalItem, PortalPos.position, Quaternion.identity);

        }
        IEnumerator Death()
        {
            yield return new WaitForSeconds(2);
            Instantiate(Burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        //Abilities
        if (CursedSwordAnim.GetBool("BowAttack") == true)
        {
            if(bowtime <= 0)
            {
                Instantiate(CursedArrow, ArrowPoint.position, Quaternion.identity);
                bowtime = startbowtime;
            }
            else
            {
                bowtime -= Time.deltaTime;
            }
        }

        if(CursedSwordAnim.GetBool("StaffAttack") == true)
        {
            Instantiate(Beam, BeamPoint.position, Quaternion.identity);
        }
        if(CursedSwordAnim.GetBool("SpawnerAttack") == true)
        {
            if (spawnertime <= 0)
            {
                Instantiate(Minion, Sp1.position, Quaternion.identity);
                Instantiate(Minion, Sp2.position, Quaternion.identity);
                Instantiate(Minion, Sp3.position, Quaternion.identity);
                spawnertime = startspawnertime;
            }
            else
            {
                spawnertime -= Time.deltaTime;
            }
        }
        if (CursedSwordAnim.GetBool("ArrowAttack") == true)
        {
            if (arrowtime <= 0)
            {
                Instantiate(Arrow, ShootPoint.position, Quaternion.identity);
                arrowtime = startarrowtime;
            }
            else
            {
                arrowtime -= Time.deltaTime;
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
