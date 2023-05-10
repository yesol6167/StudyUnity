using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "ScriptableObjects/NpcData", order = 0)]
public class NpcData : ScriptableObject
{
    internal static object NpcSTATE;
    [SerializeField] Sprite _npcPotrait;
    [SerializeField] string _name;
    [SerializeField] int _StarveLevel;
    [SerializeField] int _Strength;
    public Sprite NpcPotrait
    {
        get => _npcPotrait;  
    }
    public string Name
    {
        get => _name;
    }
    public int StarveLevel
    {
        get => _StarveLevel;
        set => _StarveLevel = value;
    }

    public int Strength
    {
        get => _Strength;
        set => _Strength = value;
    }
}
