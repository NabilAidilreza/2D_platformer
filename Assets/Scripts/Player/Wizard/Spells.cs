using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    //Arrow Attack Variables
    public Rigidbody2D Laser;
    public GameObject ForceBall;
    public Transform ballPoint;
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
    public Rigidbody2D projectile;
    public Transform Launcher;
    private float projectileSpeed = 10f;
    public Transform ShotPoint;
    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;
        wizmana = this.GetComponent<WizMove>().Mana;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        float Hor = Input.GetAxis("Horizontal");
        ShootingLeft(Hor);
        wizmana = this.GetComponent<WizMove>().Mana;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Launcher 1
        Vector2 launcherPos = Launcher.position;
        Vector2 lookDir = mousePos - launcherPos;
        Launcher.right = lookDir;
        //CheckForMainSpam
        if (timeBtwShots <= 0)
        {
            if(wizmana >= Mana1)
            {
                //E Press
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
                {
                    //Create Projectile
                    Rigidbody2D projectileInstance;
                    projectileInstance = Instantiate(projectile, ShotPoint.position, ShotPoint.rotation) as Rigidbody2D;
                    projectileInstance.GetComponent<Rigidbody2D>().velocity = ShotPoint.right * projectileSpeed;
                    ManaPoints = Mana1;
                    SendMessageUpwards("ManaDrain", ManaPoints);
                    //Restart
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
                if (Input.GetMouseButton(0))
                {
                    //Create Projectile
                    Rigidbody2D projectileInstance;
                    projectileInstance = Instantiate(Laser, ShotPoint.position, ShotPoint.rotation) as Rigidbody2D;
                    projectileInstance.GetComponent<Rigidbody2D>().velocity = ShotPoint.right * (projectileSpeed+90);
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
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Instantiate(ForceBall, ballPoint.position, transform.rotation);
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
