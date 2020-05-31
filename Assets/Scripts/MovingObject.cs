using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;
    public int walkCount;
    protected int currentWalkCount;

    protected bool npcCanMove = true;

    protected Vector3 vector;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;     //통과 불가능한 것을 설정해주는 것
    public Animator animator;

    protected void Move(string _dir, int _frequency)
    {
        StartCoroutine(MoveCoroutine(_dir, _frequency));
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        npcCanMove = false;
        vector.Set(0, 0, vector.z);
        switch (_dir)
        {
            case "LEFT":
                vector.x = 1f;
                break;
            case "RIGHT":
                vector.x = -1f;
                break;
        }

        animator.SetFloat("DirX", vector.x);
        animator.SetFloat("DirY", vector.y);
        animator.SetBool("Walking", true);

        while (currentWalkCount < walkCount)
        {
            transform.Translate(vector.x * speed, 0, 0);

            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }

        currentWalkCount = 0;
        if(_frequency != 5)
            animator.SetBool("Walking", false);
        npcCanMove = true;
    }

    protected bool CheckCollsion()
    {
        RaycastHit2D hit;       //방해물이 없으면 Null 반환, 방해물에 부딪히면 방해물 반환

        Vector2 start = transform.position;      //캐릭터의 현재 위치 값
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);        //캐릭터가 이동하고자 하는 위치 값

        boxCollider.enabled = false;        //캐릭터가 가지고 있는 고유의 박스 컬라이더를 잠깐 끄는 기능
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;         //꺼놨던 박스 컬라이더를 다시 켜는 기능

        if (hit.transform != null)
            return true;
        return false;
    }
}