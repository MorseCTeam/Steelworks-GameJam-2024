using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceController : MonoBehaviour
{
    public ScreenSpace CurrentScreenSpace = ScreenSpace.Camera;
    
    [SerializeField] private GameObject EmailScreen;
    [SerializeField] private GameObject LibraryCameraScreen;
    [SerializeField] private GameObject ShutDownScreen;
    public void OpenEmail()
    {
        EmailScreen.SetActive(true);
        LibraryCameraScreen.SetActive(false);
        ShutDownScreen.SetActive(false);
        CurrentScreenSpace = ScreenSpace.Email;
    }

    public void OpenLibraryCameraScreen()
    {
        EmailScreen.SetActive(false);
        LibraryCameraScreen.SetActive(true);
        ShutDownScreen.SetActive(false);
        CurrentScreenSpace = ScreenSpace.Camera;
    }

    public void ShutDown()
    {
        EmailScreen.SetActive(false);
        LibraryCameraScreen.SetActive(false);
        ShutDownScreen.SetActive(true);
        CurrentScreenSpace = ScreenSpace.ShutDown;
    }
}

public enum ScreenSpace
{
    None,
    Email,
    Camera,
    ShutDown
}
