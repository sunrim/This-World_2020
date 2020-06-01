using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string characterName;

    public float speed;
    public int walkCount;
    protected int currentWalkCount;

    private bool notCoroutine = false;
    protected Vector3 vector;

    public Queue<string> queue;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;     //통과 불가능한 것을 설정해주는 것
    public Animator animator;

    public void Move(string _dir, int _frequency = 5)
    {
        queue.Enqueue(_dir);
        if (!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _frequency)); 
        }
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        while(queue.Count != 0)
        {
            string direction = queue.Dequeue();
            vector.Set(0, 0, vector.z);

            switch (direction)
            {
                case "LEFT":
                    vector.x = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * speed, 0, 0);

                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }

            currentWalkCount = 0;
            if (_frequency != 5)
                animator.SetBool("Walking", false);
        }
        animator.SetBool("Walking", false);
        notCoroutine = false;
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