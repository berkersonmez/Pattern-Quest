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
		checkLevelUp();
		updateTexts();
	}

	public void checkLevelUp() {
		Player player = GameSaveController.instance.getPlayer();
		int xpReq = GameSaveController.instance.xpRequiredForLevel(player.level + 1);
		while (player.xp >= xpReq) {
			player.xp -= xpReq;
			player.level++;
			xpReq = GameSaveController.instance.xpRequiredForLevel(player.level + 1);
			Debug.Log("LEVEL UP!");
			// TODO: Level up things here!
		}
	}

	public void updateTexts() {
		Player player = GameSaveController.instance.getPlayer();
		textGold.text = "Gold: " + player.gold;
		textLevel.text = "Level: " + player.level;
		textXP.text = "XP: " + player.xp + "/" + GameSaveController.instance.xpRequiredForLevel(player.level + 1);
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
