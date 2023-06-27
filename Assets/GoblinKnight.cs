using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKnight : MonoBehaviour
{
    public float minTime;
    public float maxTime;
    private float time;
    private float ran;
    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(minTime,maxTime);
        ran = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
