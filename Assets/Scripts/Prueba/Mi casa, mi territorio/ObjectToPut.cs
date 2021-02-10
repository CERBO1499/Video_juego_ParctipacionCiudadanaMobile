using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace CasaTerritorio
{
    [RequireComponent(typeof(Image))]
    public class ObjectToPut : MonoBehaviour, IPointerDownHandler
    {
        #region Information
        
        bool drag;
        bool takeFromScroll;
        #endregion

        #region Components
        RectTransform rect;
        Image myImage;
        Image myObjCopy;
        Sprite mySprite;
        #endregion

        #region Properties
        public bool PtakeFromScroll { get => takeFromScroll; set => takeFromScroll = value; }
        public ObjectToPut PcopiedImage { get; set; }
        #endregion

        #region Events
        public static Action<Image> OnGrabbingObject;
        #endregion

        private void Awake()
        {
            rect = GetComponent<RectTransform>();

            myImage = GetComponent<Image>();

            takeFromScroll = true;

            mySprite = myImage.sprite;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartCoroutine(DragCoroutine(eventData));
        }

        IEnumerator DragCoroutine(PointerEventData pointerEventData)
        {
            float mousePosition = Input.mousePosition.x;

            float t = Time.time;

            while (Time.time <= t + 0.05f)
                yield return null;

            if (Math.Abs(Input.mousePosition.x - mousePosition) >= 10f)
                yield break;

            if (takeFromScroll == true) CopyImage();
            else myObjCopy = GetComponent<Image>();

            drag = true;

            OnGrabbingObject(myObjCopy);

            while (drag && myObjCopy != null)
            {
                Vector3 screenPoint = Input.mousePosition;

                screenPoint.z = 100.0f;
                
                myObjCopy.rectTransform.position = Camera.main.ScreenToWorldPoint(screenPoint);

                pointerEventData.position = Input.mousePosition;

                yield return null;

                if (Application.platform == RuntimePlatform.Android)
                    drag = Input.touchCount > 0;
                else
                    drag = Input.GetMouseButton(0);
            }

            drag = true;
        }
        Image CopyImage()
        {
            GameObject myObj = new GameObject();            
            Image myNewImage = myObj.AddComponent<Image>();
            var v = myObj.AddComponent<ObjectToPut>();

            v.PtakeFromScroll = false;

            myNewImage.sprite = mySprite;
            myNewImage.GetComponent<RectTransform>().SetParent(GameManagerRoomPanel.instance.PcurrentRoom.copiesPanel);

            myNewImage.transform.position = rect.transform.position;
            myNewImage.rectTransform.localScale = Vector3.one;

            myNewImage.gameObject.SetActive(true);

            myNewImage.preserveAspect = true;
            myNewImage.SetNativeSize();
            myNewImage.rectTransform.sizeDelta *= 5f;

            return myObjCopy = myNewImage;
        }
    }
}

