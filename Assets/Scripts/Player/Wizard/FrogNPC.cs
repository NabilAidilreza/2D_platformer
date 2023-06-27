using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogNPC : MonoBehaviour
{
    private float movementDuration = 2.0f;
    private float waitBeforeMoving = 2.0f;
    private bool hasArrived = false;
    public Transform Burst;
    private Animator FrogAnim;
    //CheckDir
    public float oldPosition;
    private void Update()
    {
        StartCoroutine(Die());
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
        ////////////////////////////////////
        if (!hasArrived)
        {
            hasArrived = true;
            
            float randX = Random.Range(-5.0f, 5.0f);
            StartCoroutine(MoveToPoint(new Vector3(transform.position.x + randX, transform.position.y, 0)));
        }
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        Instantiate(Burst, transform.position, Quaternion.identity);

    }
    private IEnumerator MoveToPoint(Vector3 targetPos)
    {
        
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;

    }
}