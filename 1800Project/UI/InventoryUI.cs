using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    public GameObject InventoryWindow;
    bool IsOpen = false;

    public Slot[] slots;
    public Transform slotsContent;

    // Start is called before the first frame update
    void Start()
    {
        slots = slotsContent.GetComponentsInChildren<Slot>();
        InventoryWindow.SetActive(false);
        //inven.slotCountChange += SlotChange;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            IsOpen = !IsOpen;
            if(IsOpen) { InventoryWindow.SetActive(true); }
            else InventoryWindow.SetActive(false);
        }
    }
    private void SlotChange(int val)
    {
        for(int i=0; i < slots.Length; ++i)
        {
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }
}
