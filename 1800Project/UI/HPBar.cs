using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Transform myHP;
    public Slider M_HPBar;
    // Start is called before the first frame update
    void Start()
    {
        if (myHP == null) //monster 상태가 dead인 경우
        {
            Destroy(gameObject);    
        }
    }
    // Update is called once per frame
    void Update()
    {  
        if(myHP != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(myHP.position);
            transform.position = pos;
        }
        
        else
        {
            Destroy(gameObject);
        } 
    }
}
