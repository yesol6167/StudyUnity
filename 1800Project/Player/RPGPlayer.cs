using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RPGPlayer : BattleSystem, IObject
{
    public PlayerUI myUI;
    public LayerMask enemyMask = default;
    public LayerMask pickMask = default;
    public LayerMask HerbMask = default;
    public enum STATE
    {
        Create, Play, Gather, Death
    }

    public STATE myState = STATE.Create;

    void ChangeState(STATE st)
    {
        if (myState == st) return;
        myState = st;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Play:
                break;
            case STATE.Gather:
                break;
            case STATE.Death:
                StopAllCoroutines();
                Player_Anim.SetTrigger("Dead");
                foreach (IBattle ib in myAttackers)
                {
                    ib.IsDead(transform);
                }
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Play:
                if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
                //���콺 �����Ͱ� ui���� ��ġ���� �ʰ� ������
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    //���̾��ũ�� �ش��ϴ� ������Ʈ�� ���õǾ����� Ȯ���Ѵ�.
                    if (Physics.Raycast(ray, out hit, 1000.0f, enemyMask) && Monster.Inst.MonState != Monster.STATE.Dead)
                    {
                        myTarget = hit.transform;
                        AttackTarget(myTarget);
                    }

                    else if (Physics.Raycast(ray, out hit, 1000.0f, pickMask))
                    {
                        MoveToPosition(hit.point);
                    }

                }
                if (Input.GetKeyDown(KeyCode.E)) //E��ư�� ������ ä��.��� �������� �Ѿ�� ���� ����.
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 10.0f))
                    {
                        if (hit.collider.CompareTag("Door"))
                        {
                            hit.collider.transform.GetComponent<Door>().OpenCheck = !hit.collider.transform.GetComponent<Door>().OpenCheck;
                            Debug.Log("��");
                        }
                    }
                }
                break;

            case STATE.Gather:  
                if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 8.0f, HerbMask)) //��� Ŭ��������
                    {                     
                        Player_Anim.SetTrigger("Gather");

                        if(Player_Anim.GetBool("IsGathering")) //ä���ϰ� �ִ� ���¶�� 
                        {
                            Player_Anim.ResetTrigger("Gather"); //�ٽ� ��긦 Ŭ���ؼ� Gather Trigger�� �ߵ����� ���ϵ��� ����
                        }

                        if (!Player_Anim.GetBool("IsGathering")) //ä���ϰ� �ִ� ���°� �ƴ϶�� �̵� �����ϰ� ����
                        {
                            DropObjects(hit.transform.position);
                            MoveToPosition(hit.point, null, true);
                        }
                    }

                    else if (Physics.Raycast(ray, out hit, 100.0f, pickMask))
                    {
                        if(!Player_Anim.GetBool("IsGathering"))
                        {
                            MoveToPosition(hit.point, null, true);
                        }      
                    }
                }
                break;
            case STATE.Death:
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myStat.changeHP = (float h) => myUI.P_HPBar.value = h;
        ChangeState(STATE.Play);
    }

    // Update is called once per frame
    void Update()
    {
        myUI.HP_Text.text = myStat.curHP.ToString() + "/" + myStat.MaxHP.ToString();
        StateProcess();
    }
    public override void OnDamage(float damage)
    {
        myStat.curHP -= damage;
        if (Mathf.Approximately(myStat.curHP, 0.0f))
        {
            ChangeState(STATE.Death);
        }
        else
        {
            Player_Anim.SetTrigger("Damage");
        }
    }
    public override bool IsLive()
    {
        return !Mathf.Approximately(myStat.curHP, 0.0f);
    }
    public override void IsDead(Transform tr)
    {
        if (tr == myTarget)
        {
            StopAllCoroutines();
        }
    }
    public virtual void AddMoney(int money)
    {
        myStat.Money += money;
        myUI.Money_Text.text = money.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("HerbZone"))
        {
            ChangeState(STATE.Gather);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("HerbZone"))
        {
            ChangeState(STATE.Play);
        }
    }
    public virtual void DropObjects(Vector3 pos)
    {
        GameObject BloodyPrefabs = Instantiate(ObjectManager.inst.coinPrefab) as GameObject;
        BloodyPrefabs.transform.parent = ObjectManager.inst.transform;
        BloodyPrefabs.transform.position = new Vector3(pos.x + 0.5f, BloodyPrefabs.transform.position.y, pos.z);
    }
}

