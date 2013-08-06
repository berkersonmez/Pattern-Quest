﻿using UnityEngine;
using System.Collections;

public class DungeonController : MonoBehaviour {
	public static DungeonController instance;
	public Creature currentCreature;
	public Battle battle;
	public Player player;
	
	private Avatar mobAvatar;
	private Avatar playerAvatar;
	
	void Start () {
		instance = this;
		player = GameSaveController.instance.getPlayer();
		mobAvatar = GameObject.Find("Avatar Creature").GetComponent<Avatar>();
		playerAvatar = GameObject.Find("Avatar Player").GetComponent<Avatar>();
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
			Invoke("switchWinWindow", .5f);
		} else {
			playerAvatar.deadAnim();
			Invoke("switchLoseWindow", .5f);
		}
		
	}
	
	void switchWinWindow() {
		DungeonCamera.instance.winWindow();
	}
	
	void switchLoseWindow() {
		DungeonCamera.instance.loseWindow();
	}
	
	void Update() {
		if (battle != null) {
			battle.update();
		}
	}
}
