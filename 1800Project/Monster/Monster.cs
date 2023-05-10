using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Monster : BattleSystem, IObject
{
    public static Monster Inst = null;
    public GameObject HPBarObject;
    public GameObject HPObj;
    public Transform HPBarPos;
    HPBar myHPBar = null;

    Vector3 MonPos = Vector3.zero;

    public enum STATE
    {
        Create, Idle, Roaming, Battle, Dead
    }

    public STATE MonState = STATE.Create;
    private void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateHPBar();
        MonPos = transform.position;
        ChangeState(STATE.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    void ChangeState(STATE ms)
    {
        if (MonState == ms) return;
        MonState = ms;
        switch (MonState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                StartCoroutine(DelayRoaming(2.0f));
                HPObj.SetActive(false);
                break;
            case STATE.Roaming:
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(-4.0f, 4.0f);
                pos.z = Random.Range(-4.0f, 4.0f);
                pos = MonPos + pos;
                MoveToPosition(pos, () => ChangeState(STATE.Idle));
                HPObj.SetActive(false);
                break;
            case STATE.Battle:
                AttackTarget(myTarget);
                HPObj.SetActive(true);
                break;
            case STATE.Dead:
                StopAllCoroutines();
                HPObj.SetActive(true);
                Player_Anim.SetBool("Death", true);
                foreach(IBattle ib in myAttackers)
                {
                    ib.IsDead(transform);
                }
                StartCoroutine(Dying(3.0f));
                break;
        }
    }

    void StateProcess()
    {
        switch (MonState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.Roaming:
                break;
            case STATE.Battle:
                break;
            case STATE.Dead:
                break;
        }
    }
    IEnumerator DelayRoaming(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(STATE.Roaming);
    }

    public void FindTarget(Transform Target)
    {
        if (MonState == STATE.Dead) return;
        myTarget = Target;
        StopAllCoroutines(); //타겟을 발견해서 battle 상태가 된다면 모든 코루틴 종료
        ChangeState(STATE.Battle);
    }

    public void LostTarget()
    {
        if (MonState == STATE.Dead) return;
        myTarget = null;
        StopAllCoroutines();
        Player_Anim.SetBool("WalkForward", false);
        Player_Anim.SetBool("IsAttacking", false);
        ChangeState(STATE.Idle);
    }

    public override void OnDamage(float damage) //인터페이스는 다중상속 가능
    {
        myStat.curHP -= damage;
        if (Mathf.Approximately(myStat.curHP, 0.0f))
        {
            ChangeState(STATE.Dead);
        }
        else
        {
            Player_Anim.SetTrigger("Damage");
        }
    }
    public override bool IsLive()
    {
        return MonState != STATE.Dead;
    }
    public override void IsDead(Transform tr)
    {
        if(tr == myTarget)
        {
            LostTarget();
        }
    }
    IEnumerator Dying(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
        ObjectManager.inst.DropCoin(transform.position, 200);
        DropObjects(transform.position);
    }
    public void CreateHPBar()
    {
        HPObj = Instantiate(Resources.Load("Prefabs/HPBar"), HPBarObject.transform) as GameObject;
        myHPBar = HPObj.GetComponent<HPBar>();
        myHPBar.myHP = HPBarPos;

        myStat.changeHP = (float h) => myHPBar.M_HPBar.value = h;
    }

    public virtual void DropObjects(Vector3 pos)
    {
       GameObject MeatPrefabs = Instantiate(ObjectManager.inst.meatPrefab) as GameObject;
       MeatPrefabs.transform.parent = ObjectManager.inst.transform;
       MeatPrefabs.transform.position = new Vector3(pos.x+0.5f, MeatPrefabs.transform.position.y, pos.z);
    }
}
