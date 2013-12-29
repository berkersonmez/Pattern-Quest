using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Power : Spell {

	public bool active;

	public string effectOn;		//"self" is used for buffs that effect when you attack //"enemy" is used for buffs that procs when the enemy attacks
	public int amount;
	public int percent;
	public int coolDown;		//turn count to be active again
	public int remainingTurn;	//turn count if the power is active for only some spesific turns
	public int totalTurn;		//-1 means it is active until it is dispelled

	//True means reaction ended the spell no need to complete it in the spell.cast part
	public virtual bool react(Spell castedSpell, string effectOn){

		return false;
	}

	public Power(){
		percent=0;
	}

	public Power(int amount, int percent){
		this.amount = amount;
		this.percent = percent;
	}

	public virtual void update(){
		if(active == false){
			if(coolDown > 0)
				coolDown--;
			if(coolDown == 0){
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

}
