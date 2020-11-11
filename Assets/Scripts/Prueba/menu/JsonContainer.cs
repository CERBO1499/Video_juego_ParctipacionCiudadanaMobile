using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonContainer : MonoBehaviour
{
    #region Static
    public static JsonContainer instance;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] JsonCharacter character;
    public JsonCharacter Pcharacter
    {
        get { return character; } set { character = value; }
    }
    [SerializeField] JsonId id;
    public JsonId Pid
    {
        get { return id; }
        set { id = value; }
    }
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}