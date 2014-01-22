using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ActiveSpell{
	
	public Spell spell;
	public int remainingTurn;
	
	public ActiveSpell (Spell spell){
		this.spell = spell;
		this.remainingTurn = spell.turn;
	}
	
	public bool effect(Battle battle, Creature caster, Creature target){
		remainingTurn--;
		this.spell.turnEffect(battle, ref caster, ref target);
		if(remainingTurn == 0)
			return false;
		else
			return true;
	}
}