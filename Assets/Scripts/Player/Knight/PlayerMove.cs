using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Slider healthbar;
    public Slider shieldbar;

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
    public int Shield;
    private int MaxShield;
    //Special Abilities

    //Effects
    public GameObject Burst;
    private Scene scene;
    public float RegenRate;
    private float timeBtwSpdUp;
    public float startTimeBtwSpdUp;

    void Start()
    {
        //START VARIABLES
        rb = GetComponent<Rigidbody2D>();
        Myanimator = GetComponent<Animator>();
        facingRight = true;
        extraJumps = extraJumpValue;
        MaxHealth = 100;
        Health = MaxHealth;
        MaxShield = 100;
        Shield = MaxShield;
        Shield = 100;
        scene = SceneManager.GetActiveScene();
        
    }

    void FixedUpdate()
    {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        if(Shield > MaxShield)
        {
            Shield = MaxShield;
        }
        if (Health >= 0)
        {
            
            Health += RegenRate * Time.deltaTime;
            
        }
        float Hor = Input.GetAxis("Horizontal");
        // UPDATES
        HandleMovement(Hor);
        Flip(Hor);
        Jump();
        Jump();
        Crouch();
        IsJumping();
        IsAttacking();
        IsShooting();
        IsDead();
        
        healthbar.value = Health;
        shieldbar.value = Shield;
        if (Health < -40)
        {
            SceneManager.LoadScene(scene.name);
        }
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        
        if (timeBtwSpdUp <= 0)
        {
            //Ultimate
            if (Input.GetKeyDown(KeyCode.Q))
            {
                moveSpd += 10;
                StartCoroutine(SpdUp());
                timeBtwSpdUp = startTimeBtwSpdUp;
            }

        }
        else
        {
            //Decrease Over Time
            timeBtwSpdUp -= Time.deltaTime;
        }

    }
    IEnumerator SpdUp()
    {
        yield return new WaitForSeconds(30);
        moveSpd -= 10;
        
    }
    private void HandleMovement(float Hor)
    {
        //Crouch Speed
        if (Input.GetKey(KeyCode.S)){
            rb.velocity = new Vector2(Hor * moveSpd * 1/2, rb.velocity.y);
            Myanimator.SetFloat("Speed", Mathf.Abs(Hor));
        }
        
        //Normal Speed
        else
        {
            rb.velocity = new Vector2(Hor * moveSpd, rb.velocity.y);
            Myanimator.SetFloat("Speed", Mathf.Abs(Hor));
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
            
        } else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true) {
            rb.velocity = Vector2.up * jumpForce;
            
        }
    }


    //ANIMATIONS


    //Crouch & Def Up
    private void Crouch()
    {
        if(Input.GetKey(KeyCode.S) && isGrounded == true)
        {
            Myanimator.SetBool("IsCrounching", true);
        }
        else
        {
            Myanimator.SetBool("IsCrounching", false);
            
            
        }
    }
    // Jumping Anim
    private void IsJumping()
    {
        if (Input.GetKeyDown(KeyCode.W)){
            Myanimator.SetBool("IsJumping", true);
        } 
        else
        {
            Myanimator.SetBool("IsJumping", false);
        }
    }
    //Sword Attack Anim
    private void IsAttacking()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Myanimator.SetBool("IsAttacking", true);
        }
        else
        {
            Myanimator.SetBool("IsAttacking", false);
        }
    }
    //Bow Attack Anim
    private void IsShooting()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Myanimator.SetBool("IsShooting", true);
            
        }
        else
        {
            Myanimator.SetBool("IsShooting", false);
        }
    }
    //Dead Anim
    private void IsDead()
    {
        if (Health <= 0)
        {
            Myanimator.SetBool("IsDead", true);
            rb.velocity = new Vector2(0, 0);
            
        }
        else
        {
            Myanimator.SetBool("IsDead", false);
        }
    }
    
    public void PlayerDamage(int damage)
    {
        //TakeDamage
        if(Input.GetKey(KeyCode.S) && Shield >= 0)
        {
            Shield -= damage;
            
            Debug.Log("Shield Hit");
        }
        else if(Input.GetKey(KeyCode.S) && Shield <= 0)
        {
            Health -= damage;
            
            Debug.Log("Health Hit");
        }
        else
        {
            Health -= damage;
            
            Debug.Log("No SHield Hit");
        }

    }
    public void Heal(int HealPoints)
    {
        Health += HealPoints;
    }
    public void ShieldHeal(int ShieldPoints)
    {
        Shield += ShieldPoints;
    }
}