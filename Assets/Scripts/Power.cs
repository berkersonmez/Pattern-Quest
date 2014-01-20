using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Power : Spell {

	public bool active;

	public string effectOn;		//"self" is used for buffs that effect when you attack //"enemy" is used for buffs that procs when the enemy attacks
	public int currentAmount;
	public int totalAmount;
	public int percent;
	public int remainingTurn=-1;	//turn count if the power is active for only some spesific turns
	public int totalTurn=-1;		//-1 means it is active until it is dispelled

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

	public override void resetCooldown(){
		this.currentCooldDown = 0;
		this.active = true;
		this.remainingTurn = this.totalTurn;
	}

	public override void update(){
		if(active == false){
			if(currentCooldDown > 0)
				currentCooldDown--;
			else if(currentCooldDown == 0){
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
				break;
			}
		}
	}

}
