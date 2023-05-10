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
            GameObject tempCoin = Instantiate(coinPrefab) as GameObject; //���� ���� ���������� ����
            tempCoin.transform.parent = transform; //���� ������ �������� ������Ʈ �Ŵ����� �ڽ����� ��

            tempCoin.SetActive(false);
            coins.Add(tempCoin);
        }
    }
    public void DropCoin(Vector3 pos, int coinValue)
    {
        GameObject reusedCoin = null;
        for (int i = 0; i < coins.Count; ++i)
        {
            if (coins[i].activeSelf == false) //������ �������� bool���� ��� false�� �����̸� ���� ������ ����
            {
                reusedCoin = coins[i];
                break;
            }
        }
        if(reusedCoin == null) //������ ������� �ʾƼ� reusedCoin�� null�� ���¶��
        {
            GameObject newCoin = Instantiate(coinPrefab) as GameObject; //���� coinPrefab�� ��������
            coins.Add(newCoin);
            reusedCoin = newCoin;
        }
        reusedCoin.SetActive(true);
        reusedCoin.GetComponent<Coin>().CoinValue(coinValue);
        reusedCoin.transform.position = new Vector3(pos.x, reusedCoin.transform.position.y, pos.z);
    }
}
