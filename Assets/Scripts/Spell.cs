using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spell {
	public enum EffectType {PROJECTILE = 0, ENEMY_INSTANT, SELF_INSTANT};
	
	public int damage=0;
	public int damageOverTime=0;
	public int mana=0;
	public string type;
	public int heal=0;
	public int healOverTime=0;
	public int level=1;
	public int maxLevel;
	public bool isOneTick=false;	//This is an special variable to make 1 tick of any dot understandable
	public string name;
	public string spriteName = "avatar_spell_0";
	public int turn=0;
	public bool isOverTime=false;
	public List<int> shape = new List<int>();
	public int effectType;

	public Creature owner;

	public string tooltipText;
	
	public Spell(){
		
	}
	
	public Spell(int damage){
		this.damage = damage;
		this.mana = 0;
		//this.mana = (int)Mathf.Sqrt(damage);
		//Debug.Log("mana: " + this.mana);
		this.name = "Basic Attack";
		this.shape.Add(9);
		this.shape.Add(9);
	}

	public Spell(string name, int damage){
		this.name = name;
		this.damage = damage;
		this.mana = 0;
	}

	public Spell(string name, int damage, bool isOneTick){
		this.name = name;
		this.damage = damage;
		this.isOneTick = isOneTick;
		this.mana = 0;
	}

	public virtual bool cast(Battle battle, Creature caster, Creature target){
		if(caster.currentMana - mana < 0)
			return false;
		Spell temp = new Spell();
		temp = this.copy();
		if(temp.isOneTick == false)
			caster.react(temp,"self");
		caster.decreaseMana(mana);
		bool result = target.react(temp,"enemy");
		if(result)
			return true;

		int currentDamage;
		if(temp.isOneTick == true)
			currentDamage = temp.damage;
		else
			currentDamage = temp.damage + caster.spellPower - target.armor;
		if(currentDamage < 0)
			currentDamage = 0;
		target.decreaseHp(caster, currentDamage, name);
		return true;		
	}
	
	public virtual bool increaseLevel(){
		if(level == maxLevel)
			return false;
		else{
			level++;
			return true;
		}
	}
	
	public virtual void turnEffect(Battle battle, ref Creature caster, ref Creature target){
		if(this.damageOverTime > 0){
			float currentDamage = (float)(damageOverTime*turn - target.armor + caster.spellPower) / turn;
			Spell temp = new Spell(this.name,(int)currentDamage,true);
			temp.cast(battle, caster, target);
			return;
		}
		if(healOverTime > 0)
			target.increaseHp(healOverTime, name);
	}

	public virtual void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		if (mana != 0) tooltipText += "Mana Cost: " + mana + "\n";
		if (damage != 0) tooltipText += "Damage: " + damage + "\n";
		if (heal != 0) tooltipText += "Heal: " + heal + "\n";
	}

	public Spell copy(){
		return (Spell)this.MemberwiseClone();
	}
}
