using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerception : MonoBehaviour
{
    public UnityEvent<Transform> FindTarget = default;
    public UnityEvent LostTarget = default;
    public Transform myTarget = null;
    public LayerMask enemyMask = default;

    private void OnTriggerEnter(Collider other) 
    {
        if (myTarget != null) return;
        if((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            //타겟을 처음 발견했을때
            myTarget = other.transform;
            FindTarget?.Invoke(myTarget);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(myTarget == other.transform)
        {
            //타겟이 범위 밖일때
            myTarget = null;
            LostTarget?.Invoke();
        }
    }
}
