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
		
		foreach (XmlNode oneItem in xmlItems) {
  			XmlNodeList itemContentList = oneItem.ChildNodes;
  			Item obj = new Item(); 
    
   			foreach (XmlNode itemContent in itemContentList) {
    			if(itemContent.Name == "name")
    				obj.name = itemContent.InnerText;
    			if(itemContent.Name == "damage")
    				obj.damage = int.Parse(itemContent.InnerText);
				if(itemContent.Name == "spellPower")
    				obj.spellPower = int.Parse(itemContent.InnerText);
				if(itemContent.Name == "type")
    				obj.type = itemContent.InnerText;
				if(itemContent.Name == "armor")
    				obj.armor = int.Parse(itemContent.InnerText);
				if(itemContent.Name == "hp")
    				obj.hp = int.Parse(itemContent.InnerText);
				if(itemContent.Name == "mana")
    				obj.mana = int.Parse(itemContent.InnerText);
				if(itemContent.Name == "level")
    				obj.level = int.Parse(itemContent.InnerText);
		   }
		   itemList.Add(obj);
			
  		}
	}
	
	// Use this for initialization
	void Start () {
		getItem ();
		Debug.Log("(name:" + itemList[0].name + "damage:" + itemList[0].damage + ")");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
