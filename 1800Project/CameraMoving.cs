using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public static CameraMoving Inst = null;
    
    public Transform CameraTarget; //캐릭터
    public Transform CameraHead; 
    float dist = 0.0f;
    public float ZoomSpeed = 2.0f;
    public Vector2 ZoomRange = new Vector2(-180.0f, 30.0f);
    
    Vector3 curRot = Vector3.zero;
    Vector3 CamDir = Vector3.zero;

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CamDir = transform.position - CameraTarget.position;
        dist = CamDir.magnitude; //magnitude = Vector의 길이를 반환하며, 읽기 전용이다(Vector의 길이 = 거리)
        CamDir.Normalize();//벡터의 정규화

    }

    // Update is called once per frame
    void Update()
    {
        dist += Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed; //마우스 휠로 줌 인아웃 조절
        dist = Mathf.Clamp(dist, 1.0f, 10.0f);
        transform.position = CameraTarget.position + CamDir * dist;

        CameraRotation();
    }

    //카메라 회전
    void CameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            //마우스가 이전에 비해 얼마나 이동했는지 구한다.
            //Mouse Y는 마우스의 상,하 움직임에 대한 정보를 가진다. X축으로 회전하기 때문에 X에 Mouse Y를 축적시킨다.

            curRot.x += Input.GetAxis("Mouse Y") * -1 ;
            curRot.y += Input.GetAxis("Mouse X") ;

            curRot.x = Mathf.Clamp(curRot.x, 6.0f, 90.0f);
            curRot.y = Mathf.Clamp(curRot.y, ZoomRange.x, ZoomRange.y);

            Quaternion quat = Quaternion.Euler(new Vector3(curRot.x, curRot.y, 0.0f));
            CameraHead.transform.rotation = Quaternion.Slerp(CameraHead.transform.rotation, quat, Time.deltaTime * 20.0f);
        }

    }
}
