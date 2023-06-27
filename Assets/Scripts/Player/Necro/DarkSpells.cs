using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSpells : MonoBehaviour
{
    //Arrow Attack Variables
    public GameObject Homing;
    public GameObject Ring;
    public GameObject Ulti;
    public Transform RingPoint;
    public Transform SpawnPoint;
    public Transform UltiPoint;
    //ArrowSpam
    private float timeBtwShots;
    public float startTimeBtwShots;
    private float timeBtwHomes;
    public float startTimeBtwHomes;
    private float timeBtwUlti;
    public float startTimeBtwUlti;
    public int ManaPoints;
    private float wizmana;
    public int Mana1;
    public int Mana2;
    public int Mana3;
    private Animator NecroAnim;
    public Rigidbody2D projectile;
    public Transform Launcher;
    public Transform Launcher2;
    public Transform ShotPoint;
    public Transform ShotPoint2;
    private int A;
    private float projectileSpeed = 100f;
    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        A = 0;
        transform.rotation = Quaternion.identity;
        wizmana = this.GetComponent<NecroMove>().Mana;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        float Hor = Input.GetAxis("Horizontal");
        ShootingLeft(Hor);
        wizmana = this.GetComponent<NecroMove>().Mana;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Launcher 1
        Vector2 launcherPos = Launcher.position;
        Vector2 lookDir = mousePos - launcherPos;
        Launcher.right = lookDir;
        // Launcher 2
        Vector2 launcher2Pos = Launcher.position;
        Vector2 lookDir2 = mousePos - launcherPos;
        Launcher2.right = lookDir2;
        //CheckForMainSpam
        if (timeBtwShots <= 0)
        {
            if (wizmana >= Mana1)
            {
                //E Press
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
                {
                    A ++;
                    if(A % 2 == 0)
                    {
                        //Create Projectile
                        Rigidbody2D projectileInstance;
                        projectileInstance = Instantiate(projectile, ShotPoint.position, ShotPoint.rotation) as Rigidbody2D;
                        projectileInstance.GetComponent<Rigidbody2D>().velocity = ShotPoint.right * projectileSpeed;                       
                    }
                    else
                    {
                        //Create Projectile
                        Rigidbody2D projectileInstance;
                        projectileInstance = Instantiate(projectile, ShotPoint2.position, ShotPoint2.rotation) as Rigidbody2D;
                        projectileInstance.GetComponent<Rigidbody2D>().velocity = ShotPoint2.right * projectileSpeed;
                    }
                    //Restart
                    ManaPoints = Mana1;
                    SendMessageUpwards("ManaDrain", ManaPoints);
                    timeBtwShots = startTimeBtwShots;
                    
                }                
            }
        }
        else
        {
            //Decrease Over Time
            timeBtwShots -= Time.deltaTime;
        }
        

        //CheckForHomeSpam
        if (timeBtwHomes <= 0)
        {
            if (wizmana >= Mana2)
            {
                //E Press
                if (Input.GetMouseButtonDown(0))
                {
                    //Create Projectile
                    
                    Instantiate(Ring, RingPoint.position, transform.rotation);
                    ManaPoints = Mana2;
                    SendMessageUpwards("ManaDrain", ManaPoints);
                    //Restart
                    timeBtwHomes = startTimeBtwHomes;
                }
            }
        }
        else
        {
            //Decrease Over Time
            timeBtwHomes -= Time.deltaTime;
        }
        //CheckForHomeSpam
        if (timeBtwUlti <= 0)
        {
            if (wizmana >= Mana3)
            {
                //E Press
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    Instantiate(Ulti, UltiPoint.position, transform.rotation);
                    ManaPoints = Mana3;
                    SendMessageUpwards("ManaDrain", ManaPoints);
                    //Restart
                    timeBtwUlti = startTimeBtwUlti;
                }
            }
        }
        else
        {
            //Decrease Over Time
            timeBtwUlti -= Time.deltaTime;
        }
    }
    private void ShootingLeft(float Hor)
    {
        if (Hor > 0 && !facingRight || Hor < 0 && facingRight)
        {
            facingRight = !facingRight;
            Launcher.Rotate(0f, 180f, 0f);
        }

    }
}