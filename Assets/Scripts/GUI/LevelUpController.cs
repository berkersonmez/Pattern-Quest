using UnityEngine;
using System.Collections;

public class LevelUpController : MonoBehaviour {

	private Player player;
	private int statPoints;
	[HideInInspector]
	public int statPointLeft;
	public tk2dTextMesh t_spLeft;

	public tk2dTextMesh t_Health;
	public tk2dTextMesh t_Mana;
	public tk2dTextMesh t_HpRegen;
	public tk2dTextMesh t_ManaRegen;
	public tk2dTextMesh t_Damage;
	public tk2dTextMesh t_CritChance;
	public int incrementHealth;
	public int incrementMana;
	public int incrementHpRegen;
	public int incrementManaRegen;
	public int incrementDamage;
	public int incrementCritChance;
	public StatIncrementButton b_Health;
	public StatIncrementButton b_Mana;
	public StatIncrementButton b_HpRegen;
	public StatIncrementButton b_ManaRegen;
	public StatIncrementButton b_Damage;
	public StatIncrementButton b_CritChance;

	public tk2dUIItem uiItem_Apply;
	public tk2dUIItem uiItem_Clear;

	void Start () {
		initialize(1);

	}

	public void initialize(int levelUpCount) {
		statPoints = levelUpCount * Globals.instance.statPointsPerLevel;
		statPointLeft = statPoints;
		updateSpLeftText();

		player = GameSaveController.instance.getPlayer();

		tk2dTextMesh t_incHealth = t_Health.transform.Find("AttrIncrement").GetComponent<tk2dTextMesh>();
		t_Health.text = "Health: " + player.hp;
		t_Health.Commit();
		t_incHealth.text = "(+" + incrementHealth + ") x";
		t_incHealth.Commit();

		tk2dTextMesh t_incMana = t_Mana.transform.Find("AttrIncrement").GetComponent<tk2dTextMesh>();
		t_Mana.text = "Mana: " + player.mana;
		t_Mana.Commit();
		t_incMana.text = "(+" + incrementMana + ") x";
		t_incMana.Commit();

		tk2dTextMesh t_incHpRegen = t_HpRegen.transform.Find("AttrIncrement").GetComponent<tk2dTextMesh>();
		t_HpRegen.text = "HP Regen: " + player.hpRegen;
		t_HpRegen.Commit();
		t_incHpRegen.text = "(+" + incrementHpRegen + ") x";
		t_incHpRegen.Commit();

		tk2dTextMesh t_incManaRegen = t_ManaRegen.transform.Find("AttrIncrement").GetComponent<tk2dTextMesh>();
		t_ManaRegen.text = "Mana Regen: " + player.manaRegen;
		t_ManaRegen.Commit();
		t_incManaRegen.text = "(+" + incrementManaRegen + ") x";
		t_incManaRegen.Commit();

		tk2dTextMesh t_incDamage = t_Damage.transform.Find("AttrIncrement").GetComponent<tk2dTextMesh>();
		t_Damage.text = "Damage: " + player.damage;
		t_Damage.Commit();
		t_incDamage.text = "(+" + incrementDamage + ") x";
		t_incDamage.Commit();

		tk2dTextMesh t_incCritChance = t_CritChance.transform.Find("AttrIncrement").GetComponent<tk2dTextMesh>();
		t_CritChance.text = "Crit. Chance: " + player.criticalStrikeChance;
		t_CritChance.Commit();
		t_incCritChance.text = "(+" + incrementCritChance + ") x";
		t_incCritChance.Commit();

		uiItem_Apply.OnClick += OnApply;
		uiItem_Clear.OnClick += OnClear;
	}

	void OnApply() {
		player.hp += incrementHealth * b_Health.spGiven;
		player.mana += incrementMana * b_Mana.spGiven;
		player.hpRegen += incrementHpRegen * b_HpRegen.spGiven;
		player.manaRegen += incrementManaRegen * b_ManaRegen.spGiven;
		player.damage += incrementDamage * b_Damage.spGiven;
		player.criticalStrikeChance += incrementCritChance * b_CritChance.spGiven;
		StatsController.instance.updateStats();
		GameSaveController.instance.saveGame();
		Destroy(gameObject);
	}

	void OnClear() {
		b_Health.spGiven = 0;
		b_Health.updateSpGivenText();
		b_Mana.spGiven = 0;
		b_Mana.updateSpGivenText();
		b_HpRegen.spGiven = 0;
		b_HpRegen.updateSpGivenText();
		b_ManaRegen.spGiven = 0;
		b_ManaRegen.updateSpGivenText();
		b_Damage.spGiven = 0;
		b_Damage.updateSpGivenText();
		b_CritChance.spGiven = 0;
		b_CritChance.updateSpGivenText();
		statPointLeft = statPoints;
		updateSpLeftText();
	}

	public void updateSpLeftText() {
		t_spLeft.text = "Stat Points Left: " + statPointLeft;
	}

}
