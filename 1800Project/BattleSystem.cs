using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    void OnDamage(float damage);
    bool IsLive();
    void AddAttacker(IBattle ib);
    void RemoveAttacker(IBattle ib);
    void IsDead(Transform tr);
}

public class BattleSystem : CharacterMoveMent, IBattle
{
    protected List<IBattle> myAttackers = new List<IBattle>();
    Transform _target = null;

    protected Transform myTarget
    {
        get => _target;
        set
        {
            _target = value;
            if(_target != null) //타겟이 있으면 AddAttacker에 추가
            {
                _target.GetComponent<IBattle>()?.AddAttacker(this);
            }
        }
    }
    public void AttackTarget()
    {
        //타겟과 내 위치의 거리가 AttackRange 안에 있어야 데미지가 들어가게 설정
       if(Vector3.Distance(myTarget.position, transform.position) <= myStat.AttackRange + 0.5f)
        {
            myTarget.GetComponent<IBattle>()?.OnDamage(myStat.AD);
        }
    }
    public virtual void OnDamage(float damage) //자식이 재정의해서 쓰기 위해 Virtual 사용
    {

    }

    public virtual bool IsLive()
    {
        return true;
    }
    public void AddAttacker(IBattle ib)
    {
        myAttackers.Add(ib);
    }
    public void RemoveAttacker(IBattle ib)
    {
        for(int i = 0; i < myAttackers.Count;) //삭제루틴이 있을때 자동증가X
        {
            if(ib == myAttackers[i])
            {
                myAttackers.RemoveAt(i);
                break;
            }
            ++i;
        }
    }
    public virtual void IsDead(Transform tr)
    {

    }
}
