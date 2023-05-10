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
    public List<Quest> AllQuestList = new List<Quest>(); //��� ����Ʈ
    public List<Quest> QuestingList = new List<Quest>();  //�������� ����Ʈ
    List<Quest> temp = new List<Quest>(); //�������� ����Ʈ �ߺ� üũ�� ����Ʈ
    public List<Quest> EndQuestList = new List<Quest>(); //�Ϸ�� ����Ʈ

    //��� ����Ʈ ������ ����
    [SerializeField]
    public Quest[] _quests;
    public GameObject[] _questPrefabs;

    public GameObject AllQuestObj;
    public GameObject QuestingObj;
   
    public GameObject QuestInfoWin;
    public TMP_Text QuestInfoText;

    //QuestSort�� Hungry�� ��� ����Ʈ ��Ͽ� Hungry ����Ʈ �߰�
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

    //����Ʈ �³��ϸ� �������� ����Ʈ�� �߰�
    public void ProgressQuest(Quest pquests)
    {
        for (int i = 0; i < AllQuestList.Count; ++i)
        {
            pquests = AllQuestList[i];
            if (pquests._questCheck[i] == true ) 
            {
                temp.Add(pquests); //�ߺ�üũ�� ����Ʈ�� �߰�
                for (int j = 0; j < temp.Count; ++j)
                {
                    if (!QuestingList.Contains(temp[j])) //�������� ����Ʈ�� �ߺ��� ����Ʈ�� ���ٸ�
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
