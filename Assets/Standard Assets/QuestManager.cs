using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {
    public Text QuestText;
    
    private Dictionary<int, string> quests;
    // Use this for initialization
	void Start () {
        quests = new Dictionary<int, string>();
        quests.Add(1, "Find the house.");
        quests.Add(2, "The door is locked. Find the key to open the house!");
        quests.Add(3, "You found the key!");
        quests.Add(4, "Find your daughter Kelly!");
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void LoadQuest(int questID)
    {
        QuestText.text = quests[questID];
    }
}
