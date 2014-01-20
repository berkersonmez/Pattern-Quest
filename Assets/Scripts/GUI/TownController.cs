using UnityEngine;
using System.Collections;

public class TownController : MonoBehaviour {

	public static TownController instance;

	public GameObject levelUpWindowPrefab;

	private tk2dTextMesh textGold;
	private tk2dTextMesh textLevel;
	private tk2dTextMesh textXP;
	private tk2dTextMesh textTP;

	public Quest[] quests;
	public Talent[] talents;

	void Start () {
		instance = this;
		textGold = GameObject.Find("Gold").GetComponent<tk2dTextMesh>();
		textLevel = GameObject.Find("Level").GetComponent<tk2dTextMesh>();
		textXP = GameObject.Find("XP").GetComponent<tk2dTextMesh>();
		textTP = GameObject.Find("Talent Points").GetComponent<tk2dTextMesh>();
		checkLevelUp();
		updateTexts();
	}

	public void checkLevelUp() {
		Player player = GameSaveController.instance.getPlayer();
		int xpReq = GameSaveController.instance.xpRequiredForLevel(player.level + 1);
		int levelUpCount = 0;
		while (player.xp >= xpReq) {
			player.xp -= xpReq;
			player.level++;
			player.tp++;
			xpReq = GameSaveController.instance.xpRequiredForLevel(player.level + 1);
			levelUpCount++;
		}
		if (levelUpCount > 0) {
			GameObject levelUpWindow = Instantiate(levelUpWindowPrefab) as GameObject;
			levelUpWindow.transform.position = new Vector3(Camera.main.transform.position.x,
			                                               Camera.main.transform.position.y,
			                                               -7f);
			levelUpWindow.GetComponent<LevelUpController>().initialize(levelUpCount);
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

	public void updateTalentGUI() {
		Player player = GameSaveController.instance.getPlayer();
		foreach (Talent talent in talents) {
			talent.refreshGUI();
		}
		textTP.text = "Talent Points left: " + player.tp;
		textTP.Commit();
	}

	public void resetTalents() {
		foreach (Talent talent in talents) {
			talent.deactivate();
		}
	}

	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			Tooltip.instance.hideTooltip();
		}
	}
}
