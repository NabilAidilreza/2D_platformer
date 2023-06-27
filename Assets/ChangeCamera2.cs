using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera2 : MonoBehaviour
{
    public GameObject cameraOne;
    public GameObject cameraTwo;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraTwo.SetActive(true);
            cameraOne.SetActive(false);

        }
    }
}
