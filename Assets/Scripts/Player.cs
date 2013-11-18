using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Creature {
	
	public Item red_stone;
	public Item blue_stone;
	public Item green_stone;
	
	public Player(){
		isPlayer = true;
		spellPerTurn = 2;
		spriteName = "avatar_player_1";
	}
	
	public override void decreaseHp(int amount) {
		currentHp -= amount;
		if(currentHp < 0)
			currentHp = 0;
		CombatTextController.instance.deployText(amount, (int)CombatTextController.Placement.PLAYER);
	}
}
