using UnityEngine;
using System.Collections;

public class QuestInfoButton : MonoBehaviour {

	public Quest quest;
	public tk2dTextMesh text; // Assign using inspector
	private tk2dUIItem itemUI;
	
	void Start () {
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnClick += OnClick;
	}
	
	// Sets spell of the item slot.
	public void setQuest(Quest quest) {
		this.quest = quest;
		if (text == null) {
			text = transform.parent.Find("questName").GetComponent<tk2dTextMesh>();
		}
		text.text = quest.questName;
		if (quest.checkCompletion()) {
			text.color = Color.green;
		} else {
			text.color = Color.red;
		}
		text.Commit();
	}

	void OnClick() {
		quest.setTooltipText();
		GameObject questParchement = Instantiate(quest.questParchementPrefab) as GameObject;
		questParchement.transform.parent = this.transform.parent.parent.parent;
		questParchement.transform.localPosition = new Vector3(0f, 0f, -1f);
		tk2dTextMesh parchementText = questParchement.transform.Find("Text").GetComponent<tk2dTextMesh>();
		parchementText.text = quest.getTooltipText();
		parchementText.Commit();
		QuestButton qButton = questParchement.transform.Find("AcceptDeclineButton").GetComponent<QuestButton>();
		qButton.initialize(quest);
		qButton = questParchement.transform.Find("BackButton").GetComponent<QuestButton>();
		qButton.initialize(quest);
	}

}
