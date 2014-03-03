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
		// Other variables to save goes here.
	}
	
	public static GameSaveController instance;
	
	public GameSave currentGame;
	public static BinaryFormatter bf = new BinaryFormatter();
	public Player player = null;
	
	void Start () {
		instance = this;
		player = null;
	}
	
	public bool loadGame() {
		currentGame = load<GameSave>("GameSave");
		if (currentGame == null) {
			return false;
		}
		player = getPlayer();
		return true;
	}
	
	public void makeNewSave(string playerName) {
		currentGame = new GameSave();
		player = new Player();
		player.name = playerName;
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
		spell = new LifeDrain();
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
		saveGame();
	}
	
	// Collect data to "currentGame" in this method before save.
	public void saveGame() {
		currentGame.player = player;
		save("GameSave", currentGame);
	}
	
	public Player getPlayer() {
		if (player == null) {
			player = currentGame.player;
		}
		return player;
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
