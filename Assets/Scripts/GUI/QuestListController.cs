using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestListController : MonoBehaviour {
	public static QuestListController instance;

	public GameObject prefabQuest;
	public GameObject questList;

	void Start () {
		instance = this;
		questList = transform.Find("QuestsList").gameObject;
		
		refreshQuestList();
	}

	public void refreshQuestList() {
		foreach(Transform child in questList.transform) {
			Destroy(child.gameObject);
		}
		Dictionary<int, Dictionary<string, int>> quests = GameSaveController.instance.player.questSlayCounter;
		int i = 0;
		foreach (KeyValuePair<int, Dictionary<string, int>> pair in quests) {
			Quest quest = TownController.instance.quests[pair.Key];
			GameObject questEntry = Instantiate(prefabQuest) as GameObject;
			questEntry.transform.parent = questList.transform;
			questEntry.transform.localPosition = new Vector3(-9.5f, 14.5f - (4.3f * i), 0f);
			QuestInfoButton holder = questEntry.transform.Find("QuestInfo").GetComponent<QuestInfoButton>();
			holder.setQuest(quest);
			i++;
		}
	}


}
