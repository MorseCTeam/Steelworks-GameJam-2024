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
    
    private EmailData _currentEmail = null;
    private AudioController audioController;

    private void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
    }

    private void Start()
    {
        audioController?.Play(SoundType.ComputerStartup);
        audioController?.Play(SoundType.ComputerBuzz, loop: true);
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
        if(audioController == null) audioController = FindObjectOfType<AudioController>();
        FindObjectOfType<ScreenSpaceController>().OpenEmail();
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
            if(searchForEndOfTag)
            {
                continue;
            }
            if(Char.IsLetterOrDigit(_currentEmail.EmailText[i-1])) audioController.Play(SoundType.KeyBoardBeep,volume:0.1f);
            
            yield return new WaitForSeconds(_currentEmail.LetterLength);
        }
        emailDisplay.text = _currentEmail.EmailText;

        yield return new WaitForSeconds(_currentEmail.EndWait);
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
    [field: SerializeField] public float EndWait { get; set; }
}