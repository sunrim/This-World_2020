using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{

    public int itemID;//아이템의 고유ID 중복 불가능
    public string itemName; // 아이템 이름 중복가능
    public string itemDescription;
    public int itemCount;
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType{
        Use,
        Equip,
        Quest,
        ETC
    }

    public Item(int _itemID, string _itemName, string _itemDes, int _itemCount=1){
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemCount = _itemCount;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
}