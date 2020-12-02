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
            if (Input.GetAxis("Horizontal") != 0)
                rigibody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0f).normalized * 5f;
            else
                rigibody.velocity = Vector3.zero;

            yield return null;
        }
    }

    public void Move(Vector2 axis)
    {
        if (Application.platform == RuntimePlatform.Android)
            rigibody.velocity = axis * 5f;
    }
}