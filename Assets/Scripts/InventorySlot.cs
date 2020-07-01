using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD
public class NewBehaviourScript : MonoBehaviour
{
=======
public class InventorySlot : MonoBehaviour{
>>>>>>> a53f5b54ad30bb8153663a3c4fa55abb44fdf8b8
    public Image icon;
    public Text itemName_Text;
    public Text itemCount_Text;
    public GameObject selected_Item;

    public void Additem(Item _item)
    {
        itemName_Text.text = _item.itemName;
        icon.sprite = _item.itemIcon;
    }

    public void RemoveItem()
    {
        itemName_Text.text = "";
        icon.sprite = null;
    }
 }

