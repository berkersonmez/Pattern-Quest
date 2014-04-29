using UnityEngine;
using System.Collections;

public class AvatarChoice : MonoBehaviour {
	
	private tk2dSprite s_avatar;
	private tk2dSprite s_border;
	private int id;
	
	void Awake() {
		s_avatar = transform.Find("Avatar").GetComponent<tk2dSprite>();
		s_border = GetComponent<tk2dSprite>();
		GetComponent<tk2dUIItem>().OnClick += OnClick;
	}
	
	public void initialize(int id) {
		this.id = id;
	}
	
	void OnClick() {
		select ();
	}
	
	public void select() {
		AvatarSelection.instance.resetChoiceVisuals();
		AvatarSelection.instance.select(id);
		visualiseSelect();
	}
	
	public void visualiseSelect() {
		s_border.color = Color.yellow;
	}
	
	public void visualiseUnselect() {
		s_border.color = Color.white;
	}
	
	public string getAvatar() {
		return s_avatar.CurrentSprite.name;
	}
}
