using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMoveMent : CharacterProperty
{ 
    //ĳ���� �̵��� ����

    Coroutine MoveCo = null;
    Coroutine RotCo = null;
    Coroutine AttackCo = null;

    protected void AttackTarget(Transform Target)
    {
        StopAllCoroutines();
        AttackCo = StartCoroutine(AttackingTarget(Target, myStat.AttackRange, myStat.AttackDelay));
    }

    //UnityAction�� Unity���� �������ִ� delegate
    protected void MoveToPosition(Vector3 pos, UnityAction done = null, bool Rot = true)
    {
        if(AttackCo != null)
        {
            StopCoroutine(AttackCo);
            AttackCo = null;
        }


        if(MoveCo != null) //�����̰� �ִ� �����̸�
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

        //�ȱ� ����
        Player_Anim.SetBool("WalkForward", true);

        while (dist > 0.0f)
        {
            if(Player_Anim.GetBool("IsAttacking")) //�������� ���¿����� �̵����� �ʰ�
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

           if (Player_Anim.GetBool("IsGathering")) //ä������ ���¿����� �̵����� �ʰ�
            {
                Player_Anim.SetBool("WalkForward", false);
                yield break;
            }
            yield return null;
        }

        //�ȱ� ��
        Player_Anim.SetBool("WalkForward", false);
        done?.Invoke(); //done �Լ��� null�� �ƴϸ� �Լ� ����
    }
  
    IEnumerator RotatingToPosition(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        float Angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f) //�� ������ ����
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

            //�̵�
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
                    //����
                    playTime = 0.0f;
                    Player_Anim.SetTrigger("Attack");
                }
            }
            //ȸ��
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
