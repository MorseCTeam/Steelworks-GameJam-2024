using System.Collections;
using UnityEngine;

public class EmailSender : MonoBehaviour
{
    [SerializeField] private EmailData startingEmail;
    [SerializeField] private EmailData instructionEmail;

    private EmailSystemController _emailSystemController;

    private void Start()
    {
        _emailSystemController = FindObjectOfType<EmailSystemController>();
        
    }

    public void PerformStartingEmail()
    {
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
