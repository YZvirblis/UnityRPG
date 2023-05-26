using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Dictionary<string, Item> database = new Dictionary<string, Item>();
    #region Singleton

    public static ItemDatabase instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Item database found!");
            return;
        }

        instance = this;
    }

    #endregion

    private void Start()
    {
        BuildDatabase();
    }

    private void BuildDatabase()
    {
        Debug.Log("Initiating item database");

        Object[] items = Resources.LoadAll("Items", typeof(Item));
        foreach (Object i in items)
        {
            if (
                i != null)
            {
                Debug.Log(i.name);
                database.Add(i.name, (Item)i);
            }
        }

        Object[] equipment = Resources.LoadAll("Equipment", typeof(Item));
        foreach (Object i in equipment)
        {
            if (i != null)
            {

                Debug.Log(i.name);
                database.Add(i.name, (Item)i);
            }
        }
    }
}
