using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JournalUI : MonoBehaviour
{
    public GameObject journalUI;  // The entire UI
    [SerializeField] GameObject questItemPrefab;
    [SerializeField] GameObject questListItems;
    [SerializeField] GameObject questDetailsContent;

    void Start()
    {
        //questList = QuestList.GetInstance();
        //questManager = QuestManager.GetInstance();
        //questManager.onQuestsChangedCallback += AddQuestToUI;    // Subscribe to the onItemChanged callback
        QuestEvents.onQuestAccepted += AddQuestToUI;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            journalUI.SetActive(!journalUI.activeSelf);
        }
    }

    void AddQuestToUI(Quest q)
    {
        GameObject newQuestItem = Instantiate(questItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newQuestItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = q.QuestName;
        newQuestItem.transform.SetParent(questListItems.transform);
        newQuestItem.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                SelectQuest(q);
            });
        newQuestItem.gameObject.name = q.QuestName;
    }

    public void SelectQuest(Quest q)
    {
        questDetailsContent.transform.Find("QuestTitle").GetComponent<TextMeshProUGUI>().text = q.QuestName;
        questDetailsContent.transform.Find("QuestDescription").GetComponent<TextMeshProUGUI>().text = q.Description;
    }
}
