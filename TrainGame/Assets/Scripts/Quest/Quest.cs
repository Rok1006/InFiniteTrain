using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest : ScriptableObject
{
   [System.Serializable]
   public struct Info
    {
        public string name;
        public string description;
        public bool mainQuest;

    }

    [Header("Info")] public Info Information;
    [System.Serializable]
    public struct Stat
    {
        public ScriptableObject reward;
    }
    [Header("Reward")] public Stat Reward;

    public bool completed { get; protected set; }
    public QuestCompletedEvent questCompleted;

    public abstract class QuestGoal : ScriptableObject
    {
        protected string description;
        public int currentAmount { get; protected set; }
        public int requiredAmount;
        public bool completed { get; protected set; }
        [HideInInspector] public UnityEvent goalCompleted;

        public virtual string GetDescription()
        {
            return description;
        }
        public virtual void Initialize()
        {
            completed = false;
        }
    }

    public List<QuestGoal> Goals;
}

public class QuestCompletedEvent : UnityEvent<Quest> { }
