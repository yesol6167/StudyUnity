using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/QuestData", order = -1)]
public class Quest : ScriptableObject
{
    public enum QuestSort
    {
        Hungry, Sick
    }
    public enum QuestItem
    {
        Meat, Herb
    }

    [SerializeField] public string _questName;
    [SerializeField] public QuestSort _questSort;
    [SerializeField] public QuestItem[] _items;
    [SerializeField] public string _questInfo;
    [SerializeField] public bool[] _questCheck;

    public string questName
    {
        get => _questName;
        set => _questName = value;
    }

    public QuestSort questSort
    {
        get => _questSort;
        set => _questSort = value;
    }
    public QuestItem[] items
    {
        get => _items;
        set => _items = value;
    }

    public string info
    {
        get => _questInfo;
        set => _questInfo = value;
    }

    public bool[] questcheck
    {
        get => _questCheck;
        set => _questCheck = value;
    }

}