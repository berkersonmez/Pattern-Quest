using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Creature {
	
	public string name;
	public Item weapon;
	public Item head;
	public Item necklace;
	public Item shoulder;
	public Item chest;
	public Item wrist;
	public Item gloves;
	public Item waist;
	public Item leg;
	public Item boots;
	
	public Player(){
		isPlayer = true;
	}
}
