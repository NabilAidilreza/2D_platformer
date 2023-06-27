using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : MonoBehaviour
{
    private Animator SkeleAnim;
    public float lifeTime;
    public Transform enemy;
    public float speed;
    public GameObject Burst;
    public float Attackrange;
    public float retreatDistance;
    public float DistanceToP;
    public Transform ShootPoint;
    public GameObject MageBall;
    public float startwaittime;
    private float waittime;
    //CheckDir
    public float oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        SkeleAnim = GetComponent<Animator>();
        oldPosition = transform.position.x;
        StartCoroutine(Life());
        waittime = startwaittime;
    }

    // Update is called once per frame
    void Update()
    {
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


        float DtoC = Mathf.Infinity;
        Goblin closestEnemy = null;
        Goblin[] all = GameObject.FindObjectsOfType<Goblin>();
        foreach (Goblin curr in all)
        {
            float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
            if (DtoE < DtoC)
            {
                DtoC = DtoE;
                closestEnemy = curr;
            }
        }
        DistanceToP = Vector2.Distance(transform.position, closestEnemy.transform.position);
        if(DistanceToP >= Attackrange)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position, speed * Time.deltaTime);
            SkeleAnim.SetBool("Run", true);
            SkeleAnim.SetBool("Attack", false);
        }
        else if(DistanceToP > retreatDistance && DistanceToP <= Attackrange)
        {
            //ATTACK
            if(waittime <= 0)
            {
                Instantiate(MageBall, ShootPoint.position, Quaternion.identity);
                waittime = startwaittime;
            }
            else
            {
                waittime -= Time.deltaTime;
            }
            SkeleAnim.SetBool("Attack", true);
            SkeleAnim.SetBool("Run", false);
        }
        else if(DistanceToP < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position, -speed * Time.deltaTime);
            SkeleAnim.SetBool("Run", true);
            SkeleAnim.SetBool("Attack", false);
        }
        
        
    }
    IEnumerator Life()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        Instantiate(Burst, transform.position, Quaternion.identity);
    }

}
