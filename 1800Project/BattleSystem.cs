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
            if(_target != null) //Ÿ���� ������ AddAttacker�� �߰�
            {
                _target.GetComponent<IBattle>()?.AddAttacker(this);
            }
        }
    }
    public void AttackTarget()
    {
        //Ÿ�ٰ� �� ��ġ�� �Ÿ��� AttackRange �ȿ� �־�� �������� ���� ����
       if(Vector3.Distance(myTarget.position, transform.position) <= myStat.AttackRange + 0.5f)
        {
            myTarget.GetComponent<IBattle>()?.OnDamage(myStat.AD);
        }
    }
    public virtual void OnDamage(float damage) //�ڽ��� �������ؼ� ���� ���� Virtual ���
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
        for(int i = 0; i < myAttackers.Count;) //������ƾ�� ������ �ڵ�����X
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
