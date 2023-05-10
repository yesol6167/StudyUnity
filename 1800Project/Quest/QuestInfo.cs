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

    //클릭한 퀘스트의 정보 가져오기
    public void OpenQuestInfo()
    {
        ButtonName = EventSystem.current.currentSelectedGameObject.name; //방금 클릭한 버튼의 이름
        
        for (int i=0; i<QuestManager.Instance._quests.Length; ++i)
        {
            if (myButton.GetComponent<TMP_Text>().text == QuestManager.Instance._quests[i]._questName)
            {
                QuestManager.Instance.QuestInfoText.text = QuestManager.Instance._quests[i]._questInfo;
                QuestManager.Instance._quests[i]._questCheck[i] = true;
            }
           
           //클릭한 버튼의 이름과 다른 버튼의 이름이 같지 않은 경우 
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
