using UnityEngine;
using System.Collections;

public class FleeButton : MonoBehaviour {

	void Start() {
		GetComponent<tk2dUIItem>().OnClick += OnClick;
	}
	
	void OnClick() {
		GameSaveController.instance.saveGame();
		Application.LoadLevel("main");
	}
}
