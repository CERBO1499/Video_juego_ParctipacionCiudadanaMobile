using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace CasaTerritorio
{
    public class ObjectToPut : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Information
        
        bool drag;
        #endregion
        #region Components
        RectTransform rect;
        Image myImage;
        Image myObjCopy;
        Sprite mySprite;
        #endregion

        private void Awake()
        {
            rect = GetComponent<RectTransform>();

            myImage = GetComponent<Image>();

            mySprite = myImage.sprite;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
            StartCoroutine(DragCoroutine(eventData));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            drag = false;
        }
        IEnumerator DragCoroutine(PointerEventData pointerEventData)
        {
            CopyImage();

            drag = true;

            while (drag && myObjCopy != null)
            {
                Vector3 screenPoint = Input.mousePosition;

                screenPoint.z = 90.0f;
                
                myObjCopy.rectTransform.position = Camera.main.ScreenToWorldPoint(screenPoint);

                pointerEventData.position = Input.mousePosition;

                yield return null;
            }
        }
        Image CopyImage()
        {
            GameObject myObj = new GameObject();            

            Image myNewImage = myObj.AddComponent<Image>();

            myNewImage.sprite = mySprite;

            myNewImage.GetComponent<RectTransform>().SetParent(GameManager.instance.PparentCopyes);

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

