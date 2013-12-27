using UnityEngine;
using System.Collections;

public class Poison : Spell {
	
	public Poison(){
		name = "Poison";
		damage = 0;
		damageOverTime = 3;
		turn = 3;
		mana = 4;
		type = "nature";
		level = 1;
		isOverTime = true;
		spriteName = "avatar_spell_2";
		shape.Add(8);
		shape.Add(6);
		shape.Add(8);
	}
		
	public override bool increaseLevel(){
		if(level == maxLevel)
			return false;
		else{
			level++;
			damageOverTime = Mathf.CeilToInt(damageOverTime*3/2);
			mana = Mathf.CeilToInt(mana*5/3);
			return true;
		}
	}
		
	public override bool cast(Battle battle, Creature caster, Creature target){
		if(caster.currentMana - mana < 0)
			return false;
		caster.decreaseMana(mana);
		battle.addActiveSpell(this, target);
		return true;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		Creature caster = owner;
		float currentDamageOverTime = (float)(damageOverTime*turn + caster.spellPower) / turn;

		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		if (mana != 0) tooltipText += "Mana Cost: " + mana + "\n\n";
		tooltipText += "Deals " + (int)currentDamageOverTime + " damage every turn for " + turn + " turns.\n";
	}
	
}
