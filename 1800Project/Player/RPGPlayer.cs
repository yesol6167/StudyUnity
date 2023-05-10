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
                //마우스 포인터가 ui위에 위치하지 않고 있으며
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    //레이어마스크에 해당하는 오브젝트가 선택되었는지 확인한다.
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
                if (Input.GetKeyDown(KeyCode.E)) //E버튼을 누르면 채집.사냥 공간으로 넘어가는 문을 연다.
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 10.0f))
                    {
                        if (hit.collider.CompareTag("Door"))
                        {
                            hit.collider.transform.GetComponent<Door>().OpenCheck = !hit.collider.transform.GetComponent<Door>().OpenCheck;
                            Debug.Log("문");
                        }
                    }
                }
                break;

            case STATE.Gather:  
                if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 8.0f, HerbMask)) //허브 클릭했을때
                    {                     
                        Player_Anim.SetTrigger("Gather");

                        if(Player_Anim.GetBool("IsGathering")) //채집하고 있는 상태라면 
                        {
                            Player_Anim.ResetTrigger("Gather"); //다시 허브를 클릭해서 Gather Trigger가 발동되지 못하도록 설정
                        }

                        if (!Player_Anim.GetBool("IsGathering")) //채집하고 있는 상태가 아니라면 이동 가능하게 설정
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

