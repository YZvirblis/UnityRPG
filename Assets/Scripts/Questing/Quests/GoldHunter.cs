using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldHunter : Quest
{
    void Start()
    {
        QuestName = "Gold Hunter";
        Description = "That dude needs me to get some gold. I should go and find a piece of gold to give him.";
        ItemReward = null;
        Goals.Add(new CollectionGoal(this, "374hg", "collect gold", false, 0, 2));
        QuestGiver = GameObject.Find("Gold Hunter");

        QuestEvents.onQuestReadyToComplete += ReadyToComplete;
        Goals.ForEach(g => g.Init());
        Init();
    }

    public override void Init()
    {
        base.Init();
        QuestEvents.AcceptQuest(this);

    }

    void ReadyToComplete(Quest q)
    {
        if (q.ID == ID)
        {
            string name = string.Join(null, QuestName.Split(' '));
            DialogueManager.GetInstance().story.variablesState[name] = 2;
            Ink.Runtime.Object v = DialogueManager.GetInstance().story.variablesState.GetVariableWithName(name);
            DialogueManager.GetInstance().dialogueVariables.variables[name] = v;
            //QuestGiver.GetComponent<DialogueTrigger>().AdvanceStage();
        }
    }
}
