using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

public class XmlParse : MonoBehaviour {
	
	public static XmlParse instance;
	
	public TextAsset itemXML;
	public TextAsset spellXML;
	public TextAsset creatureXML;
	public TextAsset mapXML;
	public TextAsset quizXML;
	public TextAsset storeXML;
	
	private XmlDocument _itemDoc;
	private XmlDocument _creatureDoc;
	private XmlDocument _quizDoc;
	private XmlDocument _mapDoc;
	private XmlDocument _storeDoc;
	
	public XmlDocument ItemDoc {
		get {
			if (_itemDoc == null) {
				_itemDoc = new XmlDocument();
				_itemDoc.LoadXml(itemXML.text);
			}
			return _itemDoc;
		}
	}
	
	public XmlDocument CreatureDoc {
		get {
			if (_creatureDoc == null) {
				_creatureDoc = new XmlDocument();
				_creatureDoc.LoadXml(creatureXML.text);
			}
			return _creatureDoc;
		}
	}
	
	public XmlDocument QuizDoc {
		get {
			if (_quizDoc == null) {
				_quizDoc = new XmlDocument();
				_quizDoc.LoadXml(quizXML.text);
			}
			return _quizDoc;
		}
	}
	
	public XmlDocument MapDoc {
		get {
			if (_mapDoc == null) {
				_mapDoc = new XmlDocument();
				_mapDoc.LoadXml(mapXML.text);
			}
			return _mapDoc;
		}
	}
	
	public XmlDocument StoreDoc {
		get {
			if (_storeDoc == null) {
				_storeDoc = new XmlDocument();
				_storeDoc.LoadXml(storeXML.text);
			}
			return _storeDoc;
		}
	}
	
	public List<Item> itemList = new List<Item>();
	public List<Spell> spellList = new List<Spell>();
	
	void Awake() {
		instance = this;
	}
	
	public Creature getCreature(string creatureName){
		Creature obj = new Creature();
		
		XmlNode datNode = CreatureDoc.SelectSingleNode("/creatures/creature[name='" + creatureName + "']");
		XmlNodeList creatureContentList = datNode.ChildNodes;
		
		foreach (XmlNode creatureContent in creatureContentList) {
			if(creatureContent.Name == "name")
				obj.name = creatureContent.InnerText;
			if(creatureContent.Name == "damage"){
				obj.damage = getValue(creatureContent.InnerText);
				Spell spell = new Spell(obj.damage);
				spell.owner = obj;
				obj.spellList.Add(spell);
			}
			if(creatureContent.Name == "hp")
				obj.hp = getValue(creatureContent.InnerText);	
			if(creatureContent.Name == "spellPower")
				obj.spellPower = getValue(creatureContent.InnerText);	
			if(creatureContent.Name == "level")
				obj.level = getValue(creatureContent.InnerText);	
			if(creatureContent.Name == "type")
				obj.type = creatureContent.InnerText;
			if(creatureContent.Name == "spriteName")
				obj.spriteName = creatureContent.InnerText;
			if(creatureContent.Name == "gold")
				obj.gold = getValue(creatureContent.InnerText);
			if(creatureContent.Name == "items"){
				XmlNodeList xmlItems = creatureContent.ChildNodes;
				string itemName = determineDroppedItem(xmlItems);
				Item droppedItem = getItem(itemName);
				if(droppedItem == null){
					Debug.Log("Item dusmedi :((");
					continue;
				}
				obj.droppedItems.Add(droppedItem);
				Debug.Log(obj.droppedItems[0].name);
			}
			if(creatureContent.Name == "spellList"){
				List<string> spellNamesList = new List<string>();
				XmlNodeList xmlSpells = creatureContent.ChildNodes;
				foreach (XmlNode xmlSpell in xmlSpells) {
					Debug.Log("GIRDI");
					string className="";
					string allParameters="";
					className = getConstructor(xmlSpell.InnerText, ref allParameters);
					spellNamesList.Add(className);
					Spell newSpell = Globals.instance.createSpell(className, allParameters, obj);
					obj.spellList.Add(newSpell);
					Debug.Log(newSpell.name);
					}
				}
	 	 	}
		obj.setValues();
		return obj;
	}
	
