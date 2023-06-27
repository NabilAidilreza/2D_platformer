using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WizMove : MonoBehaviour
{
    public Slider healthbar;
    public Slider manabar;

    // Console Variables
    private Rigidbody2D rb;

    private Animator Myanimator;

    // Horizontal Movement Variables
    public float moveSpd;

    private bool facingRight;

    // Jump Variables

    private bool isGrounded;
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;
    public float jumpForce;

    // Health and Damage
    public float Health;
    private int MaxHealth;
    public float Mana;
    private int MaxMana;
    public float RegenRate;
    //Special Abilities

    //Effects
    public GameObject Burst;
    private Scene scene;



    void Start()
    {
        //START VARIABLES
        rb = GetComponent<Rigidbody2D>();
        Myanimator = GetComponent<Animator>();
        facingRight = true;
        extraJumps = extraJumpValue;
        MaxHealth = 60;
        Health = MaxHealth;
        MaxMana = 100;
        Mana = MaxMana;
        scene = SceneManager.GetActiveScene();
        

    }

    void FixedUpdate()
    {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        if (Mana > MaxMana)
        {
            Mana = MaxMana;
        }
        if(Health >= 0)
        {
            Mana += RegenRate * 10 * Time.deltaTime;
            Health += RegenRate * Time.deltaTime;
            Debug.Log(Mana);
        }
        float Hor = Input.GetAxis("Horizontal");
        // UPDATES
        HandleMovement(Hor);
        Flip(Hor);
        Jump();
        Jump();
        Force();
        ForceField();
        healthbar.value = Health;
        manabar.value = Mana;
        if (Health < -40)
        {
            SceneManager.LoadScene(scene.name);
        }
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(Health <= 0)
        {
            rb.velocity = new Vector2(0, 0);
        }
        
    }

    private void HandleMovement(float Hor)
    {
        //Protection Speed
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(Hor * moveSpd * 1 / 2, rb.velocity.y);
            //Myanimator.SetFloat("Speed", Mathf.Abs(Hor));
        }

        //Normal Speed
        else
        {
            rb.velocity = new Vector2(Hor * moveSpd, rb.velocity.y);
           // Myanimator.SetFloat("Speed", Mathf.Abs(Hor));
        }


    }

    //Flip
    private void Flip(float Hor)
    {
        if (Hor > 0 && !facingRight || Hor < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;


        }
    }

    //Jump && Double Jump
    private void Jump()
    {
        if (isGrounded == true)
        {
            
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0)

        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;

        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;

        }
    }


    //ANIMATIONS
    private void Force()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Myanimator.SetBool("Force", true);
        }
        else
        {
            Myanimator.SetBool("Force", false);
        }
    }
    private void ForceField()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Myanimator.SetBool("ForceField", true);
        }
        else
        {
            Myanimator.SetBool("ForceField", false);
        }
    }


    ///ANIMATIONS/////////////
    public void PlayerDamage(int damage)
    {
        //TakeDamage
        if (Input.GetKey(KeyCode.S) && Mana >= 0)
        {
            Mana -= damage;

            Debug.Log("Mana Hit");
        }
        else if (Input.GetKey(KeyCode.S) && Mana <= 0)
        {
            Health -= damage;

            Debug.Log("Health Hit");
        }
        else
        {
            Health -= damage;

            Debug.Log("No Mana Hit");
        }

    }
    public void ManaDrain(int ManaPoints)
    {
        Mana -= ManaPoints;
    }
    public void Heal(int HealPoints)
    {
        Health += HealPoints;
    }
    public void ManaHeal(int ManaPoints)
    {
        Mana += ManaPoints;
    }
}