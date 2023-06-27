using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElipsePlatform : MonoBehaviour
{
    public Transform rotationCenter;

    public float rotationRadius; 
    public float angularSpeed;

    float posX, posY = 0f;
    public float angle;

    // Update is called once per frame
    void Update()
    {
        posX = rotationCenter.position.x + (Mathf.Cos(angle) * rotationRadius);
        posY = rotationCenter.position.y + (Mathf.Sin(angle) * rotationRadius);
        transform.position = new Vector2(posX, posY);
        angle = angle + (Time.deltaTime * angularSpeed);

        if (angle >= 360f)
            angle = 0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.collider.transform.parent.SetParent(transform);
        }
            
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.parent.SetParent(null);
    }
}