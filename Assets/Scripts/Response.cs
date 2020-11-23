using System;
using UnityEngine;

public class Response : MonoBehaviour
{
    #region Information
    public Action<string> output;
    #endregion

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Receive(string data)
    {
        output(data);
    }
}