using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LikeAbilityWindow : MonoBehaviour,IBeginDragHandler, IDragHandler
{
    public TMP_Text[] myText;
    public NpcData[] myData;

    public GameObject LikeWindow;
    public Button ExitBtn;
    Vector2 dragOffset = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        myText[0].text = myData[0].Name;
        myText[1].text = myData[1].Name;
    }
    public void OpenLikeWin()
    {
        LikeWindow.SetActive(true);
    }
    public void ClickExit()
    {
        LikeWindow.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)LikeWindow.transform.localPosition - eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = dragOffset + eventData.position;
  
        LikeWindow.transform.localPosition = pos;
    }
}
