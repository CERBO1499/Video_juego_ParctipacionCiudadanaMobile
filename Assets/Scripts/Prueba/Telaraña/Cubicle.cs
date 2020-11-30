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
    #endregion

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TelarañaManager.instance.line)
        {
            GameObject line = new GameObject("Line", typeof(LineRenderer));

            line.transform.SetParent(TelarañaManager.instance.Plines);

            line.transform.localPosition = Vector3.zero;

            line.transform.localScale = Vector3.one;

            this.line = line.GetComponent<LineRenderer>();

            this.line.material = TelarañaManager.instance.lineMaterial;

            this.line.sortingOrder = 1;

            Vector3 screenPoint = Input.mousePosition;

            screenPoint.z = 90.0f;

            this.line.SetPosition(0, Camera.main.ScreenToWorldPoint(screenPoint));

            point = 1;

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

            GameObject cubicle = null;

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
        if (line.positionCount == 2)
        {
            Destroy(line.gameObject);

            line = null;
        }
    }
}