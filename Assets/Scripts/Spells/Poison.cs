using UnityEngine;
using System.Collections;

[System.Serializable]
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

	public Poison(string name, int damageOverTime, int turn){
		this.name = name;
		damage = 0;
		this.damageOverTime = damageOverTime;
		this.turn = turn;
		mana = 5;
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
		
	public override bool cast(Battle battle, Creature caster, Creature target, int critIncrease){
		string combatTextExtra = "";
		if(caster.currentMana - mana < 0)
			return false;

		Spell temp = new Spell();
		temp = this.copy();
		temp.damageOverTime += (int)((float)(caster.SpellPower - target.Armor) / turn);
		caster.react(temp,"self",ref combatTextExtra);
		caster.decreaseMana(mana);
		bool result = target.react(temp,"enemy",ref combatTextExtra);
		if(result == true)
			return true;
		battle.addActiveSpell(temp, target);
		return true;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		Creature caster = owner;
		float currentDamageOverTime = (float)(damageOverTime*turn + caster.SpellPower) / turn;

		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		if (mana != 0) tooltipText += "Mana Cost: " + mana + "\n\n";
		tooltipText += "Deals " + (int)currentDamageOverTime + " damage every turn for " + turn + " turns.\n";
	}
	
}
