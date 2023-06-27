using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex;
    public GameObject UIPlayer;
    public GameObject UIBoss;
    public GameObject CrazedPortal;
    public GameObject SwordPortal;

    // Use this for initialization
    void Start()
    {
        currentCameraIndex = 0;

        //Turn all cameras off, except the first default one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        //If any cameras were added to the controller, enable the first one
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[0].GetComponent<Camera>().name + ", is now enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the c button is pressed, switch to the next camera
        //Set the camera at the current index to inactive, and set the next one in the array to active
        //When we reach the end of the camera array, move back to the beginning or the array.
        if (CrazedPortal.GetComponent<Portal>().Entered == true)
        {
            currentCameraIndex = 1;
            Debug.Log("Changing camera to Crazed");
            cameras[0].gameObject.SetActive(false);
            cameras[currentCameraIndex].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            UIPlayer.GetComponent<Canvas>().worldCamera = cameras[currentCameraIndex];
            UIBoss.GetComponent<Canvas>().worldCamera = cameras[currentCameraIndex];
        }
        if (SwordPortal.GetComponent<Portal>().Entered == true)
        {
            currentCameraIndex = 2;
            Debug.Log("Changing camera to Sword");
            cameras[0].gameObject.SetActive(false);
            cameras[currentCameraIndex].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            UIPlayer.GetComponent<Canvas>().worldCamera = cameras[currentCameraIndex];
            UIBoss.GetComponent<Canvas>().worldCamera = cameras[currentCameraIndex];
        }
    }
}