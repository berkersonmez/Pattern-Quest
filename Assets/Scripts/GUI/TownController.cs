using UnityEngine;
using System.Collections;

public class TownController : MonoBehaviour {

	public static TownController instance;

	private tk2dTextMesh textGold;
	private tk2dTextMesh textLevel;
	private tk2dTextMesh textXP;

	public Quest[] quests;

	void Start () {
		instance = this;
		textGold = GameObject.Find("Gold").GetComponent<tk2dTextMesh>();
		textLevel = GameObject.Find("Level").GetComponent<tk2dTextMesh>();
		textXP = GameObject.Find("XP").GetComponent<tk2dTextMesh>();
		updateTexts();
	}

	public void updateTexts() {
		Player player = GameSaveController.instance.getPlayer();
		textGold.text = "Gold: " + player.gold;
		textLevel.text = "Level: " + player.level;
		textXP.text = "XP: " + player.xp;
		textGold.Commit();
		textLevel.Commit();
		textXP.Commit();
	}

	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			Tooltip.instance.hideTooltip();
		}
	}
}
