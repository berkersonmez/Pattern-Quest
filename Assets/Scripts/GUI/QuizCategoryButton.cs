using UnityEngine;
using System.Collections;

public class QuizCategoryButton : MonoBehaviour {
	
	[HideInInspector]
	public string category;
	
	private tk2dUIItem u_button;
	private tk2dTextMesh t_text;
	
	void Awake () {
		u_button = GetComponent<tk2dUIItem>();
		u_button.OnClick += OnClick;
		t_text = transform.Find("ButtonGraphic/Text").GetComponent<tk2dTextMesh>();
	}
	
	void OnClick() {
		TownMenu.instance.quizWindow();
		QuizController.instance.startQuiz(category);
	}
	
	public void setCategory(string category) {
		this.category = category;
		t_text.text = category;
		t_text.Commit();
	}
}
