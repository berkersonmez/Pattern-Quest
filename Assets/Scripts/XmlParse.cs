using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class XmlParse : MonoBehaviour {
	
	public TextAsset itemXML;
	public List<Item> itemList = new List<Item>();
	
	public void getItem() {
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(itemXML.text);
		XmlNodeList xmlItems = xmlDoc.GetElementsByTagName("item");
		
		XmlNode droppedItem = determineDroppedItem(xmlItems);
		if(droppedItem == null){
			Debug.Log("NO ITEM DROPPED");
			return;
		}
		
		Debug.Log(droppedItem.Attributes["dropChance"].Value);
		
		XmlNodeList itemContentList = droppedItem.ChildNodes;
		Item obj = new Item(); 

		foreach (XmlNode itemContent in itemContentList) {
		if(itemContent.Name == "name"){
			if(itemContent.InnerText != null)
				obj.name = itemContent.InnerText;		
		}
		if(itemContent.Name == "damage"){
			obj.damage = getValue(itemContent.InnerText);
		}
		if(itemContent.Name == "spellPower")
			obj.spellPower = getValue(itemContent.InnerText);
		if(itemContent.Name == "type"){
			int typeCode = getValue(itemContent.InnerText);
			obj.type = getType(typeCode);
		}
		if(itemContent.Name == "rarity")
			obj.rarity = itemContent.InnerText;
		if(itemContent.Name == "armor")
			obj.armor = getValue(itemContent.InnerText);
		if(itemContent.Name == "hp")
			obj.hp = getValue(itemContent.InnerText);
		if(itemContent.Name == "mana")
			obj.mana = getValue(itemContent.InnerText);
		if(itemContent.Name == "level")
			obj.level = getValue(itemContent.InnerText);
	  	}
		//Fixes empty and wrong values
		obj.setValues();
	  	itemList.Add(obj);
	}
	
	public XmlNode determineDroppedItem(XmlNodeList xmlItems){
		int randomValue = Random.Range(1,100);
		foreach (XmlNode oneItem in xmlItems) {
			int dropChance = int.Parse(oneItem.Attributes["dropChance"].Value);
			if(randomValue <= dropChance)
				return oneItem;
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
	
	public string getType(int typeCode){
		switch(typeCode){
		case 1:
			return "weapon";
			break;
		case 2:
			return "head";
			break;
		case 3:
			return "necklace";
			break;
		case 4:
			return "shoulder";
			break;
		case 5:
			return "chest";
			break;
		case 6:
			return "wrist";
			break;
		case 7:
			return "gloves";
			break;
		case 8:
			return "waist";
			break;
		case 9:
			return "leg";
			break;
		case 10:
			return "boots";
			break;
		}
		return "typeless";
	}
	
	// Use this for initialization
	void Start () {
		getItem ();
		if(itemList.Count > 0)
			Debug.Log("(name:" + itemList[0].name + " - damage:" + itemList[0].damage + ")");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
