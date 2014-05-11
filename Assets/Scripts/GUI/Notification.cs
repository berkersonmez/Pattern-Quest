using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour {
	public static Notification instance;
	private tk2dUIItem u_acceptButton;
	private tk2dTextMesh t_text;
	private System.Action action;
	
	void Awake () {
		instance = this;
		u_acceptButton = transform.Find("NotifBG/AcceptButton").GetComponent<tk2dUIItem>();
		u_acceptButton.OnClick += OnAccept;
		t_text = transform.Find("NotifBG/Text").GetComponent<tk2dTextMesh>();
	}
	
	void Start() {
		gameObject.SetActive(false);
	}
	
	void OnAccept() {
		gameObject.SetActive(false);
		if (action != null) action.Invoke();
	}
	
	public void setText(string text, System.Action action) {
		t_text.text = text;
		t_text.Commit();
		this.action = action;
	}
	
	public static void activate(string text, System.Action actionAfterAccept) {
		instance.gameObject.SetActive(true);
		instance.setText(text, actionAfterAccept);
	}
}
