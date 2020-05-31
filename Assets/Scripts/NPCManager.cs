using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class NPCMove
{
    //NPC의 움직임을 담당하는 클래스
    [Tooltip("NPCMove를 체크하면 NPC가 움직임")]
    public bool NPCmove;

    public string[] direction;      //npc가 움직일 방향 설정

    [Range(1, 5)] [Tooltip("1 = 천천히, 2 = 조금 천천히, 3 = 보통, 4 = 빠르게, 5 = 연속적")]
    public int frequency;       //npc가 움직일 방향의 속도
}

public class NPCManager : MovingObject
{
    [SerializeField]
    public NPCMove npc;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void SetMove()
    {
    }

    public void SetNotMove()
    {

    }

    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for(int i = 0;i < npc.direction.Length; i++)
            {
                switch (npc.frequency)
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:

                        break;
                }
                yield return new WaitUntil(() => npcCanMove);

                //실질적인 이동구간
                base.Move(npc.direction[i], npc.frequency);

                if (i == npc.direction.Length - 1)
                    i = -1;
            }
        }
    }
}
