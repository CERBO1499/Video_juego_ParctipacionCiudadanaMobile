using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpiderwebOption : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Informtion
    bool drag;
    GameObject cubicle;
    int lastIndex;
    #endregion

    #region Components
    RectTransform root;
    RectTransform rect;
    Image image;
    #endregion

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        root = GetComponent<RectTransform>().parent.GetComponent<RectTransform>();

        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TelarañaManager.instance.drag)
        {
            lastIndex = rect.GetSiblingIndex();

            rect.SetParent(rect.parent.parent.parent.parent);

            root.sizeDelta = new Vector3(root.sizeDelta.x, root.sizeDelta.y - (rect.sizeDelta.y + 240));

            StartCoroutine(DragCoroutine(eventData));
        }
    }

    IEnumerator DragCoroutine(PointerEventData pointerEventData)
    {
        drag = true;

        while (drag)
        {
            Vector3 screenPoint = Input.mousePosition;

            screenPoint.z = 90.0f;

            rect.position = Camera.main.ScreenToWorldPoint(screenPoint);

            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> resoults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerEventData, resoults);

            cubicle = null;

            for (int i = 0; i < resoults.Count; i++)
            {
                if (resoults[i].gameObject.tag == "Cubicle")
                {
                    cubicle = resoults[i].gameObject;

                    break;
                }
            }

            yield return null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        drag = false;

        if (cubicle != null)
        {
            Cubicle c = cubicle.GetComponent<Cubicle>();

            if (c.image != null)
            {
                RectTransform anotherRect = c.image.GetComponent<RectTransform>();

                anotherRect.SetParent(root);

                anotherRect.localPosition = new Vector3(anotherRect.localPosition.x, anotherRect.localPosition.y, 0f);

                root.sizeDelta = new Vector3(root.sizeDelta.x, root.sizeDelta.y + (rect.sizeDelta.y + 240));

                c.image.SetActive(true);
            }
            else
                TelarañaManager.instance.images++;

            c.image = gameObject;

            cubicle.GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Image>().sprite = image.sprite;

            gameObject.SetActive(false);

            if (TelarañaManager.instance.images == 4)
            {
                TelarañaManager.instance.images = 0;

                TelarañaManager.instance.SetCircle();
            }
        }
        else
        {
            rect.SetParent(root);

            rect.SetSiblingIndex(lastIndex);

            rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);

            root.sizeDelta = new Vector3(root.sizeDelta.x, root.sizeDelta.y + (rect.sizeDelta.y + 240));
        }
    }
}