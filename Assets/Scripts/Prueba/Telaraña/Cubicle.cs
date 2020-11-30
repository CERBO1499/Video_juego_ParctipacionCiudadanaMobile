using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Cubicle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Information
    public GameObject image = null;
    bool drag;
    LineRenderer line;
    int point;
    bool anchored;
    #endregion

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TelarañaManager.instance.line)
        {
            GameObject line = new GameObject("Line", typeof(LineRenderer), typeof(WebLine));

            line.transform.SetParent(TelarañaManager.instance.Plines);

            line.transform.localPosition = Vector3.zero;

            line.transform.localScale = Vector3.one;

            this.line = line.GetComponent<LineRenderer>();

            this.line.material = TelarañaManager.instance.lineMaterial;

            this.line.sortingOrder = 1;

            this.line.SetPosition(0, GetComponent<RectTransform>().position);

            point = 1;

            line.GetComponent<WebLine>().initialPosition = GetComponent<RectTransform>();

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

            line.SetPosition(point, Camera.main.ScreenToWorldPoint(screenPoint));

            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> resoults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerEventData, resoults);

            bool anchored = false;

            for (int i = 0; i < resoults.Count; i++)
            {
                if (resoults[i].gameObject.tag == "Cubicle")
                {
                    line.SetPosition(point, resoults[i].gameObject.GetComponent<RectTransform>().position);

                    line.GetComponent<WebLine>().finalPosition = resoults[i].gameObject.GetComponent<RectTransform>();

                    anchored = true;

                    break;
                }
            }

            this.anchored = anchored;

            yield return null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (TelarañaManager.instance.line)
        {
            if (line.positionCount == 2)
            {
                if (!anchored)
                    Destroy(line.gameObject);

                anchored = false;

                drag = false;

                line = null;
            }
        }
    }
}