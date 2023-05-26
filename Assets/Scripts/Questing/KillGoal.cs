using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public string TargetID { get; set; }

    public KillGoal(Quest quest, string targetID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.TargetID = targetID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        CombatEvents.onTargetDeath += TargetDied;

    }

    void TargetDied(CharacterStats target)
    {
        if (target.ID == this.TargetID)
        {
            this.CurrentAmount++;
            Evaluate();
            Quest.CheckGoals();
        }
    }
}
