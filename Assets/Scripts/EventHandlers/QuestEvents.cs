using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    public delegate void QuestEventHandler(Quest quest);
    public static event QuestEventHandler onQuestAccepted;
    public static event QuestEventHandler onQuestReadyToComplete;
    public static event QuestEventHandler onQuestCompleted;

    public static void AcceptQuest(Quest quest)
    {
        if (onQuestAccepted != null)
        {
            onQuestAccepted(quest);
        }
    }
    public static void ReadyToCompleteQuest(Quest quest)
    {
        if (onQuestReadyToComplete != null)
        {
            onQuestReadyToComplete(quest);
        }
    }
    public static void CompleteQuest(Quest quest)
    {
        if (onQuestCompleted != null)
        {
            onQuestCompleted(quest);
        }
    }
}
