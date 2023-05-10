using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Serializable]
    public struct ItemStat
    {
        public ItemData orgData;

        public string Name
        {
            get => orgData.ItemName;
        }
        public string Info
        {
            get => orgData.ItemInfo;
        }
        public Sprite Image
        {
            get => orgData.ItemImage;
        }

        public ItemData Data
        {
            get => orgData;
            set => orgData = value; 
        }
    }

    [SerializeField] public ItemStat itemStat;
    bool isPickUp;

    public enum ObjectType
    {
        Coin, Meat, Herb
    }
    public string Name
    {
        get => itemStat.Name;
    }
    public string Info
    {
        get => itemStat.Info;
    }
    public Sprite Image
    {
        get => itemStat.Image;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickUp && Input.GetKeyDown(KeyCode.Z))
            PickUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag.Equals("Player")))
        {
            isPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if((other.gameObject.tag.Equals("Player")))
        {
            isPickUp=false;
        }
    }
    public void PickUp()
    {
        InvenData.instance.itemDB.Add(itemStat);
        //Destroy(gameObject);
    }
}

