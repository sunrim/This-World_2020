using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(00001, "망치", "무언가를 부실 수 있을 것 같다.",  Item.ItemType.Use));
        itemList.Add(new Item(00002, "교무실 열쇠", "잠긴 곳을 열 수 있을 것 같다.",  Item.ItemType.Use)):
        itemList.Add(new Item(00003, "과학실 열쇠", "잠긴 곳을 열 수 있을 것 같다.",  Item.ItemType.Use));
        itemList.Add(new Item(00004, "보건실 열쇠", "잠긴 곳을 열 수 있을 것 같다.",  Item.ItemType.Use));
        itemList.Add(new Item(00005, "실습실 열쇠", "잠긴 곳을 열 수 있을 것 같다.",  Item.ItemType.Use));
        itemList.Add(new Item(00006, "조교실 열쇠", "잠긴 곳을 열 수 있을 것 같다.",  Item.ItemType.Use));
        itemList.Add(new Item(00007, "기사실 열쇠", "잠긴 곳을 열 수 있을 것 같다.",  Item.ItemType.Use));
        itemList.Add(new Item(00008, "의약품", "모든지 치료할 수 있을 것 같다.",  Item.ItemType.Use))
    }
}
