using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_01 : MonoBehaviour
{
    Animator anim;

    float h;
    float v;

    void Start()
    {
        anim = GetComponent<Animator>();
       
      
    }

   
    void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            //anim.SetTrigger("walk");
            anim.SetBool("walking",true);
        } 
        else 
        {
            //anim.SetTrigger("stop");
            anim.SetBool("walking",false);
        }
                  
    }
}
