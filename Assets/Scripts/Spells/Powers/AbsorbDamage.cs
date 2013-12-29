using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbsorbDamage : Power {
	
	public AbsorbDamage(){
		name = "AbsorbDamage";
		amount = 5;
		percent = 0;
		effectOn = "enemy";
		mana = 0;
		type = "fire";
		active = true;
		level = 1;
	}

	public override bool react(Spell castedSpell, string effectOn){
		if(effectOn != this.effectOn)
			return false;
		if(amount > 0){
			if(amount <= castedSpell.damage){
				castedSpell.damage -= amount;
				amount = 0;
				//DungeonController.instance.battle.creature.decreaseHp(DungeonController.instance.battle.player,amount,"absorb");
			}
			else {
				amount -= castedSpell.damage;
				castedSpell.damage = 0;
			}
			if(amount == 0)
				this.active = false;
			return false;
		}
		if(percent > 0)
			castedSpell.damage = castedSpell.damage * (percent / 100);
		return false;
	}
	
}