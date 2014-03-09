using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {

	public int levelNo;
	public int unlocksAtLevel;
	public string levelName;
	private tk2dUIItem uiItem;
	private tk2dSprite s_button;
	private Player player;
	private bool isEnabled;
	private string tooltipText;
	
	void Start() {
		uiItem = GetComponent<tk2dUIItem>();
		uiItem.OnDown += OnDown;
		uiItem.OnRelease += OnRelease;
		player = GameSaveController.instance.getPlayer();
		s_button = transform.Find("ButtonGraphic").GetComponent<tk2dSprite>();
	}

	void OnDown() {
		Invoke("OnHold", .5f);
	}
	
	void OnRelease() {
		if (IsInvoking("OnHold")) {
			CancelInvoke("OnHold");
			OnClick();
		}
	}
	
	void OnHold() {
		setTooltipText();
		Tooltip.instance.setText(tooltipText);
		Tooltip.instance.showTooltip(transform.position);
	}
	
	void OnClick() {
		if (!isEnabled) return;
		loadLevel();
	}
	
	void loadLevel() {
		// Fill "_LevelDescriptor" with dungeon info.
		LevelDescriptor.instance.describeLevel(levelNo);
		Application.LoadLevel("dungeon");
	}

	void setTooltipText() {
		tooltipText = "^Cbab14aff" + levelName;
		if (unlocksAtLevel > 0) {
			tooltipText += "\n^CffffffffUnlocks at Level: " + unlocksAtLevel;
		}
	}

	void Update() {
		if (player.level >= unlocksAtLevel) {
			isEnabled = true;
			s_button.color = new Color(1f, 1f, 1f, 1f);
		} else {
			isEnabled = false;
			s_button.color = new Color(1f, 1f, 1f, .4f);
		}
	}
}
