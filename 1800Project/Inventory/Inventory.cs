using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    //�Լ��� ����ó�� Ȱ���ϱ� ���� delegate ���
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
        SlotCnt = 4; //ó�� ���� ������ 4��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
