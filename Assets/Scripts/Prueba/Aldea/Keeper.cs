using UnityEngine;

public class Keeper : MonoBehaviour
{
    #region Information
    [Header("Information")]
    public GameObject keeped;
    #endregion

    public void Clear()
    {
        keeped = null;
    }
}