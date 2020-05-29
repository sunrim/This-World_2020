using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    static public MovingObject instance;

    public string currentMapName;       //transferMap 스크립트에 있는 transferMap 변수의 값 저장

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;     //통과 불가능한 것을 설정해주는 것

    public float speed;
    
    private Vector3 vector;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;

    private Animator animator;

    void Start() {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }


            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;       //방해물이 없으면 Null 반환, 방해물에 부딪히면 방해물 반환

            Vector2 start = transform.position;      //캐릭터의 현재 위치 값
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);        //캐릭터가 이동하고자 하는 위치 값

            boxCollider.enabled = false;        //캐릭터가 가지고 있는 고유의 박스 컬라이더를 잠깐 끄는 기능
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;         //꺼놨던 박스 컬라이더를 다시 켜는 기능

            if (hit.transform != null)
                break;

            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }

                if (applyRunFlag)
                {
                    currentWalkCount++;
                }

                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }

            currentWalkCount = 0;

        }
        animator.SetBool("Walking", false);
        canMove = true;
    }
       
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
        
    }
}