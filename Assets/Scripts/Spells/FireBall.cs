using UnityEngine;
using System.Collections;

public class FireBall : Spell {
	
	public FireBall(){
		name = "FireBall";
		damage = 10;
		mana = 3;
		type = "fire";
		level = 1;
		shape.Add(9);
		shape.Add(2);
		shape.Add(9);
	}
		
	public override bool increaseLevel(){
		if(level + 1 < maxLevel){
			level = level+1;
			damage = damage*2;
			mana = Mathf.CeilToInt(mana*5/3);
			return true;
		}
		else
			return false;
	
	}
		
	public override bool cast(Battle battle, Creature caster, Creature target){
		if(caster.currentMana - mana < 0)
			return false;
		Spell temp = new Spell();
		temp = this.copy();
		caster.react(temp,"self");
		bool result = target.react(temp,"enemy");
		if(result)
			return true;

		int currentDamage = temp.damage + caster.spellPower - target.armor;
		if(currentDamage < 0)
			currentDamage = 0;
		caster.decreaseMana(mana);
		target.decreaseHp(caster, currentDamage, name);
		Debug.Log(this.name + "'i patlattim *" + this.damage + "*");
		return true;
		//Fireball görsel efekti yapan fonksiyon eklenecek
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		Creature caster = owner;
		int currentDamage = damage + caster.spellPower;

		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		if (mana != 0) tooltipText += "Mana Cost: " + mana + "\n\n";
		tooltipText += "Inflicts " + currentDamage + " damage to enemy.\n";
	}
	
}
