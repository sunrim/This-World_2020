using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    private DatabaseManager theDatabase;
    private OrderManager theOrder;
    private AudioManager theAudio;

    public string key_sound;
    public string enter_sound;
    public string cancel_sound;
    public string open_sound;
    public string beep_sound;

    private InventorySlot[] slots; // 인벤토리 슬롯들

    private List<Item> inventoryItemList; // 플레이어가 소지한 아이템 리스트.

    public Text Description_Text; // 부연 설명.

    public Transform tf; // slot 부모객체.

    public GameObject go; // 인벤토리 활성화 비활성화.
    public GameObject go_OOC; // 선택지 활성화 비활성화.

    private int selectedItem; // 선택된 아이템.

    private bool activated = false; // 인벤토리 활성화시 true;
    private bool itemActivated; // 아이템 활성화시 true.
    private bool stopKeyInput; // 키입력 제한 (소비할 때 질의가 나올 텐데, 그 때 키입력 방지)
    private bool preventExec; // 중복실행 제한.

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);


    // Use this for initialization
    void Start()
    {
        instance = this;
        theAudio = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theDatabase = FindObjectOfType<DatabaseManager>();

        inventoryItemList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
        inventoryItemList.Add(new Item(00001, "망치", "무언가를 부실 수 있을 것 같다." ));
        inventoryItemList.Add(new Item(00002, "교무실 열쇠", "잠긴 곳을 열 수 있을 것 같다." ));
        inventoryItemList.Add(new Item(00003, "과학실 열쇠", "잠긴 곳을 열 수 있을 것 같다." ));
        inventoryItemList.Add(new Item(00004, "보건실 열쇠", "잠긴 곳을 열 수 있을 것 같다." ));
        inventoryItemList.Add(new Item(00005, "실습실 열쇠", "잠긴 곳을 열 수 있을 것 같다." ));
        inventoryItemList.Add(new Item(00006, "조교실 열쇠", "잠긴 곳을 열 수 있을 것 같다." ));
        inventoryItemList.Add(new Item(00007, "기사실 열쇠", "잠긴 곳을 열 수 있을 것 같다." ));
        inventoryItemList.Add(new Item(00008, "의약품", "뭐든지 치료할 수 있을 것 같다." ));
        inventoryItemList.Add(new Item(00009, "쪽지", "누군가가 남긴 쪽지." ));
    }
    
    
    
    

    public void ShowItem()
    {
        
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].Additem(inventoryItemList[i]);
             } // 인벤토리 탭 리스트의 내용을, 인벤토리 슬롯에 추가

        Console.WriteLine(inventoryItemList.Count);
        SelectedItem();
    } // 아이템 활성화 (inventoryTabList에 조건에 맞는 아이템들만 넣어주고, 인벤토리 슬롯에 출력)

    public void SelectedItem()
    {

        StopAllCoroutines();
        if (inventoryItemList.Count > 0)
        {
                Color color = slots[0].selected_Item.GetComponent<Image>().color;
                color.a = 0f;

            for (int i = 0; i < inventoryItemList.Count; i++)
                slots[i].selected_Item.GetComponent<Image>().color = color;
                Description_Text.text = inventoryItemList[selectedItem].itemDescription;
                StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
            Description_Text.text = "해당 타입의 아이템을 소유하고 있지 않습니다.";
    } // 선택된 아이템을 제외하고, 다른 모든 탭의 컬러 알파값을 0으로 조정.

    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.3f);
        }
    } // 선택된 아이템 반짝임 효과.

    // Update is called once per frame
    void Update()
    {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;

                if (activated)
                {
                    theAudio.Play(open_sound);
                    theOrder.NotMove();
                    go.SetActive(true);
                    selectedItem = 0;
                    itemActivated = true;
                    ShowItem();
                }
                else
                {
                    theAudio.Play(cancel_sound);
                    StopAllCoroutines();
                    go.SetActive(false);
                    itemActivated = false;
                    theOrder.Move();
                }
            } // 인벤토리 열고 닫기

            if (activated)
            {
                if (itemActivated)
                {
                    if (inventoryItemList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryItemList.Count - 2)
                                selectedItem += 2;
                            else
                                selectedItem %= 2;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 2;

                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryItemList.Count - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;

                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                        {



                        }

                    }
                } // 아이템 활성화시 키입력 처리.

                if (Input.GetKeyUp(KeyCode.Z)) // 중복 실행 방지.
                    preventExec = false;
            } // 인벤토리가 열리면 키입력처리 활성화.
        }
    }
    
}
