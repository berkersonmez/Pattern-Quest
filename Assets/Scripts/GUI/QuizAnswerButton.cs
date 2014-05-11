using UnityEngine;
using System.Collections;

public class QuizAnswerButton : MonoBehaviour {

	[HideInInspector]
	public int id;

	private tk2dUIItem u_button;
	private tk2dTextMesh t_text;
	
	void Start () {
		u_button = GetComponent<tk2dUIItem>();
		u_button.OnClick += OnClick;
		t_text = transform.Find("ButtonGraphic/Text").GetComponent<tk2dTextMesh>();
	}
	
	void OnClick() {
		QuizController.instance.answer(id);
	}
	
	public void setChoice(int choiceID, string choiceText) {
		id = choiceID;
		t_text.text = choiceText;
		t_text.Commit();
	}
}
