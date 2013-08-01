using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {

	public int levelNo;
	private tk2dUIItem uiItem;
	
	void Start() {
		uiItem = GetComponent<tk2dUIItem>();
		uiItem.OnClick += OnClick;
	}
	
	void OnClick() {
		loadLevel();
	}
	
	void loadLevel() {
		// Fill "_LevelDescriptor" with dungeon info.
		LevelDescriptor.instance.describeLevel(levelNo);
		Application.LoadLevel("dungeon");
	}
}
