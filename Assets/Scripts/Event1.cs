using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!flag && Input.GetKey(KeyCode.Z))
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("NPC(B)", "RIGHT");
    }
}
