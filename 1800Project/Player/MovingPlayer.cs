using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : CharacterProperty
{

    public float MoveSpeed = 20.0f;
    Vector3 PlayerDir = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        PlayerDir.x = Input.GetAxis("Horizontal");
        PlayerDir.y = Input.GetAxis("Vertical");

        //Vector3 LookRight = new Vector3(CameraMoving.Inst.CameraHead.right.x, 0.0f, CameraMoving.Inst.CameraHead.right.z).normalized; //방향
        Vector3 LookForward = new Vector3(CameraMoving.Inst.CameraHead.forward.x, 0.0f, CameraMoving.Inst.CameraHead.forward.z).normalized;
        //Vector3 MoveDir = LookRight * PlayerDir.x + LookForward * PlayerDir.y; //카메라를 바라보는 방향으로 이동

        transform.forward = LookForward;
        //transform.position += MoveDir * Time.deltaTime ;


        //선형보간을 이용하여 캐릭터가 부드럽게 이동하게 함
        float x = Mathf.Lerp(Player_Anim.GetFloat("x"), PlayerDir.x, MoveSpeed * Time.deltaTime);
        float y = Mathf.Lerp(Player_Anim.GetFloat("z"), PlayerDir.y, MoveSpeed * Time.deltaTime);

        Player_Anim.SetFloat("x", x);
        Player_Anim.SetFloat("z", y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player_Anim.SetTrigger("IsJumping");
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Player_Anim.SetTrigger("IsAttack");
        }
    }
    public void OnDamage(float damage)
    {

    }
}
