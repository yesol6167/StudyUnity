using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct CharacterStat
{
    [SerializeField] float hp;
    [SerializeField] float maxHp;
    [SerializeField] float mp;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotSpeed;
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;
    [SerializeField] float ad; //공격력
    [SerializeField] int money;

    public UnityAction<float> changeHP;
    public float curHP
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0.0f, maxHp);
            changeHP?.Invoke(hp / maxHp); //(현재 피/ 최대 피) 비율을 slider value에 전달
        }
    }
    public float curMP
    {
        get => mp; 
    }

    public float MaxHP
    {
        get => maxHp;
    }
    public float MoveSpeed
    {
        get => moveSpeed;
    }
    public float RotSpeed
    {
        get => rotSpeed;
    }
    public float AttackRange
    {
        get => attackRange;
    }
    public float AttackDelay
    {
        get => attackDelay;
    }
    public float AD
    {
        get => ad;
    }
    public int Money
    {
        get => money;
        set => money = value;
    }
}