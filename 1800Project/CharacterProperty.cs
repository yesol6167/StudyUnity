using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty : MonoBehaviour
{

    //Ä³¸¯ÅÍ ½ºÅÝ
    public CharacterStat myStat;

    Animator p_anim = null;
 
    protected Animator Player_Anim
    {
        get
        {
            if(p_anim == null)
            {
                p_anim = GetComponent<Animator>();
            }
            return p_anim;
        }
       
    }
}
