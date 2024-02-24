using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FaderController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        StartCoroutine(Fade(FadeType.FadeIn));
        
    }

    IEnumerator Fade(FadeType type, float length = 1f, Action OnEndAction = null)
    {
        float alphaValue = type == FadeType.FadeIn ? 1 : 0;
        float direction = type == FadeType.FadeIn ? -1 : 1;
        while (direction > 0 && alphaValue < 1f || direction < 0 && alphaValue > 0f)
        {
            alphaValue += direction / length * Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,alphaValue);
            yield return null;
        }
        OnEndAction?.Invoke();
    }

}



public enum FadeType
{
    FadeIn,
    FadeOut
}