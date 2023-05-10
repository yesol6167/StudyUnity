using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMoveMent : CharacterProperty
{ 
    //캐릭터 이동과 공격

    Coroutine MoveCo = null;
    Coroutine RotCo = null;
    Coroutine AttackCo = null;

    protected void AttackTarget(Transform Target)
    {
        StopAllCoroutines();
        AttackCo = StartCoroutine(AttackingTarget(Target, myStat.AttackRange, myStat.AttackDelay));
    }

    //UnityAction은 Unity에서 제공해주는 delegate
    protected void MoveToPosition(Vector3 pos, UnityAction done = null, bool Rot = true)
    {
        if(AttackCo != null)
        {
            StopCoroutine(AttackCo);
            AttackCo = null;
        }


        if(MoveCo != null) //움직이고 있는 상태이면
        {
            StopCoroutine(MoveCo);
            MoveCo = null;
        }
        
        MoveCo = StartCoroutine(MovingToPosition(pos,done));
        
        if(Rot)
        {
            if (RotCo != null)
            {
                StopCoroutine(RotCo);
                RotCo = null;
            }

            RotCo = StartCoroutine(RotatingToPosition(pos));
        }
        
    }
    IEnumerator MovingToPosition(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        //걷기 시작
        Player_Anim.SetBool("WalkForward", true);

        while (dist > 0.0f)
        {
            if(Player_Anim.GetBool("IsAttacking")) //공격중인 상태에서는 이동하지 않게
            {
                Player_Anim.SetBool("WalkForward", false);
                yield break;
            }

            if(!Player_Anim.GetBool("IsAttacking"))
            {
                float delta = myStat.MoveSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            }

           if (Player_Anim.GetBool("IsGathering")) //채집중인 상태에서는 이동하지 않게
            {
                Player_Anim.SetBool("WalkForward", false);
                yield break;
            }
            yield return null;
        }

        //걷기 끝
        Player_Anim.SetBool("WalkForward", false);
        done?.Invoke(); //done 함수가 null이 아니면 함수 실행
    }
  
    IEnumerator RotatingToPosition(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        float Angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f) //두 벡터의 내적
        {
            rotDir = -rotDir;
        }

        while (Angle > 0.0f)
        {
            if(!Player_Anim.GetBool("IsAttacking"))
            {
                float delta = myStat.RotSpeed * Time.deltaTime;
                if (delta > Angle)
                {
                    delta = Angle;
                }
                Angle -= delta;
                transform.Rotate(Vector3.up * rotDir * delta, Space.World);
            }
            yield return null;
        }
    }

    IEnumerator AttackingTarget(Transform Target, float AttackRange, float AttackDelay)
    {
        float playTime = 0.0f;
        float delta = 0.0f;
       
        while(Target != null)
        {
            if(!Player_Anim.GetBool("IsAttacking"))
            {
                playTime += Time.deltaTime;
            }

            //이동
            Vector3 dir = Target.position - transform.position;
            float dist = dir.magnitude;
            dir.Normalize();
           
            if(dist > AttackRange)
            {
                Player_Anim.SetBool("WalkForward", true);
                delta = myStat.MoveSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                transform.Translate(dir * delta, Space.World);
            }

            else
            {
                Player_Anim.SetBool("WalkForward", false);
                if(playTime >= AttackDelay)
                {
                    //공격
                    playTime = 0.0f;
                    Player_Anim.SetTrigger("Attack");
                }
            }
            //회전
            delta = myStat.RotSpeed * Time.deltaTime;
            float Angle = Vector3.Angle(dir, transform.forward);
            float RotDir = 1.0f;
            if(Vector3.Dot(transform.right, dir) < 0.0f)
            {
                RotDir = -RotDir;
            }
            if(delta > Angle)
            {
                delta = Angle;
            }
            transform.Rotate(Vector3.up * delta * RotDir, Space.World);

            yield return null;
        }
        Player_Anim.SetBool("WalkForward", false);
    }

    private void Start()
    {
        if (Player_Anim.GetBool("IsGathering")) StopAllCoroutines();
    }

}
