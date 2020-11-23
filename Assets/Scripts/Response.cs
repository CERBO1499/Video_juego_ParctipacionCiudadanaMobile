using System;
using UnityEngine;

public class Response : MonoBehaviour
{
    #region Information
    public Action<string> output;
    #endregion

    public void Receive(string data)
    {
        output(data);
    }
}