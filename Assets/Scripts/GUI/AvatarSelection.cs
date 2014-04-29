using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvatarSelection : MonoBehaviour {
	public static AvatarSelection instance;
	public int selectedChoiceID;
	public List<AvatarChoice> choices = new List<AvatarChoice>();
	
	void Awake() {
		instance = this;
	}
	
	void Start() {
		initialize();
	}
	
	public void initialize() {
		int i = 0;
		foreach (Transform slot in transform) {
			AvatarChoice choice = slot.GetComponent<AvatarChoice>();
			if (choice == null) continue;
			choice.initialize(i);
			choices.Add(choice);
			i++;
		}
		i = Random.Range(0, choices.Count);
		choices[i].visualiseSelect();
		select(i);
	}
	
	public void select(int choiceID) {
		selectedChoiceID = choiceID;
	}
	
	public void resetChoiceVisuals() {
		foreach (AvatarChoice choice in choices) {
			choice.visualiseUnselect();
		}
	}
	
	public string getSelectedAvatar() {
		return choices[selectedChoiceID].getAvatar();
	}
}
