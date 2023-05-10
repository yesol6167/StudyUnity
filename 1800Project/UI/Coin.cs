using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 180.0f;
    public LayerMask PlayerMask = default;

    [System.NonSerialized]
    public int money = 100;

    public void CoinValue(int money)
    {
        this.money = money;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((PlayerMask & 1 << other.gameObject.layer) != 0) //플레이어가 동전 콜라이더 안으로 들어왔을때
        {
            other.gameObject.GetComponent<RPGPlayer>().AddMoney(money);
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f);
    }
}
