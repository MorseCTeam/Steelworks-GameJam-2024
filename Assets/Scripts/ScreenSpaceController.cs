using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceController : MonoBehaviour
{
    [SerializeField] private GameObject EmailScreen;
    [SerializeField] private GameObject LibraryCameraScreen;

    public void OpenEmail()
    {
        EmailScreen.SetActive(true);
        LibraryCameraScreen.SetActive(false);
    }

    public void OpenLibraryCameraScreen()
    {
        EmailScreen.SetActive(false);
        LibraryCameraScreen.SetActive(true);
    }

    public void ShutDown()
    {
        EmailScreen.SetActive(false);
        LibraryCameraScreen.SetActive(false);
    }
}
