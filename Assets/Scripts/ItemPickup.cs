using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public int itemID;
    public int _count;
    public string pickUpSound;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.instance.Play(pickUpSound);
            //인벤토리에 추가
            Inventory.instance.GetAnItem(itemID, _count);
;            Destroy(this.gameObject);
        }
    }
}
