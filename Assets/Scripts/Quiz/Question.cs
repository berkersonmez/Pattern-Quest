using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Question {

	public int id;
	public string query;
	public Dictionary<int, string> choices = new Dictionary<int,string>();
	public int answer;
	
	public void addChoice(int id, string choice) {
		choices.Add(id, choice);
	}
}
