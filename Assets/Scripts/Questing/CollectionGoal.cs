using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGoal : Goal
{
    public string TargetID { get; set; }

    public CollectionGoal(Quest quest, string targetID, string description, bool completed, int currentAmount, int requiredAmount)
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
        InventoryEvents.onItemPickedUp += PickedUpItem;
        InventoryEvents.onItemDropped += DroppedItem;
        CurrentAmount += Inventory.instance.GetCapacityByID(this.TargetID);
    }

    void PickedUpItem(Item item)
    {
        if (item.ID == this.TargetID)
        {
            this.CurrentAmount++;
            Evaluate();
            Quest.CheckGoals();
        }
    }
    void DroppedItem(Item item)
    {
        if (item.ID == this.TargetID)
        {
            this.CurrentAmount--;
            Evaluate();
            Quest.CheckGoals();
        }
    }
}
