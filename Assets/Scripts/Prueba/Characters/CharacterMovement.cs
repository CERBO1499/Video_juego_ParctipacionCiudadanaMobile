using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{

    Rigidbody rbPlayer;
    [SerializeField]float speed = 10f;
    
   


    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        PlayerControl();
    }

    void PlayerControl()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal") * speed, rbPlayer.velocity.y, Input.GetAxisRaw("Vertical") * speed);
            rbPlayer.velocity = direction.normalized * 10;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 7.3f);
            }
        } else
        {
            rbPlayer.velocity = Vector3.zero;
        }
        

        

        
    }
}
