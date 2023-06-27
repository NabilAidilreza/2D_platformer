using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private int scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene);
        }
    }

}
