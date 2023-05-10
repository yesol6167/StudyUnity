using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

public class QuestManager : Singleton<QuestManager>
{
    public List<Quest> AllQuestList = new List<Quest>(); //모든 퀘스트
    public List<Quest> QuestingList = new List<Quest>();  //진행중인 퀘스트
    List<Quest> temp = new List<Quest>(); //진행중인 퀘스트 중복 체크용 리스트
    public List<Quest> EndQuestList = new List<Quest>(); //완료된 퀘스트

    //모든 퀘스트 프리팹 저장
    [SerializeField]
    public Quest[] _quests;
    public GameObject[] _questPrefabs;

    public GameObject AllQuestObj;
    public GameObject QuestingObj;
   
    public GameObject QuestInfoWin;
    public TMP_Text QuestInfoText;

    //QuestSort가 Hungry일 경우 퀘스트 목록에 Hungry 퀘스트 추가
    public void AddHungry(Quest quests)
    {
        for (int i = 0; i < _quests.Length; ++i)
        {
            quests = _quests[i];
            if (quests._questSort == Quest.QuestSort.Hungry)
            {
                AllQuestList.Add(quests);
                
                GameObject ALLQ = Instantiate(Resources.Load("Prefabs/PostQuest"), AllQuestObj.transform) as GameObject;
                ALLQ.GetComponentInChildren<TMPro.TMP_Text>().text = quests._questName;
                ALLQ.GetComponentInChildren<TMPro.TMP_Text>().name = $"PostQuest_{i}";
                _questPrefabs[i] = ALLQ;
            }
        }
    }

    //퀘스트 승낙하면 진행중인 퀘스트에 추가
    public void ProgressQuest(Quest pquests)
    {
        for (int i = 0; i < AllQuestList.Count; ++i)
        {
            pquests = AllQuestList[i];
            if (pquests._questCheck[i] == true ) 
            {
                temp.Add(pquests); //중복체크용 리스트에 추가
                for (int j = 0; j < temp.Count; ++j)
                {
                    if (!QuestingList.Contains(temp[j])) //진행중인 퀘스트에 중복된 퀘스트가 없다면
                    {
                        QuestingList.Add(temp[j]);
                        GameObject INGQ = Instantiate(Resources.Load("Prefabs/ProgressQuest"), QuestingObj.transform) as GameObject;
                        INGQ.GetComponentInChildren<TMPro.TMP_Text>().text = pquests._questName;
                       
                    }
                }
                //pquests._questCheck[i] = false;
            }
           
        }

     
    }


}
