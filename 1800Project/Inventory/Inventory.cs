using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    //함수를 변수처럼 활용하기 위해 delegate 사용
    public delegate void SlotCountChange(int slotCount);
    public SlotCountChange slotCountChange;

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            slotCountChange.Invoke(slotCnt);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SlotCnt = 4; //처음 슬롯 개수는 4개
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
