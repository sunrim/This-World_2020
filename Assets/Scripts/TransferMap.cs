using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;      //이동할 맵의 이름.

    public Transform target;
    public BoxCollider2D targetBound;

    [Tooltip("LEFT,RIGHT")]
    public string direction;        //캐릭터가 바라보고 있는 방향
    private Vector2 vector;         //getfloat("dirX")

    [Tooltip("계단이 있다 : true, 계단이 없다 : false")]
    public bool stair;      //계단이 있냐 없냐.

    private PlayerManager thePlayer;
    private CameraManager theCamera;
    private FadeManager theFade;
    private OrderManager theOrder;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!stair)
        {
            if(collision.gameObject.name == "Player")
            {
                StartCoroutine(TransferCoroutine());
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (stair)
        {
            if (collision.gameObject.name == "Player")
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));
                    switch (direction)
                    {
                        case "LEFT":
                            if(vector.x == -1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "RIGHT":
                            if (vector.x == 1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        default:
                            StartCoroutine(TransferCoroutine());
                            break;
                    }
                }
            }
        }
    }

    IEnumerator TransferCoroutine()
    {
        theOrder.NotMove();
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        thePlayer.currentMapName = transferMapName;
        theCamera.SetBound(targetBound);
        theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        thePlayer.transform.position = target.transform.position;
        yield return new WaitForSeconds(1f);
        theFade.FadeIn();
        yield return new WaitForSeconds(0.5f);
        theOrder.Move();
    }
}
