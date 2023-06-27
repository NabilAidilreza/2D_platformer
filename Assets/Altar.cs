using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public GameObject gameover;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z);
            Instantiate(gameover, pos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
