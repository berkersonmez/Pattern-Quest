using UnityEngine;
using System.Collections;

public class DungeonController : MonoBehaviour {
	public static DungeonController instance;
	public Creature currentCreature;
	public Battle battle;
	public Player player;
	
	private Avatar mobAvatar;
	private Avatar playerAvatar;
	private tk2dUIItem buttonNextBattle;
	private tk2dTextMesh textNextBattle;
	private tk2dUIItem buttonRetry;
	private tk2dUIItem buttonMainMenu;
	
	void Start () {
		instance = this;
		player = GameSaveController.instance.getPlayer();
		player.restoreHealthMana();
		mobAvatar = GameObject.Find("Avatar Creature").GetComponent<Avatar>();
		playerAvatar = GameObject.Find("Avatar Player").GetComponent<Avatar>();
		buttonNextBattle = GameObject.Find("Nextcreature Button").GetComponent<tk2dUIItem>();
		buttonNextBattle.OnClick += nextBattleClick;
		textNextBattle = GameObject.Find("Nextcreature Text").GetComponent<tk2dTextMesh>();
		buttonRetry = GameObject.Find("Tryagain Button").GetComponent<tk2dUIItem>();
		buttonRetry.OnClick += retryClick;
		buttonMainMenu = GameObject.Find("Backtomenu Button").GetComponent<tk2dUIItem>();
		buttonMainMenu.OnClick += mainMenuClick;
		playerAvatar.setOwner(player);
		enterDungeon();
	}
	
	public void enterDungeon() {
		currentCreature = LevelDescriptor.instance.getNextCreature();
		Debug.Log(currentCreature.name);
		startBattle();
	}
	
	public void startBattle() {
		battle = new Battle(ref player, ref currentCreature);
		mobAvatar.setOwner(currentCreature);
	}
	
	public void switchTurn(bool isPlayersTurn) {
		playerAvatar.visualizeTurn(isPlayersTurn);
		mobAvatar.visualizeTurn(!isPlayersTurn);
	}
	
	public void finishBattle(bool playerWon) {
		if (playerWon) {
			mobAvatar.deadAnim();
			LootWindow.instance.prepare();
			Invoke("switchWinWindow", .5f);
		} else {
			playerAvatar.deadAnim();
			Invoke("switchLoseWindow", .5f);
		}
		
	}
	
	void switchWinWindow() {
		DungeonCamera.instance.winWindow();
		currentCreature = LevelDescriptor.instance.getNextCreature();
		if (currentCreature != null) {
			textNextBattle.text = "Next Creature";
		} else {
			textNextBattle.text = "Go Back to Town";
		}
		textNextBattle.Commit();
	}
	
	void switchLoseWindow() {
		DungeonCamera.instance.loseWindow();
	}
	
	void nextBattleClick() {
		if (currentCreature != null) {
			DungeonCamera.instance.battleWindow();
			startBattle();
		} else {
			Application.LoadLevel("main");
		}
	}
	
	void retryClick() {
		LevelDescriptor.instance.resetLevel();
		Application.LoadLevel("dungeon");
	}
	
	void mainMenuClick() {
		Application.LoadLevel("main");
	}
	
	void Update() {
		if (battle != null) {
			battle.update();
		}
	}
}
