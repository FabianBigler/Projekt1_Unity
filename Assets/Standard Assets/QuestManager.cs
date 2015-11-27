using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestManager {
    private Text questTxt;
    private Dictionary<int, string> quests;

    public QuestManager(Text questText)
    {
        this.questTxt = questText;
        quests = new Dictionary<int, string>();
        quests.Add(1, "Find the house.");
        quests.Add(2, "The door is locked. Find the key!");
        quests.Add(3, "You found the key! Enter the house.");
        quests.Add(4, "Find your daughter Kelly!");
        quests.Add(5, "You found the {0} key! Go find the {0} room!");
    }

	// Update is called once per frame
	void Update () {
	}

    public void LoadQuest(int questID)
    {
        questTxt.text = quests[questID];
    }

    public void LoadQuest(int questID, string key)
    {
        questTxt.text = string.Format(quests[questID], key);
    }
}
