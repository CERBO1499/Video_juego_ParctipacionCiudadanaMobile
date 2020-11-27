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

            rect.SetParent(rect.parent.parent.parent.parent, true);

            root.sizeDelta = new Vector3(root.sizeDelta.x, root.sizeDelta.y - (rect.sizeDelta.y + 240));

            StartCoroutine(DragCoroutine(eventData));
        }
    }

    IEnumerator DragCoroutine(PointerEventData pointerEventData)
    {
        drag = true;

        while (drag)
        {
            rect.position = Input.mousePosition;

            pointerEventData.position = rect.position;

            List<RaycastResult> resoults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerEventData, resoults);

            cubicle = null;

            for (int i = 0; i < resoults.Count; i++)
            {
                if (resoults[i].gameObject.tag == "Cublicle")
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
                c.image.GetComponent<RectTransform>().SetParent(root, true);

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
            rect.SetParent(root, true);

            rect.SetSiblingIndex(lastIndex);

            root.sizeDelta = new Vector3(root.sizeDelta.x, root.sizeDelta.y + (rect.sizeDelta.y + 240));
        }
    }
}