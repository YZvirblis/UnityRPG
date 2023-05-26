using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Ink.UnityIntegration;


public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    //[SerializeField] private TextAsset inkJSON;
    public Story story;

    [Header("Globals Ink File")]
    [SerializeField] private InkFile globalsInkFile;

    [Header("UI Refernce")]
    [SerializeField] private GameObject dialogueText;
    [SerializeField] private GameObject choiceButton;
    [SerializeField] private GameObject dialogueContentWrapper;
    [SerializeField] private GameObject continuePointerPrefab;
    [SerializeField] private GameObject dialoguePanel;

    [Header("Quests")]
    [SerializeField] GameObject questsGameobject;

    private const string SPEAKER_TAG = "Speaker";
    private const string ACCEPT_QUEST_TAG = "Accept";
    private const string COMPLETE_QUEST_TAG = "Complete";

    public DialogueVariables dialogueVariables;

    public bool dialogueIsPlaying { get; private set; }



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
        }
        else if (instance == this)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in scene");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        //dialogueContentWrapper = GameObject.Find("DialogueContent");
        //dialoguePanel = GameObject.Find("DialoguePanel");
        dialoguePanel.SetActive(false);
    }
    private void Update()
    {
        if (dialogueIsPlaying)
        {
            bool clicked = Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.F);
            if (clicked && !story.canContinue && story.currentChoices.Count <= 0)
            {
                ExitDialogue();
            }
            if (clicked && story.canContinue)
            {
                UpdateDialogueUI();
            }

        }

    }

    private void UpdateDialogueUI()
    {
        List<GameObject> newDialogue = new List<GameObject>();

        foreach (Transform child in dialogueContentWrapper.transform)
        {
            if (child.gameObject.name == "ChoiceButton(Clone)")
            {
                Destroy(child.gameObject);
            }
        } // Destroy answer buttons after each choice\advancement in dialogue to clean up dialogue panel.

        GameObject textObj = Instantiate(dialogueText);

        string text = LoadStoryChunk();
        List<string> tags = story.currentTags;

        if (tags.Count > 0)
        {
            foreach (string tag in tags)
            {
                string[] splitTag = tag.Split(":");
                if (splitTag.Length != 2) { Debug.LogError("Tag could not be appropriately parsed: " + tag); }
                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();

                // Handle the tag
                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        text = "<b>" + GetTextColorByTag(tagValue)[0] + tagValue + GetTextColorByTag(tagValue)[1] + "</b> - " + text; // First tag is always name and we make it bold before adding it to the initial text.
                        break;
                    case ACCEPT_QUEST_TAG:
                        // INITIATE ACCEPT QUEST
                        Debug.Log("ACCEPTING: " + tagValue);
                        Quest quest = (Quest)questsGameobject.AddComponent(System.Type.GetType(tagValue));
                        break;
                    case COMPLETE_QUEST_TAG:
                        QuestEvents.CompleteQuest((Quest)GameObject.Find("Quests").GetComponent(System.Type.GetType(tagValue)));
                        break;
                }
            }
        }


        textObj.GetComponent<TextMeshProUGUI>().text = text;
        textObj.transform.SetParent(dialogueContentWrapper.transform, false);
        newDialogue.Add(textObj);

        foreach (Choice choice in story.currentChoices)
        {
            GameObject btnObj = Instantiate(choiceButton);
            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            btnObj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                MakeChoice(choice);
            });
            btnObj.transform.SetParent(dialogueContentWrapper.transform, false);
            newDialogue.Add(btnObj);
        }

        if (story.canContinue)
        {
            UpdateDialogueUI();
        }

        dialogueContentWrapper.GetComponent<VerticalLayoutGroup>().padding.bottom = Screen.height / 2;
        StartCoroutine(DelaySetTextHeight(newDialogue));
    }
    private IEnumerator DelaySetTextHeight(List<GameObject> newDialogue)
    {
        yield return (0);
        float totalHeight = 0;
        foreach (GameObject item in newDialogue)
        {
            if (item != null)
            {
                totalHeight += item.GetComponent<RectTransform>().rect.height;
            }
        }
        dialogueContentWrapper.transform.Translate(new Vector3(0, totalHeight, 0));
    }

    void ExitDialogue()
    {
        dialogueVariables.StopListening(story);
        foreach (Transform child in dialogueContentWrapper.transform)
        {
            Destroy(child.gameObject);
        }
        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;
    }

    void MakeChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        UpdateDialogueUI();
    }

    string LoadStoryChunk()
    {
        string text = "";
        if (story.canContinue)
        {
            text = story.Continue();
        }
        return text;
    }

    private List<string> GetTextColorByTag(string tag)
    {
        string o = "<color="; // Opening color tag.
        string c = "</color>"; // Closing color tag.
        switch (tag) // Character tags color pallet where in case of "Character" we add a HEX color to the openning tag.
        {
            case "Quest Giver":
                o += "#3d8ed1>";
                break;
            case "Horny Guy":
                o += "#03fc0b>";
                break;
            case "Shady Guy":
                o += "#231ea8>";
                break;
            case "Hot Girl":
                o += "#fc1ef1>";
                break;
            default:
                o += "white>";
                break;
        }
        return new List<string> { o, c }; // Return of openning and closing tags
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        story = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        dialogueVariables.StartListening(story);
        dialogueContentWrapper.GetComponent<VerticalLayoutGroup>().padding.top = Screen.height / 2;
        UpdateDialogueUI();
    }
}
