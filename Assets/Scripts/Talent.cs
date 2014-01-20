using UnityEngine;
using System.Collections;

public class Talent : MonoBehaviour {

	private int rank;
	private string tooltipText;

	public int id; // Must be same with the index in "talents" list in "TownController" class
	public string talentName;

	public int prereqLevel;
	public int[] prereqTalents;

	public TalentRank[] ranks;

	// -----GUI------
	private tk2dUIItem uiItem;
	private tk2dSprite s_border;
	private tk2dSprite s_avatar;
	private tk2dTextMesh t_rank;
	// --------------

	void Start() {
		Player player = GameSaveController.instance.getPlayer();
		rank = player.getTalentRank(id);
		uiItem = GetComponent<tk2dUIItem>();
		s_border = GetComponent<tk2dSprite>();
		s_avatar = transform.Find("Avatar").GetComponent<tk2dSprite>();
		t_rank = transform.Find("Rank").GetComponent<tk2dTextMesh>();
		uiItem.OnDown += OnDown;
		uiItem.OnRelease += OnRelease;
		uiItem.OnClick += OnClick;
	}

	public void activate() {
		Player player = GameSaveController.instance.getPlayer();
		if (!checkPrerequisites()) return;
		if (allRanksActive()) return;
		ranks[rank].activate();
		rank++;
		player.tp--;
		player.setTalentRank(id, rank);
		GameSaveController.instance.saveGame();
		TownController.instance.updateTalentGUI();
		SpellListController.instance.refreshSpellList();
		SpellListController.instance.refreshComboList();
		SpellListController.instance.refreshPowerList();
	}

	public void deactivate() {
		Player player = GameSaveController.instance.getPlayer();
		while (rank > 0) {
			rank--;
			ranks[rank].deactivate();
			player.tp++;
		}
		player.setTalentRank(id, rank);
		GameSaveController.instance.saveGame();
		TownController.instance.updateTalentGUI();
		SpellListController.instance.refreshSpellList();
		SpellListController.instance.refreshComboList();
		SpellListController.instance.refreshPowerList();
	}

	public bool allRanksActive() {
		if (rank == ranks.Length) return true;
		return false;
	}

	public bool checkPrerequisites() {
		Player player = GameSaveController.instance.getPlayer();
		if (prereqLevel > player.level) {
			return false;
		}
		foreach (int talentID in prereqTalents) {
			if (!TownController.instance.talents[talentID].allRanksActive())
				return false;
		}
		if (player.tp <= 0) return false;
		return true;
	}

	public virtual void setTooltipText() {
		Player player = GameSaveController.instance.getPlayer();
		// Tooltip text for talent.
		// Coloring: ^CRRGGBBAA*text*
		tooltipText = "^CB647BAff" + talentName;
		if (prereqLevel > player.level) {
			tooltipText += "\n^CED5555ffRequires Level: " + prereqLevel;
		}
		foreach (int talentID in prereqTalents) {
			Talent reqTalent = TownController.instance.talents[talentID];
			if (!reqTalent.allRanksActive()) {
				tooltipText += "\n^CED5555ffRequires Talent: " + reqTalent.talentName;
			}
		}
		if (rank != 0) {
			tooltipText += "\n^Cffffffff" + ranks[rank-1].description + "\n";
		}
		if (!allRanksActive()) {
			tooltipText += "\n^C8F8F8FffNext Rank:\n" + ranks[rank].description + "\n";
		}
	}

	// -----GUI------
	void OnDown() {
		Invoke("OnHold", .5f);
	}
	
	void OnRelease() {
		if (IsInvoking("OnHold")) {
			CancelInvoke("OnHold");
		}
	}

	void OnClick() {
		if (IsInvoking("DoubleClickExpired")) {
			CancelInvoke("DoubleClickExpired");
			OnDoubleClick();
		} else {
			Invoke("DoubleClickExpired", .7f);
		}
	}

	void OnDoubleClick() {
		activate();
	}

	void DoubleClickExpired() {}
	
	void OnHold() {
		setTooltipText();
		Tooltip.instance.setText(tooltipText);
		Tooltip.instance.showTooltip(transform.position);
	}

	public void refreshGUI() {
		t_rank.text = rank + "/" + ranks.Length;
		t_rank.Commit();
		if (rank > 0) {
			if (allRanksActive()) {
				s_border.color = new Color(.37f,1f,.23f,1f);
			} else {
				s_border.color = new Color(1f,1f,.23f,1f);
			}
		} else {

			s_border.color = new Color(.35f,.35f,.35f,1f);
		}
		if (checkPrerequisites()) {
			s_avatar.color = new Color(1f,1f,1f,1f);
		} else {
			s_avatar.color = new Color(.3f,.3f,.3f,1f);
		}
	}
	// --------------
}
