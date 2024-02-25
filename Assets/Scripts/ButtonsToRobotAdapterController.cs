using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonsToRobotAdapterController : MonoBehaviour
{
    
    public ActionButton CurrentActionButton = ActionButton.None;

    public event Action OnAttackPerformed;
    public event Action<int> OnMovePerformed;
    public event Action<DirectionButton> OnTurnPerformed;
    
    public void PressActionButton(int actionButtonType)
    {
        CurrentActionButton = (ActionButton)actionButtonType;
        if(CurrentActionButton == ActionButton.Attack) OnAttackPerformed?.Invoke();;
    }

    public void PressNumpad(int value)
    {
        if(CurrentActionButton != ActionButton.Move) return;
        
        Debug.Log("Invoke: "+value);
        OnMovePerformed?.Invoke(value);
        ClearData();
    }

    public void PressDirection(int directionButton)
    {
        if(CurrentActionButton != ActionButton.Rotate) return;
        
        OnTurnPerformed?.Invoke((DirectionButton)directionButton);
        ClearData();
    }

    public void ClearData()
    {
        CurrentActionButton = ActionButton.None;
    }
}

public enum ActionButton
{
    None,
    Move=1,
    Rotate=2,
    Attack=3
}

public enum DirectionButton
{
    None,
    Up=1,
    Down=2,
    Left=3,
    Right=4
}