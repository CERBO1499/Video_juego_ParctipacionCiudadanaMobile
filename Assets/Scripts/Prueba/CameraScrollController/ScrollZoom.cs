using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class ScrollZoom : MonoBehaviour
{


    //[SerializeField] private float zoomLerpSeed = 10f;
    //[SerializeField]GameObject cmVirtualCamera;
    private Camera cam;
    private float targetZoom;
    private float zoomFactor;
    [SerializeField] private float zoomLerpSpeed = 10;

    private void Start()
    {
        cam = Camera.main;
        targetZoom = cam.fieldOfView;

    }
    private void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom,4.5f,8f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,targetZoom,Time.deltaTime*zoomLerpSpeed);
    }



}
