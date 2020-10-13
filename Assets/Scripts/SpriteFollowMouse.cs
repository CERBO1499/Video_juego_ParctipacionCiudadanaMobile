using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpriteFollowMouse : MonoBehaviour
{
    public Texture2D cursorArrow;
    Texture2D cursorNormal=null;
    bool cursorEspecial;
    private void Start()
    {
        cursorEspecial = false;
        //Cursor.SetCursor(cursorArrow,Vector2.zero,CursorMode.ForceSoftware);
        
    }


    public void Update()
    {
        if (cursorEspecial)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }
    
    }


    public void SpriteON()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.Auto);
        cursorEspecial= true;
    }
    public void SpriteOff()
    {
        cursorEspecial = false;
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }

}