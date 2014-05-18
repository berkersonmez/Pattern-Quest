using UnityEngine;
using System.Collections;

public class CheatPanelController : MonoBehaviour {

	public tk2dUITextInput i_cheat; // Assign using inspector
	public tk2dUIItem u_cheat; // Assign using inspector
	
	private Player player;
	
	void Start () {
		u_cheat.OnClick += OnClick;
		player = GameSaveController.instance.getPlayer();
	}
	
	void OnClick() {
		switch(i_cheat.Text) {
			case "gimmegold":
			player.gold += 100;
			TownController.instance.updateTexts();
			break;
			case "gimmecrystal":
			player.crystal += 100;
			TownController.instance.updateTexts();
			break;
		}
		GameSaveController.instance.saveGame();
	}
}
