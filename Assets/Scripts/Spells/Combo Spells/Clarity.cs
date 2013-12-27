using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clarity : ComboSpell {
	
	public Clarity(){
		name = "Clarity";
		damage = 0;
		mana = 0;
		type = "fire";
		level = 1;
	}
	
	public override bool cast(Battle battle, Creature caster, Creature target){
		caster.increaseMana(3,"Clarity");
		return true;
	}
	
	public override bool requires(Queue<Spell> castedSpells){
		int basic_attack_count = 0;
		foreach(Spell spell in castedSpells){
			if(spell.name == "Basic Attack")
				basic_attack_count++;
		}
		if(basic_attack_count >= 2)
			return true;
		return false;
	}
	
}
