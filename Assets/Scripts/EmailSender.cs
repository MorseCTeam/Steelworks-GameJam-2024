using System;
using System.Collections;
using UnityEngine;

public class EmailSender : MonoBehaviour
{
    [SerializeField] private EmailData startingEmail;
    [SerializeField] private EmailData instructionEmail;
    [SerializeField] private EmailData firstBugAppearanceEmail;
    [SerializeField] private EmailData firstBugKillEmail;
    [SerializeField] private EmailData fifthBugKillEmail;
    [SerializeField] private EmailData dwudziesztykaraluchpadwiadomosc;

    private EmailSystemController _emailSystemController;
    private float timer = 0;
    private bool sendFirst = false;

    private void Start()
    {
        _emailSystemController = FindObjectOfType<EmailSystemController>();
        FindObjectOfType<BugsManager>().OnBugKilled += HandleMails;
    }

    private void sendFirstEmail()
    {
        
    }

    public void PerformStartingEmail()
    {
        StartCoroutine(SendEmailUntilSuccess(startingEmail));
    }

    public void SendInstructionEmail()
    {
        StartCoroutine(SendEmailUntilSuccess(instructionEmail));
    }

    public void SendFirstBugAppearanceEmail()
    {
        StartCoroutine(SendEmailUntilSuccess(firstBugAppearanceEmail));
    }

    public void SendFirstBugKillEmail()
    {
        StartCoroutine(SendEmailUntilSuccess(firstBugKillEmail));
    }

    public void SendFifthBugKillEmail()
    {
        StartCoroutine(SendEmailUntilSuccess(fifthBugKillEmail));
    }

    public void WyslijWiadomoscPoDwudziesymKaraluchu()
    {
        StartCoroutine(SendEmailUntilSuccess(dwudziesztykaraluchpadwiadomosc));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SendEmailUntilSuccess(EmailData emailData)
    {
        while (!_emailSystemController.TryToDisplayEmail(emailData))
        {
            yield return null;
        }

        
    }

    public void HandleMails(int kills)
    {
        if (kills == 1)
        {
            SendFirstBugKillEmail();
        }

        if (kills == 5)
        {
            SendFifthBugKillEmail();
        }

        if (kills == 20)
        {
            WyslijWiadomoscPoDwudziesymKaraluchu();
        }
        
    }

    public void WaitForInstructionPerform()
    {
        FindObjectOfType<ButtonsToRobotAdapterController>().OnMovePerformed
            += InstructionPerformanceListener;
    }

    private void InstructionPerformanceListener(int amount)
    {
        if (amount != 9) return;
        
            FindObjectOfType<ButtonsToRobotAdapterController>().OnMovePerformed
                -= InstructionPerformanceListener;
            FindObjectOfType<ScreenSpaceController>().OpenLibraryCameraScreen();
        
    }
}
