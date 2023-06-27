using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StandardMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public int moveSpd;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float Hor = Input.GetAxis("Horizontal");
        HandleMovement(Hor);
    }
    private void HandleMovement(float Hor)
    {
        rb.velocity = new Vector2(Hor * moveSpd, rb.velocity.y);
    }
}
