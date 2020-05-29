using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target;       //카메라가 따라갈 대상
    public float moveSpeed;         //카메라가 얼마나 빠른 속도로 대상을 쫓을 건지
    private Vector3 targetPosition;    //대상의 현재 위치 값

    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    //박스 컬라이더 영역의 최소, 최대 xyz 값을 지님

    private float halfWidth;
    private float halfHeight;

    //카메라의 반너비, 반높이 값을 지닐 변수

    private Camera theCamera;

    //카메라의 반높이를 구할 속성을 이용하기 위한 변수

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        //매 프레임마다 이뤄지기 때문에 업데이트에 작성
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, this.transform.position.y, this.transform.position.z);
            //this = 카메라랑 대상이 겹치면 안보이기 때문에, 보이도록 약간 뒤에 설정

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);    //1초에 movespeed만큼 이동.

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
