using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_01 : MonoBehaviour
{
    Animator anim;
    FixedJoystick joystick;

    float h;
    float v;

    float hmobile;
    float vmobile;

    void Start()
    {
        anim = GetComponent<Animator>();
         if(Application.platform== RuntimePlatform.Android /*|| Application.platform == RuntimePlatform.WindowsEditor*/)
        {
            joystick = FindObjectOfType<FixedJoystick>();
        }
      
    }

    void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0 )
        {
            //anim.SetTrigger("walk");
            anim.SetBool("walking",true);
        } 
        else 
        {
            //anim.SetTrigger("stop");
            anim.SetBool("walking",false);
        }

        if(Application.platform== RuntimePlatform.Android)
        {
        hmobile=joystick.Horizontal;
        vmobile=joystick.Vertical;
            if( hmobile!=0||vmobile!=0)
            {
                anim.SetBool("walking",true);
            }
            else
            {
                anim.SetBool("walking",false);
            }
        }         
    }
}
