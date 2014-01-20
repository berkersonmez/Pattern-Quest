using UnityEngine;
using System.Collections;

public class TalentResetButton : MonoBehaviour {

	private tk2dUIItem itemUI;
	
	void Start () {
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnClick += OnClick;
	}
	
	void OnClick() {
		TownController.instance.resetTalents();
	}
}
