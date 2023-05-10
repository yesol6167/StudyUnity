using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestWindow : MonoBehaviour
{
    public GameObject QuestListWindow;
    public GameObject QuestListMenu;

    // Start is called before the first frame update
    void Start()
    {
        QuestListWindow.SetActive(false);
        QuestListMenu.SetActive(false);
      
    }

    
    public void OpenQuestMenu()
    {
        QuestListMenu.SetActive(true);

    }
    public void OpenQuestWin()
    {
        QuestListWindow.SetActive(true);
    }


}
