using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveController : MonoBehaviour {
	
	[System.Serializable]
	public class GameSave {
		public List<Item> inventory;
		public Item redStone;
		public Item blueStone;
		public Item greenStone;
		public List<string> spells = new List<string>();
		public int level;
		public int xp;
		public int gold;
		public string playerName;
		// Other variables to save goes here.
	}
	
	public static GameSaveController instance;
	
	public GameSave currentGame;
	public static BinaryFormatter bf = new BinaryFormatter();
	public Player player;
	
	void Start () {
		instance = this;
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
		player.spellList.Add(new FireBall());
		player.spellList.Add(new Poison());
		player.spellList.Add(new Spell(4));
		player.level = 1;
		saveGame();
	}
	
	// Collect data to "currentGame" in this method before save.
	public void saveGame() {
		currentGame.level = player.level;
		currentGame.playerName = player.name;
		currentGame.xp = player.xp;
		currentGame.gold = player.gold;
		currentGame.inventory = player.inventory;
		currentGame.redStone = player.redStone;
		currentGame.blueStone = player.blueStone;
		currentGame.greenStone = player.greenStone;
		foreach(Spell spell in player.spellList)
			currentGame.spells.Add(spell.name);
		save("GameSave", currentGame);
	}
	
	public Player getPlayer() {
		if (player == null) {
			player = new Player();
			player.level = currentGame.level;
			player.name = currentGame.playerName;
			player.xp = currentGame.xp;
			player.gold = currentGame.gold;
			player.inventory= currentGame.inventory;
			player.redStone= currentGame.redStone;
			player.blueStone = currentGame.blueStone;
			player.greenStone = currentGame.greenStone;
			player.spellList = Globals.instance.getSpells(currentGame.spells);
			player.spellList.Add(new Spell(player.damage));
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
}
