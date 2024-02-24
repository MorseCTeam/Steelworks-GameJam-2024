using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EmailSystemController : MonoBehaviour
{
    public bool IsBusy => _currentEmail != null;

    [SerializeField] private TextMeshProUGUI emailDisplay;

    [SerializeField] private EmailData EmailTest; 
    private EmailData _currentEmail = null;
    private AudioController audioController;

    private void Start()
    {
        TryToDisplayEmail(EmailTest);
        audioController = FindObjectOfType<AudioController>();
    }

    public bool TryToDisplayEmail(EmailData emailData)
    {
        if (IsBusy) return false;

        _currentEmail = emailData;
        StartCoroutine(DisplayCurrentEmail());
        return true;
    }

    private IEnumerator DisplayCurrentEmail()
    {
        _currentEmail.StartEmailAction.Invoke();
        emailDisplay.text = "";
        string tag = "<color=#00000000>";
        int i = 0;
        bool searchForEndOfTag = false;
        
        while (i < _currentEmail.EmailText.Length)
        {
            if (_currentEmail.EmailText[i] == '<')
            {
                searchForEndOfTag = true;
            }

            if (i > 0 && _currentEmail.EmailText[i-1] == '>')
            {
                searchForEndOfTag = false;
            }
            

            emailDisplay.text =
                _currentEmail.EmailText.Substring(0, i)
                + tag
                + _currentEmail.EmailText.Substring(i);
            
            i++;

            audioController.Play(SoundType.KeyBoardBeep);
            
            if(searchForEndOfTag)
            {
                continue;
            }
            
            yield return new WaitForSeconds(_currentEmail.LetterLength);
        }
        emailDisplay.text = _currentEmail.EmailText;

        _currentEmail.EndEmailAction.Invoke();
        _currentEmail = null;
    }

}

[Serializable]
public class EmailData
{
    [field: SerializeField] public UnityEvent StartEmailAction { get; private set; }
    [field: SerializeField] public UnityEvent EndEmailAction { get; private set; }
    [field: SerializeField, TextArea] public string EmailText { get; set; }
    [field: SerializeField] public float LetterLength { get;  set; }
}