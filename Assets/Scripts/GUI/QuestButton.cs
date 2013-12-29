using UnityEngine;
using System.Collections;

public class QuestButton : MonoBehaviour {

	private tk2dUIItem itemUI;
	private GameObject questParchement;
	private Quest quest;
	private tk2dTextMesh buttonText;

	// Use this for initialization
	void Start () {
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnClick += OnClick;
		questParchement = transform.parent.gameObject;

	}

	public void initialize(Quest quest) {
		this.quest = quest;
		buttonText = transform.Find("ButtonGraphic").Find("Text").GetComponent<tk2dTextMesh>();
		if (transform.name == "AcceptDeclineButton") {
			if (quest.isAccepted()) {
				if (quest.checkCompletion()) {
					buttonText.text = "Complete";
					buttonText.Commit();
				} else {
					buttonText.text = "Abort";
					buttonText.Commit();
				}
			} else {
				buttonText.text = "Accept";
				buttonText.Commit();
			}
		}
	}
	
	void OnClick() {
		if (transform.name == "AcceptDeclineButton") {
			if (quest.isAccepted()) {
				if (quest.checkCompletion()) {
					quest.complete();
				} else {
					quest.decline();
				}
			} else {
				quest.accept();
			}
			Destroy(questParchement);
		} else if (transform.name == "BackButton") {
			Destroy(questParchement);
		}
	}
}
