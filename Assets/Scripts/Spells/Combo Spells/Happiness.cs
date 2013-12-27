using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Happiness : ComboSpell {
	
	public Happiness(){
		name = "Happiness";
		damage = 0;
		healOverTime = 3;
		turn = 5;
		mana = 0;
		type = "nature";
		level = 1;
		isOverTime = true;
	}
	
	public override bool cast(Battle battle, Creature caster, Creature target){
		battle.addActiveSpell(this, caster);
		return true;
	}

	public override bool requires(Queue<Spell> castedSpells){
		int casted_heal_count = 0;
		foreach(Spell spell in castedSpells){
			if(spell.name == "Heal")
				casted_heal_count++;
		}
		if(casted_heal_count >= 2)
			return true;
		return false;
	}
}
