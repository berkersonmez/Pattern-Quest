using UnityEngine;
using System.Collections;

public class StatsController : MonoBehaviour {
	public static StatsController instance;

	private Avatar avatar;
	private tk2dTextMesh textDamage;
	private tk2dTextMesh textSpellDmg;
	private tk2dTextMesh textArmor;
	private tk2dTextMesh textHpRegen;
	private tk2dTextMesh textManaRegen;
	private tk2dTextMesh textCritChance;
	private tk2dTextMesh textStats;

	void Start () {
		instance = this;
		avatar = transform.Find("Avatar Player").GetComponent<Avatar>();
		Transform texts = transform.Find("Texts");
		textDamage = texts.Find("Damage").GetComponent<tk2dTextMesh>();
		textSpellDmg = texts.Find("Spell Damage").GetComponent<tk2dTextMesh>();
		textArmor = texts.Find("Armor").GetComponent<tk2dTextMesh>();
		textHpRegen = texts.Find("HP Regen").GetComponent<tk2dTextMesh>();
		textManaRegen = texts.Find("Mana Regen").GetComponent<tk2dTextMesh>();
		textCritChance = texts.Find("Crit Chance").GetComponent<tk2dTextMesh>();
		textStats = transform.Find("Texts General").Find("Stats").GetComponent<tk2dTextMesh>();
	}

	public void updateStats() {
		if (avatar.owner == null) {
			avatar.setOwner(GameSaveController.instance.getPlayer());
		}
		avatar.owner.restoreHealthMana();
		avatar.updateHealthAndMana();
		textDamage.text = "Damage: " + avatar.owner.Damage;
		textSpellDmg.text = "Spell Power: " + avatar.owner.SpellPower;
		textArmor.text = "Armor: " + avatar.owner.Armor;
		textHpRegen.text = "HP Regen: " + avatar.owner.HpRegen + "/turn";
		textManaRegen.text = "Mana Regen: " + avatar.owner.ManaRegen + "/turn";
		textCritChance.text = "Crit. Chance: " + avatar.owner.criticalStrikeChance + "%";
		textStats.text = GameSaveController.instance.getStats().getStatsText();
		textDamage.Commit();
		textSpellDmg.Commit();
		textArmor.Commit();
		textHpRegen.Commit();
		textManaRegen.Commit();
		textCritChance.Commit();
		textStats.Commit();
	}
	
}
