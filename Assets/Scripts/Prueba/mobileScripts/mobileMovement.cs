using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class mobileMovement : MonoBehaviour
{

    Rigidbody rbPlayer;
    [SerializeField]float speed = 10f;
    FixedJoystick joystick;
    [SerializeField] GameObject joyStickImage;
    
   


    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
       if(Application.platform== RuntimePlatform.Android /*|| Application.platform == RuntimePlatform.WindowsEditor*/)
        {
            joyStickImage.SetActive(true);
            joystick = FindObjectOfType<FixedJoystick>();
        }
        
    }
    void FixedUpdate()
    {
        if(Application.platform==RuntimePlatform.WebGLPlayer|| Application.platform == RuntimePlatform.WindowsEditor)
        {
            PlayerControl();
        }      
        
       
        if(Application.platform== RuntimePlatform.Android /*|| Application.platform == RuntimePlatform.WindowsEditor*/)        
        {
              JoyStickcontrol();
        }
                  

       

    }

    void PlayerControl()
    {
        joyStickImage.SetActive(false);
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal")*speed, rbPlayer.velocity.y, Input.GetAxisRaw("Vertical")*speed);
        rbPlayer.velocity = direction.normalized*10;

        if(direction!= Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 7.3f);
        }
        
    }

    void JoyStickcontrol() 
    {
        
        Vector3 directionMObile = new Vector3(joystick.Horizontal*speed,rbPlayer.velocity.y,
                                                joystick.Vertical*speed);
        rbPlayer.velocity=directionMObile.normalized*10;

        if(directionMObile!=Vector3.zero){
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation
            (directionMObile),Time.deltaTime*7.3f);
        }
    }


}
