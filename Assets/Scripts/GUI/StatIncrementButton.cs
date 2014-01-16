using UnityEngine;
using System.Collections;

public class StatIncrementButton : MonoBehaviour {

	private tk2dTextMesh t_spGiven;
	private tk2dUIItem uiItem;
	private LevelUpController levelUpController;

	[HideInInspector]
	public int spGiven;

	void Start () {
		t_spGiven = transform.Find("ButtonGraphic").Find("Text").GetComponent<tk2dTextMesh>();
		uiItem = GetComponent<tk2dUIItem>();
		uiItem.OnClick += OnClick;
		levelUpController = transform.parent.parent.GetComponent<LevelUpController>();
		spGiven = 0;
	}

	void OnClick() {
		if (levelUpController.statPointLeft > 0) {
			levelUpController.statPointLeft--;
			levelUpController.updateSpLeftText();
			spGiven++;
			updateSpGivenText();
		}
	}

	public void updateSpGivenText() {
		t_spGiven.text = "+" + spGiven + "";
	}
}
