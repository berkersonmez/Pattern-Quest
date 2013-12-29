using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour {

	public int id; // Must be same with the index in "quests" list in "TownController" class
	public string questName;
	public string description;

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
	public GameObject questParchementPrefab;
	
	void Start() {
		uiItem = GetComponent<tk2dUIItem>();
		uiItem.OnClick += OnClick;
	}

	void OnClick() {
		setTooltipText();
		GameObject questParchement = Instantiate(questParchementPrefab) as GameObject;
		questParchement.transform.parent = this.transform.parent;
		questParchement.transform.localPosition = new Vector3(0f, 0f, -1f);
		tk2dTextMesh parchementText = questParchement.transform.Find("Text").GetComponent<tk2dTextMesh>();
		parchementText.text = tooltipText;
		parchementText.Commit();
		QuestButton qButton = questParchement.transform.Find("AcceptDeclineButton").GetComponent<QuestButton>();
		qButton.initialize(this);
		qButton = questParchement.transform.Find("BackButton").GetComponent<QuestButton>();
		qButton.initialize(this);
	}

	public bool isAccepted() {
		Player player = GameSaveController.instance.getPlayer();
		return player.questSlayCounter.ContainsKey(id);
	}

	public void accept() {
		Player player = GameSaveController.instance.getPlayer();
		player.questSlayCounter.Add(id, new Dictionary<string, int>());
		foreach(string creatureName in slayEnemies) {
			player.questSlayCounter[id].Add(creatureName, 0);
		}
	}

	public void decline() {
		Player player = GameSaveController.instance.getPlayer();
		player.questSlayCounter.Remove(id);

	}

	public void complete() {
		Player player = GameSaveController.instance.getPlayer();
		player.questSlayCounter.Remove(id);
		removeTargetedItems();
		getRewards();
		GameSaveController.instance.saveGame();
		InventoryController.instance.refreshItemList();
		SpellListController.instance.refreshSpellList();
		SpellListController.instance.refreshComboList();
		SpellListController.instance.refreshPowerList();
		TownController.instance.updateTexts();
	}

	public bool checkCompletion() {
		Player player = GameSaveController.instance.getPlayer();

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
		Player player = GameSaveController.instance.getPlayer();
		bool found = false;
		foreach (Spell spell in player.spellList) {
			if (spell.name == spellName) {
				found = true;
				break;
			}
		}
		return found;
	}

	public int collectedItemCount(string itemName) {
		Player player = GameSaveController.instance.getPlayer();
		int count = 0;
		foreach (Item playerItem in player.inventory) {
			if (playerItem.name == itemName) count++;
		}
		return count;
	}

	public void removeTargetedItems() {
		if (removeCollectedItems) {
			Player player = GameSaveController.instance.getPlayer();
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
		Player player = GameSaveController.instance.getPlayer();
		return player.questSlayCounter[id][itemName];
	}

	public void getRewards() {
		Player player = GameSaveController.instance.getPlayer();
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
			Player player = GameSaveController.instance.getPlayer();
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
}
