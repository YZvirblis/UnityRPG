using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShadyMonsters : Quest
{
    void Start()
    {
        QuestName = "Shady Monsters";
        Description = "Shady guy asked me to kill those monsters over there.";
        // Create Item DB and pull a reward to give
        ItemReward = new List<Item>
        {
            ItemDatabase.instance.database["Gold"]
        };
        Goals.Add(new KillGoal(this, "b67e", "kill enemies", false, 0, 1));
        QuestGiver = GameObject.Find("Quest Giver");

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
