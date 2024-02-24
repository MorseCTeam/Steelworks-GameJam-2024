using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EmailSystemController : MonoBehaviour
{
    public bool IsBusy => _currentEmail != null;

    [SerializeField] private TextMeshProUGUI emailDisplay;

    [SerializeField] private EmailData EmailTest; 
    private EmailData _currentEmail = null;

    private void Start()
    {
        TryToDisplayEmail(EmailTest);
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
        _currentEmail.EmailText = _currentEmail.EmailText.Replace("\\n", "\u000a");
        emailDisplay.text = "";
        string tag = "<color=#00000000>";
        int i = 0;

        while (i < _currentEmail.EmailText.Length)
        {
            emailDisplay.text =
                _currentEmail.EmailText.Substring(0, i)
                + tag
                + _currentEmail.EmailText.Substring(i);

            yield return new WaitForSeconds(_currentEmail.LetterLength);
            i++;
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
    [field: SerializeField] public string EmailText { get; set; }
    [field: SerializeField] public float LetterLength { get; private set; }
}