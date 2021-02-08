using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Uno
{
    public class OptionsLineRender : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {

        #region Inforamtion
        [SerializeField] Material material;
        [SerializeField] RectTransform parent;
        bool drag;
        bool anchored;
        LineRenderer line;
        int point;
        GameObject posibility;
        ActivityLines activityLines;

        #endregion

        private void Awake()
        {
            activityLines = GetComponentInParent<ActivityLines>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GameObject line = new GameObject("Line", typeof(LineRenderer), typeof(WebLine));

            line.transform.localPosition = Vector3.zero;

            line.transform.localScale = Vector3.one;

            line.transform.SetParent(parent);

            this.line = line.GetComponent<LineRenderer>();

            this.line.material = material;

            this.line.sortingOrder = 1;

            this.line.SetPosition(0, GetComponent<RectTransform>().position);

            point = 1;

            line.GetComponent<WebLine>().initialPosition = GetComponent<RectTransform>();

            StartCoroutine(DragCoroutine(eventData));

        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            /* if (line.positionCount == 2)
             {*/
            if (posibility == null)
            {
                Destroy(line.gameObject);
            }
            else
            {
                activityLines.SelectOption(posibility);

                anchored = false;

                drag = false;

                line = null;
            }

            /* }*/
        }
        IEnumerator DragCoroutine(PointerEventData pointerEventData)
        {
            drag = true;

            while (drag && line != null)
            {
                Vector3 screenPoint = Input.mousePosition;

                screenPoint.z = 90.0f;

                line.SetPosition(point, Camera.main.ScreenToWorldPoint(screenPoint));

                pointerEventData.position = Input.mousePosition;

                List<RaycastResult> resoults = new List<RaycastResult>();

                EventSystem.current.RaycastAll(pointerEventData, resoults);

                bool anchored = false;

                posibility = null;

                for (int i = 0; i < resoults.Count; i++)
                {
                    if (resoults[i].gameObject.tag == "Option To Select")
                    {

                        posibility = resoults[i].gameObject;

                        line.SetPosition(point, resoults[i].gameObject.GetComponent<RectTransform>().position);

                        line.GetComponent<WebLine>().finalPosition = resoults[i].gameObject.GetComponent<RectTransform>();

                        anchored = true;

                        break;
                    }

                    this.anchored = anchored;

                    yield return null;
                }

                yield return null;
            }
        }
    }
}