using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailSender : MonoBehaviour
{
    [SerializeField] private EmailData startingEmail;
    [SerializeField] private EmailData instructionEmail;

    private EmailSystemController _emailSystemController;

    private void Start()
    {
        _emailSystemController = FindObjectOfType<EmailSystemController>();
        StartCoroutine(SendEmailUntilSuccess(startingEmail));
    }

    public void SendInstructionEmail()
    {
        StartCoroutine(SendEmailUntilSuccess(instructionEmail));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SendEmailUntilSuccess(EmailData emailData)
    {
        while (!_emailSystemController.TryToDisplayEmail(emailData))
        {
            yield return null;
        }
    }

}
