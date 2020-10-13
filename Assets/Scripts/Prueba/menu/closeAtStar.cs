using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeAtStar : MonoBehaviour
{

    private void Awake()
    {
        gameObject.SetActive(true);
    }
    void Start()
    {
        gameObject.SetActive(false);
    }

}
