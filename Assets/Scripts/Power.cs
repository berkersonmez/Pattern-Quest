using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Power : Spell {

	public bool active;

	public string effectOn;		//"self" is used for buffs that effect when you attack //"enemy" is used for buffs that procs when the enemy attacks
	public int currentAmount;
	public int totalAmount;
	public int remainingTurn=-1;	//turn count if the power is active for only some spesific turns
	public int totalTurn=-1;		//-1 means it is active until it is dispelled
	public bool justForThisBattle = false;

	//True means reaction ended the spell no need to complete it in the spell.cast part
	public virtual bool react(Spell castedSpell, string effectOn, ref string combatTextExtra){

		return false;
	}

	public Power(){
		percent=0;
	}

	public Power(int amount, int percent){
		this.totalAmount = amount;
		this.currentAmount = amount;
		this.percent = percent;
	}

	public override bool cast(Battle battle, Creature caster, Creature target, int critDamageIncrease){
		Debug.Log(this.name + "'in cooldown'i " + this.totalCoolDown);
		Power temp = new Power();
		temp = this.copy();
		temp.totalAmount = this.amount;
		temp.currentAmount = this.amount;
		temp.type = this.type;
		temp.justForThisBattle = true;
		this.currentCoolDown = this.totalCoolDown;
		caster.powers.Add(temp);
		// Combat text
		int placement = !target.isPlayer ? (int)CombatTextController.Placement.PLAYER : (int)CombatTextController.Placement.CREATURE;
		CombatTextController.instance.deployText(name, "("+amount.ToString()+")", placement, new Color(.9f, .9f, .9f));
		return true;
	}

	public override void resetCooldown(){
		this.currentAmount = this.totalAmount;
		this.currentCoolDown = 0;
		this.active = true;
		this.remainingTurn = this.totalTurn;
	}

	public override void update(){
		if(currentCoolDown > 0)
			currentCoolDown--;
		if(active == false){
			if(currentCoolDown == 0){
				active = true;
				remainingTurn = totalTurn;
			}
		}else{
			if(remainingTurn > 0)
				remainingTurn--;
			if(remainingTurn == 0)
				active = false;
		}
	}

	public override void change(string allParameters){
		Debug.Log(this.idName + " POWER DEGiSTiRiLiYOR");
		string[] values = allParameters.Split(',');
		for(int i=0; i<values.Length; i++){
			string[] part = values[i].Split('=');

			switch(part[0]){
			case "name":
				this.name = part[1];
				break;
			case "damage":
				this.damage += int.Parse(part[1]);
				break;
			case "heal":
				this.heal += int.Parse(part[1]);
				break;
			case "dot":
				this.damageOverTime += int.Parse(part[1]);
				this.isOverTime = true;
				break;
			case "hot":
				this.healOverTime += int.Parse(part[1]);
				this.isOverTime = true;
				break;
			case "turn":
				this.totalTurn += int.Parse(part[1]);
				break;
			case "mana":
				this.mana += int.Parse(part[1]);
				break;
			case "cooldown":
				this.totalCoolDown += int.Parse(part[1]);
				break;
			case "level":
				this.level += int.Parse(part[1]);
				break;
			case "amount":
				this.totalAmount += int.Parse(part[1]);
				this.currentAmount += int.Parse(part[1]);
				break;
			case "percent":
				this.percent += int.Parse(part[1]);
				break;
			}
		}
	}

	public override void unchange(string allParameters){
		string[] values = allParameters.Split(',');
		for(int i=0; i<values.Length; i++){
			string[] part = values[i].Split('=');
			switch(part[0]){
			case "name":
				this.name = part[1];
				break;
			case "damage":
				this.damage -= int.Parse(part[1]);
				break;
			case "heal":
				this.heal -= int.Parse(part[1]);
				break;
			case "dot":
				this.damageOverTime -= int.Parse(part[1]);
				this.isOverTime = true;
				break;
			case "hot":
				this.healOverTime -= int.Parse(part[1]);
				this.isOverTime = true;
				break;
			case "turn":
				this.totalTurn -= int.Parse(part[1]);
				break;
			case "mana":
				this.mana -= int.Parse(part[1]);
				break;
			case "cooldown":
				this.totalCoolDown -= int.Parse(part[1]);
				break;
			case "level":
				this.level -= int.Parse(part[1]);
				break;
			case "amount":
				this.totalAmount -= int.Parse(part[1]);
				break;
			}
		}
	}

	public Power copy(){
		return (Power)this.MemberwiseClone();
	}

}
