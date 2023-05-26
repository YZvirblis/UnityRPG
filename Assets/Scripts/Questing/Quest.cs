using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string ID { get; set; }
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExpReward { get; set; }
    public List<Item> ItemReward { get; set; }
    public bool Completed { get; set; }
    public GameObject QuestGiver { get; set; }

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        if (Completed)
        {
            QuestEvents.ReadyToCompleteQuest(this);
        }
    }

    void GiveReward(Quest q)
    {
        if (q.ID == ID)
        {
            if (ItemReward != null)
            {
                Debug.Log("Rewarded an Item");
                ItemReward.ForEach(i =>
                {
                    Inventory.instance.Add(i);
                });
            }
            // Update Journal UI
        }
    }

    public virtual void Init()
    {
        // default init stuff.
        QuestEvents.onQuestCompleted += GiveReward;

    }

}
