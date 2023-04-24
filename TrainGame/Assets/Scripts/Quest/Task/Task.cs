using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    [Header("Text")]
    [SerializeField]
    private string codeName;
    [SerializeField]
    private string description;
    [Header("Action")]
    [SerializeField]
    private TaskAction action;

    [Header("Setting")]
    [SerializeField]
    private int needSuccessToComplete;

    public int CurrentSuccess { get; private set; }
    public int NeedSuccessToComplete => needSuccessToComplete;
    public string CodeName => codeName;
    public string Description => description;

  
    public void RecieveReport(int successCount)
    {
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
    }
}
