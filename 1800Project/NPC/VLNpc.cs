using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public enum NpcType
{
    Npc1, Npc2
}
public enum NpcSTATE
{
    Create, Idle, Starving
}

[Serializable]
public struct NpcStat
{
    public NpcData orgData; //��ũ���ͺ� ������Ʈ���� ������ ��������
    public string Name
    {
        get => orgData.Name;
    }
    public int StarveLevel
    {
        get => orgData.StarveLevel;
    }
}
public class VLNpc : MonoBehaviour
{
    Quest myquest;
    [SerializeField] protected NpcStat myStat;
    public string Name
    {
        get => myStat.Name;
    }

    public int StarveLevel
    {
        get => myStat.StarveLevel;
    }


    public NpcSTATE myState = NpcSTATE.Create;

    public float TotalStarve = 100.0f;
    public GameObject QuestAlarm;

    public NpcSTATE npcState
    {
        get => myState;
        set => npcState = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("�̸� : " + myStat.Name);
        Debug.Log("����� ����: " + myStat.StarveLevel);

        QuestAlarm.SetActive(false);
        ChangeState(NpcSTATE.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
    void ChangeState(NpcSTATE ns)
    {
        if (myState == ns) return;
        myState = ns;

        switch (myState)
        {
            case NpcSTATE.Create:
                break;
            case NpcSTATE.Idle:
                break;
            case NpcSTATE.Starving:
                StartCoroutine(StartStarve());
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case NpcSTATE.Create:
                break;
            case NpcSTATE.Idle:
                StartIdle();
                break;
            case NpcSTATE.Starving:
                break;
        }
    }

    //����� ����
    public void StartIdle()
    {
        TotalStarve -= Time.deltaTime;

        if (TotalStarve <= myStat.StarveLevel)
        {
            ChangeState(NpcSTATE.Starving);
        }
    }

    IEnumerator StartStarve()
    {
        this.transform.GetComponent<Animator>().SetTrigger("IsStarving");
        QuestAlarm.SetActive(true);
        QuestManager.Instance.AddHungry(myquest);
        Debug.Log("����� ����");
        yield return null;
    }

    public void PostedQuest() //����Ʈ �³�
    {
        QuestManager.Instance.ProgressQuest(myquest);        
    }

}
