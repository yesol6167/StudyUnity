using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public static CameraMoving Inst = null;
    
    public Transform CameraTarget; //ĳ����
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
        dist = CamDir.magnitude; //magnitude = Vector�� ���̸� ��ȯ�ϸ�, �б� �����̴�(Vector�� ���� = �Ÿ�)
        CamDir.Normalize();//������ ����ȭ

    }

    // Update is called once per frame
    void Update()
    {
        dist += Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed; //���콺 �ٷ� �� �ξƿ� ����
        dist = Mathf.Clamp(dist, 1.0f, 10.0f);
        transform.position = CameraTarget.position + CamDir * dist;

        CameraRotation();
    }

    //ī�޶� ȸ��
    void CameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            //���콺�� ������ ���� �󸶳� �̵��ߴ��� ���Ѵ�.
            //Mouse Y�� ���콺�� ��,�� �����ӿ� ���� ������ ������. X������ ȸ���ϱ� ������ X�� Mouse Y�� ������Ų��.

            curRot.x += Input.GetAxis("Mouse Y") * -1 ;
            curRot.y += Input.GetAxis("Mouse X") ;

            curRot.x = Mathf.Clamp(curRot.x, 6.0f, 90.0f);
            curRot.y = Mathf.Clamp(curRot.y, ZoomRange.x, ZoomRange.y);

            Quaternion quat = Quaternion.Euler(new Vector3(curRot.x, curRot.y, 0.0f));
            CameraHead.transform.rotation = Quaternion.Slerp(CameraHead.transform.rotation, quat, Time.deltaTime * 20.0f);
        }

    }
}
