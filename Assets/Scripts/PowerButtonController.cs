using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerButtonController : MonoBehaviour
{
   [SerializeField] private Sprite turnedOffSprite;
   [SerializeField] private Sprite turnedOnSprite;
   private Image _image;
   private GameObject buzzObject;
   private bool _turnedOn;
   private void Awake()
   {
      _image = GetComponent<Image>();
   }

   public void TryToToggle()
   {
      if (_turnedOn == false)
      {
         FindObjectOfType<AudioController>().Play(SoundType.ComputerStartup, volume: 0.4f); 
         buzzObject = FindObjectOfType<AudioController>().Play(SoundType.ComputerBuzz, loop: true).gameObject;
         FindObjectOfType<EmailSender>().PerformStartingEmail();
         FindObjectOfType<ScreenSpaceController>().OpenEmail();
         _image.sprite = turnedOnSprite;
         _turnedOn = true;
      }

      if (FindObjectOfType<ScreenSpaceController>().CurrentScreenSpace == ScreenSpace.ShutDown)
      {
         Destroy(buzzObject);
         _image.sprite = turnedOffSprite;
         FindObjectOfType<AudioController>().Play(SoundType.OnOffSwitchClick);
         FindObjectOfType<ScreenSpaceController>().TurnOff();
         StartCoroutine(FindObjectOfType<FaderController>().Fade(FadeType.FadeOut, 2f, () =>
         {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
         }));
      }
   }
}
