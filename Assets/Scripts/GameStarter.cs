using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {
	public static GameStarter instance;
	
	tk2dTextMesh loadingText;
	tk2dUITextInput playerNameInput;
	tk2dUIItem playerCreateButton;
	
	private int newSaveSlotId;

	void Start () {
		//PlayerPrefs.DeleteAll();
		instance = this;
		
		loadingText = GameObject.Find("Loading Text").GetComponent<tk2dTextMesh>();
		playerNameInput = GameObject.Find("Player Name Input").GetComponent<tk2dUITextInput>();
		playerCreateButton = GameObject.Find("Create Char Button").GetComponent<tk2dUIItem>();
		playerCreateButton.OnClick += OnCreateClick;
		
		DontDestroyOnLoad(GameObject.Find("_LevelDescriptor"));
		Invoke("loadGame", .5f);
	}
	
	void loadGame() {
		SaveSlotsController.instance.loadSaveSlots();
		Camera.main.transform.position = new Vector3(24f, 0f, -10f);
//		if (GameSaveController.instance.loadGame(1)) {
//			Application.LoadLevel("main");
//		} else {
//			playerNameInput.transform.position = new Vector3(-6.8f, 15.1f, -1f);
//			playerCreateButton.transform.position = new Vector3(0, -15.8f, -1f);
//			loadingText.text = "Enter character name:";
//			loadingText.Commit();
//			loadingText.transform.position = new Vector3(0, 17.5f, -1f);
//		}
	}
	
	public void newSaveGame(int newSaveSlotId) {
		playerNameInput.transform.position = new Vector3(-6.8f, 15.1f, -1f);
		playerCreateButton.transform.position = new Vector3(0, -15.8f, -1f);
		Camera.main.transform.position = new Vector3(0f, 0f, -10f);
		loadingText.text = "Enter character name:";
		loadingText.Commit();
		loadingText.transform.position = new Vector3(0, 17.5f, -1f);
		this.newSaveSlotId = newSaveSlotId;
	}
	
	void OnCreateClick() {
		if (playerNameInput.Text != "") {
			GameSaveController.instance.makeNewSave(playerNameInput.Text, newSaveSlotId);
			Application.LoadLevel("main");
		}
	}
}
