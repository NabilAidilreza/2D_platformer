using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUI : MonoBehaviour
{
    public Canvas Boss;
    public Canvas Player;
    public Camera camTwo;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Boss.worldCamera = camTwo;
            Player.worldCamera = camTwo;

        }
    }
}
