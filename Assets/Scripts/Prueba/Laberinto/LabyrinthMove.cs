using System.Collections;
using UnityEngine;

public class LabyrinthMove : MonoBehaviour
{
    #region Components
    [Header("Components")]
    [SerializeField] Rigidbody2D rigibody;
    #endregion

    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
            StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine()
    {
        Debug.Log("Update Start");

        while (true)
        {
            rigibody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 5f;

            yield return null;
        }
    }

    public void Move(Vector2 axis)
    {
        if (Application.platform == RuntimePlatform.Android)
            rigibody.velocity = axis * 5f;
    }
}