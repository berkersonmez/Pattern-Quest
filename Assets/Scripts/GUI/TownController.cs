using UnityEngine;
using System.Collections;

public class TownController : MonoBehaviour {

	public static TownController instance;

	public GameObject levelUpWindowPrefab;

	private GameObject[] textGold;
	private tk2dTextMesh textLevel;
	private tk2dTextMesh textXP;
	private tk2dTextMesh textTP;
	private GameObject[] textCrystal;

	public Quest[] quests;
	public Talent[] talents;

	void Start () {
		instance = this;
		textGold = GameObject.FindGameObjectsWithTag("GoldText");
		textCrystal = GameObject.FindGameObjectsWithTag("CrystalText");
		textLevel = GameObject.Find("Level").GetComponent<tk2dTextMesh>();
		textXP = GameObject.Find("XP").GetComponent<tk2dTextMesh>();
		textTP = GameObject.Find("Talent Points").GetComponent<tk2dTextMesh>();
		if (LevelDescriptor.instance.goToMapOnLoad) {
			TownMenu.instance.mapWindowInstantly();
		}
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
		foreach (GameObject tg in textGold) {
			tk2dTextMesh tgTextMesh = tg.GetComponent<tk2dTextMesh>();
			tgTextMesh.text = player.gold.ToString();
			tgTextMesh.Commit();
		}
		foreach (GameObject tc in textCrystal) {
			tk2dTextMesh tcTextMesh = tc.GetComponent<tk2dTextMesh>();
			tcTextMesh.text = player.crystal.ToString();
			tcTextMesh.Commit();
		}
		textLevel.text = "Level: " + player.level;
		textXP.text = "XP: " + player.xp + "/" + GameSaveController.instance.xpRequiredForLevel(player.level + 1);
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

	public void updateQuestsGUI() {
		foreach (Quest quest in quests) {
			quest.update();
		}
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
