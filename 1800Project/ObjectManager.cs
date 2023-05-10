using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObject
{
    void DropObjects(Vector3 pos);
}
public class ObjectManager : MonoBehaviour, IObject
{
    public static ObjectManager inst = null;
    public GameObject coinPrefab;
    public GameObject meatPrefab;
    public GameObject bloodyHerbPrefab;

    public int maxCoins = 20;

    List<GameObject> coins = new List<GameObject>();

    private void Awake()
    {
        inst = this;
        MakeCoins();
    }
    public virtual void DropObjects(Vector3 pos)
    {
      
    }

    void MakeCoins()
    {
        for(int i=0; i < maxCoins; ++i )
        {
            GameObject tempCoin = Instantiate(coinPrefab) as GameObject; //동전 생성 프리팹으로 해줌
            tempCoin.transform.parent = transform; //새로 생성된 동전들이 오브젝트 매니저의 자식으로 들어감

            tempCoin.SetActive(false);
            coins.Add(tempCoin);
        }
    }
    public void DropCoin(Vector3 pos, int coinValue)
    {
        GameObject reusedCoin = null;
        for (int i = 0; i < coins.Count; ++i)
        {
            if (coins[i].activeSelf == false) //생성된 동전들의 bool값이 계속 false인 상태이면 재사용 가능한 상태
            {
                reusedCoin = coins[i];
                break;
            }
        }
        if(reusedCoin == null) //동전이 재사용되지 않아서 reusedCoin이 null인 상태라면
        {
            GameObject newCoin = Instantiate(coinPrefab) as GameObject; //새로 coinPrefab을 생성해줌
            coins.Add(newCoin);
            reusedCoin = newCoin;
        }
        reusedCoin.SetActive(true);
        reusedCoin.GetComponent<Coin>().CoinValue(coinValue);
        reusedCoin.transform.position = new Vector3(pos.x, reusedCoin.transform.position.y, pos.z);
    }
}
