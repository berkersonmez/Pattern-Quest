using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveController : MonoBehaviour {
	
	[System.Serializable]
	public class GameSave {
		public Player player;
		public Stats stats;
		// Other variables to save goes here.
	}
	
	public static GameSaveController instance;
	
	public GameSave currentGame;
	public static BinaryFormatter bf = new BinaryFormatter();
	public Player player = null;
	public Stats stats;
	public int currentSlot;
	
	void Awake () {
		instance = this;
		player = null;
	}
	
	public bool loadGame(int slot) {
		currentGame = load<GameSave>("GameSave" + slot);
		if (currentGame == null) {
			return false;
		}
		player = getPlayerOverride();
		stats = getStatsOverride();
		currentSlot = slot;
		return true;
	}
	
	public bool loadGame(int slot, out Player slotPlayer) {
		GameSave slotGame = load<GameSave>("GameSave" + slot);
		if (slotGame == null) {
			slotPlayer = null;
			return false;
		}
		slotPlayer = getPlayerOverride(slotGame);
		stats = getStatsOverride();
		currentSlot = slot;
		return true;
	}
	
	public void makeNewSave(string playerName, int slot, string avatarSpriteName) {
		currentGame = new GameSave();
		player = new Player();
		player.name = playerName;
		player.spriteName = avatarSpriteName;
		Spell spell = new FireBall();
		player.learnSpell(spell);
		spell = new Heal();
		player.learnSpell(spell);
		spell = new NonEndingFire();
		player.learnSpell(spell);
		spell = new BasicAttack();
		player.learnSpell(spell);
		spell = new SoulRipper();
		player.learnSpell(spell);
		spell = new Douball();
		spell.owner = player;
		player.comboSpells.Add(spell);
		spell = new Clarity();
		spell.owner = player;
		player.comboSpells.Add(spell);
		spell = new Happiness();
		spell.owner = player;
		player.comboSpells.Add(spell);
		Power power = new IncreaseDamage();
		power.owner = player;
		player.powers.Add(power);
		player.level = 1;
		player.tp = 10; // TEST
		player.xp = 0;
		player.gold = 0;
		player.crystal = 0;
		player.seenTutorial = false;
		
		stats = new Stats();
		
		currentSlot = slot;
		saveGame();
	}
	
	// Collect data to "currentGame" in this method before save.
	public void saveGame() {
		currentGame.player = player;
		currentGame.stats = stats;
		save("GameSave" + currentSlot, currentGame);
	}
	
	// Gets player lazily
	public Player getPlayer() {
		if (player == null) {
			player = currentGame.player;
		}
		return player;
	}
	
	public Stats getStats() {
		if (stats == null) {
			stats = currentGame.stats;
		}
		return stats;
	}
	
	// Gets player directly
	public Player getPlayerOverride() {
		player = currentGame.player;
		return currentGame.player;
	}
	
	public Stats getStatsOverride() {
		stats = currentGame.stats;
		return stats;
	}
	
	public Player getPlayerOverride(GameSave slotGame) {
		return slotGame.player;
	}
	
	public void deleteSave(int slot) {
		PlayerPrefs.DeleteKey("GameSave" + slot);
	}

    public void save(string prefKey, object serializableObject) {
        MemoryStream memoryStream = new MemoryStream();
        bf.Serialize(memoryStream, serializableObject);
        string tmp = System.Convert.ToBase64String (memoryStream.ToArray());
        PlayerPrefs.SetString(prefKey, tmp);
    }

    public T load<T>(string prefKey) {
	    if (!PlayerPrefs.HasKey(prefKey))
	        return default(T);
	    string serializedData = PlayerPrefs.GetString(prefKey);
	    MemoryStream dataStream = new MemoryStream(System.Convert.FromBase64String(serializedData));
	    T deserializedObject = (T)bf.Deserialize(dataStream);
	    return deserializedObject;
	}

	public int xpRequiredForLevel(int level) {
		return (int) (Globals.instance.xpStartingReq * Mathf.Pow(Globals.instance.xpReqMultiplier, level - 2));
	}
}
