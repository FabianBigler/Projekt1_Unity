using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestManager {
    private Text questTxt;
    private Dictionary<int, string> quests;
    private Dictionary<string, int> keyQuestPair;

    public QuestManager(Text questText)
    {
        this.questTxt = questText;
        quests = new Dictionary<int, string>();
        quests.Add(1, "Find the house were your daughter Kelly might be detained.");
        quests.Add(2, "The door is locked. Find the key!");
        quests.Add(3, "You found the key! Enter the house.");
        quests.Add(4, "Your daughter Kelly must be in one of these rooms. Seems like you have to find the appropriate keys.");
        quests.Add(5, "You found the {0} key! Go find the {0} room!");
        quests.Add(6, "These barrels might be useful in some way...");
        quests.Add(7, "It's dark in here.");
        quests.Add(8, "Theses boxes seem to be movable.");
        quests.Add(9, "Find the right end.");
        quests.Add(10, "Light it up!");
        quests.Add(11, "Maybe there is a hidden treasure.");
        quests.Add(12, "What is this noise?");

        keyQuestPair = new Dictionary<string, int>();
        keyQuestPair.Add("blue", 6);
        keyQuestPair.Add("red", 7);
        keyQuestPair.Add("white", 8);
        keyQuestPair.Add("yellow", 9);
        keyQuestPair.Add("green", 10);
        keyQuestPair.Add("violet", 11);              
        keyQuestPair.Add("master", 12);
    }

	// Update is called once per frame
	void Update () {
	}

    public void LoadQuest(int questID)
    {
        questTxt.text = quests[questID];
    }

    public void LoadKeyQuest(int questID, string key)
    {
        questTxt.text = string.Format(quests[questID], key);
    }

    public void LoadNextKeyQuest(string key)
    {
        questTxt.text = string.Format(quests[keyQuestPair[key]]);
    }
}