	public List<StoreGood> getStoreGoods() {
		List<StoreGood> goods = new List<StoreGood>();
		XmlNodeList xmlGoods = StoreDoc.GetElementsByTagName("good");
		
		foreach (XmlNode xmlGood in xmlGoods) {
			StoreGood good = new StoreGood();
			good.name = xmlGood.SelectSingleNode("name").InnerText;
			if (good.name == "") continue;
			good.description = xmlGood.SelectSingleNode("description").InnerText;
			good.itemID = xmlGood.SelectSingleNode("itemID").InnerText;
			good.category = xmlGood.SelectSingleNode("category").InnerText;
			good.spriteName = xmlGood.SelectSingleNode("spriteName").InnerText;
			good.realName = xmlGood.SelectSingleNode("realName").InnerText;
			good.costType = xmlGood.SelectSingleNode("costType").InnerText;
			good.costAmount = int.Parse(xmlGood.SelectSingleNode("costAmount").InnerText);
			goods.Add(good);
		}
		return goods;
	}
	
	public List<StoreCurrencyPack> getStoreCurrencyPacks() {
		List<StoreCurrencyPack> packs = new List<StoreCurrencyPack>();
		XmlNodeList xmlGoods = StoreDoc.GetElementsByTagName("pack");
		
		foreach (XmlNode xmlGood in xmlGoods) {
			StoreCurrencyPack pack = new StoreCurrencyPack();
			pack.name = xmlGood.SelectSingleNode("name").InnerText;
			if (pack.name == "") continue;
			pack.description = xmlGood.SelectSingleNode("description").InnerText;
			pack.itemID = xmlGood.SelectSingleNode("itemID").InnerText;
			pack.productID = xmlGood.SelectSingleNode("productID").InnerText;
			pack.cost = xmlGood.SelectSingleNode("cost").InnerText;
			pack.size = int.Parse(xmlGood.SelectSingleNode("size").InnerText);
			pack.type = xmlGood.SelectSingleNode("type").InnerText;
			packs.Add(pack);
		}
		return packs;
	}
	
	public Question getRandomUnansweredQuestion() {
		Question question = new Question();
		XmlNodeList xmlQuestions = QuizDoc.GetElementsByTagName("question");
		
		var exclude = GameSaveController.instance.getPlayer().answeredQuestions;
		var range = Enumerable.Range(0, xmlQuestions.Count).Where(i => !exclude.Contains(i));
		int randomNode;
		try {
			randomNode = range.ElementAt(Random.Range(0, xmlQuestions.Count - exclude.Count));
		} catch (System.Exception e) {
			Debug.Log("Exception happened: " + e.Message);
			return null;
		}
		
		XmlNode qNode = xmlQuestions.Item(randomNode);
		question.id = randomNode;
		question.query = qNode.SelectSingleNode("query").InnerText;
		question.answer = int.Parse(qNode.SelectSingleNode("answer").InnerText);
		XmlNodeList choices = qNode.SelectSingleNode("choices").ChildNodes;
		foreach(XmlNode choice in choices) {
			question.addChoice(int.Parse(choice.SelectSingleNode("@id").Value), choice.InnerText);
		}
		return question;
	}
	
	
	
	public Item getItemByStoreId(string storeId) {
		Item obj = new Item();
		
		XmlNode datNode = ItemDoc.SelectSingleNode("/items/item[storeId='" + storeId + "']");
		if(datNode == null)
			return null;
		
		XmlNodeList itemContentList = datNode.ChildNodes;
		
		foreach (XmlNode itemContent in itemContentList) {
			if(itemContent.Name == "name"){
				if(itemContent.InnerText != null)
					obj.name = itemContent.InnerText;		
			}
			if(itemContent.Name == "damage"){
				obj.damage = getValue(itemContent.InnerText);
			}
			if(itemContent.Name == "type"){
				obj.type = itemContent.InnerText;
			}
			if(itemContent.Name == "rarity")
				obj.rarity = itemContent.InnerText;
			if(itemContent.Name == "armor")
				obj.armor = getValue(itemContent.InnerText);
			if(itemContent.Name == "hp")
				obj.hp = getValue(itemContent.InnerText);
			if(itemContent.Name == "mana")
				obj.mana = getValue(itemContent.InnerText);
			if(itemContent.Name == "manaRegen")
				obj.manaRegen = getValue(itemContent.InnerText);
			if(itemContent.Name == "level")
				obj.level = getValue(itemContent.InnerText);
			if(itemContent.Name == "spriteName")
				obj.spriteName = itemContent.InnerText;
			if(itemContent.Name == "gold")
				obj.gold = getValue(itemContent.InnerText);
		}
		
		//Fixes empty and wrong values
		obj.setValues();
		return obj;
	}
	
	public Queue<string> getMapCreatures(int dungeonNo){
		XmlNodeList xmlCreatures = MapDoc.SelectNodes("dungeons/dungeon[@no = '"+ dungeonNo +"']/creatures/creature");
		
		Queue<string> creatureNames = new Queue<string>();
		
		foreach(XmlNode xmlCreature in xmlCreatures){
			creatureNames.Enqueue(xmlCreature.InnerText);
		}
		return creatureNames;
	}
	
