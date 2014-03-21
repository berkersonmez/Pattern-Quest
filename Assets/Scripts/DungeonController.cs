using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	// Loot from dungeon is held here.
	private List<Item> allLootItems = new List<Item>();
	private int allLootGold;
	private int allLootXP;
	
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
		allLootItems.Clear();
		allLootGold = 0;
		allLootXP = 0;
		currentCreature = LevelDescriptor.instance.getNextCreature();
		Debug.Log(currentCreature.name);
		startBattle();
	}
	
	public void startBattle() {
		battle = new Battle(player, currentCreature);
		mobAvatar.setOwner(currentCreature);
		GameObject.Find("Avatar Creature").GetComponent<Avatar>().updateActiveSpellVisuals();
		GameObject.Find("Avatar Player").GetComponent<Avatar>().updateActiveSpellVisuals();
	}
	
	public void switchTurn(bool isPlayersTurn) {
		playerAvatar.visualizeTurn(isPlayersTurn);
		mobAvatar.visualizeTurn(!isPlayersTurn);
		PatternController.instance.clearCritPoints();
		if (isPlayersTurn) {
			PatternController.instance.setCritPoints();
		}
	}
	
	public void finishBattle(bool playerWon) {
		if (playerWon) {
			mobAvatar.deadAnim();
			LootWindow.instance.prepare();
			allLootItems.AddRange(currentCreature.droppedItems);
			allLootGold += currentCreature.gold;
			allLootXP += currentCreature.level * Globals.instance.xpPerCreatureLevel; // TODO: Calculate XP
			player.slainCreature(currentCreature.name); // for quests (may count kills in this)
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
			player.changeManaPercent(player.manaRegenPercentBetweenBattles);//For "Mana Rest" Talent
			startBattle();
		} else {
			// END BATTLE SUCCESSFULLY
			player.inventory.AddRange(allLootItems);
			player.gold += (int) (allLootGold * (1+player.goldBonus));
			player.xp += (int) (allLootXP * (1+player.xpBonus));
			GameSaveController.instance.player = player;
			GameSaveController.instance.saveGame();
			Application.LoadLevel("main");
		}
	}
	
	void retryClick() {
		LevelDescriptor.instance.resetLevel();
		Application.LoadLevel("dungeon");
	}
	
	void mainMenuClick() {
		// END BATTLE UNSUCCESSFUL
		// Loot is not saved
		Application.LoadLevel("main");
	}
	
	void Update() {
		if (battle != null) {
			battle.update();
		}
	}
}
