using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestInfo : MonoBehaviour
{
   public Button myButton;

   public static QuestInfo Inst = null;

   public string ButtonName;
    private void Awake()
    {
        Inst = this;
    }

    //Ŭ���� ����Ʈ�� ���� ��������
    public void OpenQuestInfo()
    {
        ButtonName = EventSystem.current.currentSelectedGameObject.name; //��� Ŭ���� ��ư�� �̸�
        
        for (int i=0; i<QuestManager.Instance._quests.Length; ++i)
        {
            if (myButton.GetComponent<TMP_Text>().text == QuestManager.Instance._quests[i]._questName)
            {
                QuestManager.Instance.QuestInfoText.text = QuestManager.Instance._quests[i]._questInfo;
                QuestManager.Instance._quests[i]._questCheck[i] = true;
            }
           
           //Ŭ���� ��ư�� �̸��� �ٸ� ��ư�� �̸��� ���� ���� ��� 
           if(ButtonName != QuestManager.Instance._questPrefabs[i].GetComponentInChildren<TMPro.TMP_Text>().name) 
            {
                QuestManager.Instance._quests[i]._questCheck[i] = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}
