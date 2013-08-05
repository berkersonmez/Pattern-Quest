using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Creature {
	
	public Item weapon;
	public Item head;
	public Item necklace;
	public Item shoulder;
	public Item chest;
	public Item wrist;
	public Item gloves;
	public Item waist;
	public Item leg;
	public Item boots;
	
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
