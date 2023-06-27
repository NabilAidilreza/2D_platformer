using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleport : MonoBehaviour
{
    private float WT;
    public float SWT;

    public Transform MoveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    void Start()
    {
        WT = SWT;
        MoveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (WT <= 1)
        {
            MoveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            transform.position = MoveSpot.position;
            WT = SWT;
        }
        else
        {
            WT -= Time.deltaTime;
        }
    }
}
