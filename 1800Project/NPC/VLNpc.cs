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
    public NpcData orgData; //스크럽터블 오브젝트에서 데이터 가져오기
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
        Debug.Log("이름 : " + myStat.Name);
        Debug.Log("배고픈 정도: " + myStat.StarveLevel);

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

    //배고픈 상태
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
        Debug.Log("배고픈 상태");
        yield return null;
    }

    public void PostedQuest() //퀘스트 승낙
    {
        QuestManager.Instance.ProgressQuest(myquest);        
    }

}
