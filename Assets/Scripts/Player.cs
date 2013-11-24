using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Creature {

	public int xp;
	public Item redStone;
	public Item blueStone;
	public Item greenStone;
	public List<Item> inventory = new List<Item>();
	
	public Player(){
		isPlayer = true;
		spellPerTurn = 2;
		spriteName = "avatar_player_1";
	}
	
	public override void decreaseHp(int amount, string effectName) {
		currentHp -= amount;
		if(currentHp < 0)
			currentHp = 0;
		CombatTextController.instance.deployText(effectName, amount, (int)CombatTextController.Placement.PLAYER);
	}
}
