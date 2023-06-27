using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SwitchPlayers : MonoBehaviour
{
    public GameObject Knight;
    public GameObject Wizard;
    public GameObject Necro;
    public CinemachineVirtualCamera vcam;
    int characterselect;
    // Start is called before the first frame update
    void Start()
    {
        characterselect = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if(characterselect == 1)
        {
            vcam.Follow = Knight.transform;
        }
        if (characterselect == 2)
        {
            vcam.Follow = Wizard.transform;
        }
        if (characterselect == 3)
        {
            vcam.Follow = Necro.transform;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (characterselect == 1)
            {
                characterselect = 2;
            }
            else if (characterselect == 2)
            {
                characterselect = 3;
            }
            else if (characterselect == 3)
            {
                characterselect = 1;
            }
        }
        if (characterselect == 1)
        {
            Knight.SetActive(true);
            Wizard.SetActive(false);
            Necro.SetActive(false);
            Wizard.transform.position = Knight.transform.position;
            Necro.transform.position = Knight.transform.position;
        }
        else if (characterselect == 2)
        {
            Knight.SetActive(false);
            Wizard.SetActive(true);
            Necro.SetActive(false);
            Knight.transform.position = Wizard.transform.position;
            Necro.transform.position = Wizard.transform.position;
        }
        else if (characterselect == 3)
        {
            Knight.SetActive(false);
            Wizard.SetActive(false);
            Necro.SetActive(true);
            Knight.transform.position = Necro.transform.position;
            Wizard.transform.position = Necro.transform.position;
        }
    }
}
