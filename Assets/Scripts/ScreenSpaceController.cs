using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenSpaceController : MonoBehaviour
{
    public ScreenSpace CurrentScreenSpace = ScreenSpace.Camera;
    
    [SerializeField] private GameObject EmailScreen;
    [SerializeField] private GameObject LibraryCameraScreen;
    [SerializeField] private GameObject ShutDownScreen;
    [SerializeField] private GameObject TurnedOffScreen;
    public void OpenEmail()
    {
        EmailScreen.SetActive(true);
        LibraryCameraScreen.SetActive(false);
        ShutDownScreen.SetActive(false);
        TurnedOffScreen.SetActive(false);
        CurrentScreenSpace = ScreenSpace.Email;
    }

    public void OpenLibraryCameraScreen()
    {
        EmailScreen.SetActive(false);
        LibraryCameraScreen.SetActive(true);
        ShutDownScreen.SetActive(false);
        TurnedOffScreen.SetActive(false);
        CurrentScreenSpace = ScreenSpace.Camera;
    }

    public void ShutDown()
    {
        EmailScreen.SetActive(false);
        LibraryCameraScreen.SetActive(false);
        ShutDownScreen.SetActive(true);
        TurnedOffScreen.SetActive(false);
        CurrentScreenSpace = ScreenSpace.ShutDown;
    }

    public void TurnOff()
    {
       
        EmailScreen.SetActive(false);
        LibraryCameraScreen.SetActive(false);
        ShutDownScreen.SetActive(false);
        TurnedOffScreen.SetActive(true);
        CurrentScreenSpace = ScreenSpace.None;
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {
            Destroy(audioSource.gameObject);
        }
    }
}

public enum ScreenSpace
{
    None,
    Email,
    Camera,
    ShutDown
}
