using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour {

	public int id; // Must be same with the index in "quests" list in "TownController" class
	public string questName;
	public string description;

	public int unlocksAtLevel;
	public bool isRepeatable;
	public string[] collectItems;
	public int[] collectItemCounts;
	public bool removeCollectedItems = true;
	public string[] slayEnemies;
	public int[] slayEnemyCounts;
	public string[] learnSpells;
	public int reachLevel;

	public string[] rewardItems;
	public string[] rewardSpells;
	public string[] rewardComboSpells;
	public string[] rewardPowers;
	public int rewardGold = 0;
	public int rewardXP = 0;

	private string tooltipText;

	private tk2dUIItem uiItem;
	private Player player;
	private bool isEnabled = true;
	private bool isCompleted;
	private tk2dSprite s_button;
	public GameObject questParchementPrefab;
	
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
		setLockInfoText();
		Tooltip.instance.setText(tooltipText);
		Tooltip.instance.showTooltip(transform.position);
	}

	void OnClick() {
		if (!isEnabled) return;
		setTooltipText();
		GameObject questParchement = Instantiate(questParchementPrefab) as GameObject;
		questParchement.transform.parent = this.transform.parent;
		questParchement.transform.localPosition = new Vector3(0f, 0f, -1f);
		tk2dTextMesh parchementText = questParchement.transform.Find("Text").GetComponent<tk2dTextMesh>();
		parchementText.text = tooltipText;
		parchementText.Commit();
		QuestButton qButton = questParchement.transform.Find("AcceptDeclineButton").GetComponent<QuestButton>();
		qButton.initialize(this);
		if (isCompleted) qButton.gameObject.SetActive(false);
		qButton = questParchement.transform.Find("BackButton").GetComponent<QuestButton>();
		qButton.initialize(this);
	}

	public bool isAccepted() {
		return player.questSlayCounter.ContainsKey(id);
	}

	public void accept() {
		player.questSlayCounter.Add(id, new Dictionary<string, int>());
		foreach(string creatureName in slayEnemies) {
			player.questSlayCounter[id].Add(creatureName, 0);
		}
		s_button.SetSprite("map_sign_3");
		GameSaveController.instance.saveGame();
	}

	public void decline() {
		player.questSlayCounter.Remove(id);
		s_button.SetSprite("map_sign_1");
		GameSaveController.instance.saveGame();
	}

	public void complete() {
		player.questSlayCounter.Remove(id);
		removeTargetedItems();
		getRewards();
		InventoryController.instance.refreshItemList();
		SpellListController.instance.refreshSpellList();
		SpellListController.instance.refreshComboList();
		SpellListController.instance.refreshPowerList();
		TownController.instance.checkLevelUp();
		TownController.instance.updateTexts();
		if (!isRepeatable) {player.completedQuests.Add(id);
		s_button.SetSprite("map_sign_2");
		isCompleted = true;
		GameSaveController.instance.saveGame();
		}
	}

	public bool checkCompletion() {

		// Reach level
		if (player.level < reachLevel) return false;

		// Learn spells
		foreach (string spellName in learnSpells) {
			if (!learnedSpell(spellName)) return false;
		}

		// Collect items
		for (int i = 0; i < collectItems.Length; i++) {
			if (collectedItemCount(collectItems[i]) < collectItemCounts[i])
				return false;
		}

		// Slay enemies
		for (int i = 0; i < slayEnemies.Length; i++) {
			if (slainEnemyCount(slayEnemies[i]) < slayEnemyCounts[i])
				return false;
		}

		return true;
	}

	public bool learnedSpell(string spellName) {
		bool found = false;
		foreach (Spell spell in player.spellList) {
			if (spell.idName == spellName) {
				found = true;
				break;
			}
		}
		return found;
	}

	public int collectedItemCount(string itemName) {
		int count = 0;
		foreach (Item playerItem in player.inventory) {
			if (playerItem.name == itemName) count++;
		}
		return count;
	}

	public void removeTargetedItems() {
		if (removeCollectedItems) {
			for (int i = 0; i < collectItems.Length; i++) {
				string itemName = collectItems[i];
				int itemCountToDelete = collectItemCounts[i];
				int counter = 0;
				while (counter < itemCountToDelete) {
					int j;
					for (j = 0; j < player.inventory.Count; j++) {
						if (player.inventory[j].name == itemName) break;
					}
					player.inventory.RemoveAt(j);
					counter++;
				}
			}
		}
	}

	public int slainEnemyCount(string itemName) {
		if (!isAccepted()) return 0;
		return player.questSlayCounter[id][itemName];
	}

	public void getRewards() {
		foreach (string rewardName in rewardItems) {
			player.inventory.Add(XmlParse.instance.getItem(rewardName));
		}
		foreach (string rewardName in rewardSpells) {
			player.spellList.Add(Globals.instance.getSpell(rewardName, player));
		}
		foreach (string rewardName in rewardComboSpells) {
			player.comboSpells.Add(Globals.instance.getSpell(rewardName, player));
		}
		foreach (string rewardName in rewardPowers) {
			player.powers.Add(Globals.instance.getPower(rewardName, player));
		}
		player.gold += rewardGold;
		player.xp += rewardXP;
	}

	public virtual void setTooltipText() {
		// Tooltip text for quests.
		// Coloring: ^CRRGGBBAA*text*
		tooltipText = "^C000000ffQUEST: " + questName + "\n\n";
		tooltipText += description + "\n\n";
		tooltipText += "Requirements:\n";
		if (reachLevel != 0) {
			tooltipText += player.level < reachLevel ? "^CBD0000ff" : "^C018F2Cff";
			tooltipText += "-Reach level " + reachLevel + "\n";
		}
		foreach (string spellName in learnSpells) {
			tooltipText += !learnedSpell(spellName) ? "^CBD0000ff" : "^C018F2Cff";
			tooltipText += "-Learn '" + spellName + "' spell\n";
		}
		for (int i = 0; i < collectItems.Length; i++) {
			int count = collectedItemCount(collectItems[i]);
			tooltipText += count < collectItemCounts[i] ? "^CBD0000ff" : "^C018F2Cff";
			tooltipText += "-Collect item '" + collectItems[i] + "' (" + count + "/" + collectItemCounts[i] + ")\n";
		}
		for (int i = 0; i < slayEnemies.Length; i++) {
			int count = slainEnemyCount(slayEnemies[i]);
			tooltipText += count < slayEnemyCounts[i] ? "^CBD0000ff" : "^C018F2Cff";
			tooltipText += "-Slay '" + slayEnemies[i] + "' (" + count + "/" + slayEnemyCounts[i] + ")\n";
		}
		tooltipText += "\n^C000000ffRewards:\n";
		foreach (string rewardName in rewardItems) {
			tooltipText += "-'" + rewardName + "' item\n";
		}
		foreach (string rewardName in rewardSpells) {
			tooltipText += "-'" + rewardName + "' spell\n";
		}
		foreach (string rewardName in rewardComboSpells) {
			tooltipText += "-'" + rewardName + "' combo spell\n";
		}
		foreach (string rewardName in rewardPowers) {
			tooltipText += "-'" + rewardName + "' power\n";
		}
		if (rewardGold != 0) tooltipText += "-" + rewardGold + " gold\n";
		if (rewardXP != 0) tooltipText += "-" + rewardXP + " XP\n";
	}
	
	void setLockInfoText() {
		tooltipText = "^Cbab14aff" + questName;
		if (unlocksAtLevel > 0) {
			tooltipText += "\n^CffffffffUnlocks at Level: " + unlocksAtLevel;
		}
	}
	
	public void update() {
		if (player.level >= unlocksAtLevel) {
			isEnabled = true;
			s_button.color = new Color(1f, 1f, 1f, 1f);
			collider.enabled = true;
		} else {
			if (isEnabled == false) return;
			isEnabled = false;
			s_button.color = new Color(1f, 1f, 1f, 0f);
			collider.enabled = false;
		}
		if (player.isCompletedQuest(id)) {
			s_button.SetSprite("map_sign_2");
			isCompleted = true;
		} else if (isAccepted ()) {
			s_button.SetSprite("map_sign_3");
		} else {
			s_button.SetSprite("map_sign_1");
		}
	}

	public string getTooltipText() {
		return tooltipText;
	}
}
