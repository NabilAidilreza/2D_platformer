using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public bool IsPaused;
    public bool Restart;
    public GameObject Pausemenu;
    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPaused)
        {
            Pausemenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Pausemenu.SetActive(false);
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
        }
        if (Restart)
        {
            SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            Restart = !Restart;
        }
    }
}