	public void getSpell(){
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(spellXML.text);
		XmlNodeList xmlSpells = xmlDoc.GetElementsByTagName("spell");
		
		foreach(XmlNode oneSpell in xmlSpells){
			XmlNodeList spellContentList = oneSpell.ChildNodes;
			Spell obj = new Spell();
			
			foreach (XmlNode spellContent in spellContentList) {
				if(spellContent.Name == "name")
					obj.name = spellContent.InnerText;
				if(spellContent.Name == "damage")
					obj.damage = getValue(spellContent.InnerText);
				if(spellContent.Name == "damageOverTime")
					obj.damageOverTime = getValue(spellContent.InnerText);
				if(spellContent.Name == "mana")
					obj.mana = getValue(spellContent.InnerText);
				if(spellContent.Name == "heal")
					obj.heal = getValue(spellContent.InnerText);
				if(spellContent.Name == "healOverTime")
					obj.healOverTime = getValue(spellContent.InnerText);
				if(spellContent.Name == "level")
					obj.level = getValue(spellContent.InnerText);
				if(spellContent.Name == "maxLevel")
					obj.maxLevel = getValue(spellContent.InnerText);
				if(spellContent.Name == "turn")
					obj.turn = getValue(spellContent.InnerText);
				if(spellContent.Name == "shape")
					obj.turn = getValue(spellContent.InnerText);
		  	}
		}
		
	}
	
	public Item getItem(string name){
		Item obj = new Item();
		
		XmlNode datNode = ItemDoc.SelectSingleNode("/items/item[name='" + name + "']");
		if(datNode == null)
			return null;
		XmlNodeList itemContentList = datNode.ChildNodes;
		
		foreach (XmlNode itemContent in itemContentList) {
		if(itemContent.Name == "name"){
			if(itemContent.InnerText != null)
				obj.name = itemContent.InnerText;		
		}
		if(itemContent.Name == "damage"){
			obj.damage = getValue(itemContent.InnerText);
		}
		if(itemContent.Name == "type"){
			obj.type = itemContent.InnerText;
		}
		if(itemContent.Name == "rarity")
			obj.rarity = itemContent.InnerText;
		if(itemContent.Name == "armor")
			obj.armor = getValue(itemContent.InnerText);
		if(itemContent.Name == "hp")
			obj.hp = getValue(itemContent.InnerText);
		if(itemContent.Name == "mana")
			obj.mana = getValue(itemContent.InnerText);
		if(itemContent.Name == "manaRegen")
			obj.manaRegen = getValue(itemContent.InnerText);
		if(itemContent.Name == "level")
			obj.level = getValue(itemContent.InnerText);
		if(itemContent.Name == "spriteName")
			obj.spriteName = itemContent.InnerText;
		if(itemContent.Name == "gold")
			obj.gold = getValue(itemContent.InnerText);
	  	}


		//Fixes empty and wrong values
		obj.setValues();
	  	return obj;
	}
	
	public string determineDroppedItem(XmlNodeList xmlItems){
		int randomValue = Random.Range(1,100);
		foreach (XmlNode oneItem in xmlItems) {
			int dropChance = int.Parse(oneItem.Attributes["dropChance"].Value);
			if(randomValue <= dropChance)
				return oneItem.InnerText;
			else
				randomValue = randomValue - dropChance;
		}
		return null;
	}
	
	//Get true values for randomized variables
	public int getValue(string str){
		string[] values = str.Split(',');
		if(values.Length == 1)
			return int.Parse(values[0]);
		else{
			int val = Random.Range( int.Parse(values[0]) , int.Parse(values[1]) );
			return val;	
		}
	}

	public string getConstructor(string str, ref string allParameters){
		string[] values = str.Split(':');
		string className = values[0];
		if(values.Length == 1){
			allParameters = null;
			Debug.Log("NO PARAMETERS: " + allParameters);
			return className;
		}
		else{
			allParameters = values[1];

			return className;
		}
	}

	public string getType(int typeCode){
		switch(typeCode){
		case 1:
			return "weapon";
		case 2:
			return "head";
		case 3:
			return "necklace";
		case 4:
			return "shoulder";
		case 5:
			return "chest";
		case 6:
			return "wrist";
		case 7:
			return "gloves";
		case 8:
			return "waist";
		case 9:
			return "leg";
		case 10:
			return "boots";
		}
		return "typeless";
	}
}
