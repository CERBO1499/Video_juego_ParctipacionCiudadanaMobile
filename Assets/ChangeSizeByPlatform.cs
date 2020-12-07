using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizeByPlatform : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 1f);

            GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x * 0.8f, GetComponent<RectTransform>().anchoredPosition.y * 0.8f);
        }
    }
}
