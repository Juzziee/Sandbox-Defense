using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {
	private List<Item> database = new List<Item>();
	private JsonData itemData;

	void Start(){
		itemData = JsonMapper.ToObject (File.ReadAllText(Application.dataPath + "/StreamingAssets/ItemDatabase.json"));
		ConstructItemDatabase ();
	}

	public Item FetchItemByID(int id){
		for (int i = 0; i < database.Count; i++) {
			if (database[i].ID == id) {
				return database [i];
			}
		}
		return null;
	}

	void ConstructItemDatabase (){
		for (int i = 0; i < itemData.Count; i++) {
			database.Add (new Item (
				(int)itemData[i]["id"], 
				itemData[i]["title"].ToString (), 
				itemData[i]["description"].ToString (),
				(bool)itemData[i]["equipable"],
				(bool)itemData[i]["stackable"],
				(int)itemData[i]["stacksize"],
				(int)itemData[i]["stats"]["damage"],
				(int)itemData[i]["stats"]["speed"],
				(int)itemData[i]["stats"]["durablity"],
				itemData[i]["slug"].ToString()));
				
		}
	}

	public class Item {
		public int ID { get; set; }
		public string Title { get; set; }
		public string Decription { get; set; }
		public bool Equipable { get; set; }
		public bool Stackable { get; set; }
		public int stackSize { get; set; }
		public int Damage { get; set; }
		public float Speed { get; set; }
		public float Durability { get; set; }
		public string Slug { get; set; }
		public Sprite Sprite { get; set; } 

		public Item (int id, string title, string description, bool equipable, bool stackable, int stacksize, int damage, float speed, float durablity, string slug){
			this.ID = id;
			this.Title = title;
			this.Decription = description;
			this.Equipable = equipable;
			this.Stackable = stackable;
			this.stackSize = stacksize;
			this.Damage = damage;
			this.Speed = speed;
			this.Durability = durablity;
			this.Slug = slug;
			this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + Slug);
		}

		public Item(){
			this.ID = -1;
		}
	}
}
