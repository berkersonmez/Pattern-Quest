using UnityEngine;
using System.Collections;

public class SaveSlot : MonoBehaviour {

	private int id;
	private Player player;
	private bool isActive; // Is there a save in this slot?
	private tk2dUIItem uiItem;
	private tk2dUIItem uiDeleteButtonItem;
	
	void Start() {
		uiItem = GetComponent<tk2dUIItem>();
		uiDeleteButtonItem = transform.Find("DeleteButton").GetComponent<tk2dUIItem>();
		uiItem.OnClick += OnClick;
		uiDeleteButtonItem.OnClick += OnDeleteClick;
	}
	
	public void init(int id) {
		this.id = id;
	}
	
	public void load() {
		Player slotPlayer;
		isActive = GameSaveController.instance.loadGame(id, out slotPlayer);
		player = slotPlayer;
		updateVisuals();
	}
	
	void updateVisuals() {
		tk2dSprite s_avatar = transform.Find("Avatar").GetComponent<tk2dSprite>();
		tk2dSprite s_border = gameObject.GetComponent<tk2dSprite>();
		tk2dTextMesh s_text = transform.Find("Text").GetComponent<tk2dTextMesh>();
		if (isActive) {
			s_avatar.SetSprite(player.spriteName);
			s_avatar.color = new Color(1f, 1f, 1f, 1f);
			s_border.color = new Color(1f, 1f, 1f, 1f);
			s_text.text = player.name;
			s_text.Commit();
			uiDeleteButtonItem.gameObject.SetActive(true);
		} else {
			s_avatar.SetSprite("avatar_player_1");
			s_avatar.color = new Color(0f, 0f, 0f, 1f);
			s_border.color = new Color(.3f, .3f, .3f, 1f);
			s_text.text = "Create Character";
			s_text.Commit();
			uiDeleteButtonItem.gameObject.SetActive(false);
		}
	}
	
	void OnClick() {
		if (isActive) {
			GameSaveController.instance.loadGame(id);
			Application.LoadLevel("main");
		} else {
			GameStarter.instance.newSaveGame(id);
		}
	}
	
	void OnDeleteClick() {
		GameSaveController.instance.deleteSave(id);
		isActive = false;
		updateVisuals();
	}
}
