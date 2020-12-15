using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#region Delegate
public delegate LineRenderer LineRendererAction();
#endregion

namespace aldea
{
    public class LineCreator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Static
        public static bool create = true;
        #endregion

        #region Information
        bool drag;
        Letter initialLetter;
        #endregion

        LineRendererAction getLineRenderer;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (create)
            {
                Vector3 point = Input.mousePosition;

                point.z = 100f;

                Collider2D[] colliders = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(point), 1);

                for (int i = 0; i < colliders.Length; i++)
                {
                    Letter letter = colliders[i].gameObject.GetComponent<Letter>();

                    if (letter)
                        initialLetter = letter;
                }

                if (initialLetter != null)
                {
                    GameObject line = new GameObject("Line", typeof(LineRenderer), typeof(WebLine));

                    line.transform.SetParent(transform);

                    line.transform.localPosition = Vector3.zero;

                    line.transform.localScale = Vector3.one;

                    LineRenderer lineRenderer = line.GetComponent<LineRenderer>();

                    lineRenderer.SetPosition(0, initialLetter.gameObject.GetComponent<RectTransform>().position);

                    lineRenderer.startWidth = 3f;

                    lineRenderer.endWidth = 3f;

                    lineRenderer.startColor = new Color(1f, 0f, 0f, 0.5f);

                    lineRenderer.endColor = new Color(1f, 0f, 0f, 0.5f);

                    lineRenderer.numCapVertices = 50;

                    lineRenderer.material = UIManager.instance.PlineMaterial;

                    lineRenderer.sortingOrder = 1;

                    getLineRenderer = () =>
                    {
                        return lineRenderer;
                    };

                    StartCoroutine(DragCoroutine(lineRenderer));
                }
            }
        }

        IEnumerator DragCoroutine(LineRenderer lineRenderer)
        {
            drag = true;

            RectTransform initialLetterRect = initialLetter.gameObject.GetComponent<RectTransform>();

            while (drag)
            {
                Vector3 point = Input.mousePosition;

                point.z = 100f;

                point = Camera.main.ScreenToWorldPoint(point);

                lineRenderer.gameObject.transform.position = (point + initialLetterRect.position) / 2f;

                lineRenderer.SetPosition(1, point);

                yield return null;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (create)
            {
                LineRenderer lineRenderer = getLineRenderer?.Invoke();

                if (lineRenderer)
                {
                    List<Letter> letters = new List<Letter>();

                    RaycastHit2D[] hits = Physics2D.LinecastAll(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));

                    for (int i = 0; i < hits.Length; i++)
                    {
                        Letter letter = hits[i].collider.gameObject.GetComponent<Letter>();

                        if (letter)
                            letters.Add(letter);
                    }

                    if (letters.Contains(initialLetter))
                        letters.Remove(initialLetter);

                    for (int i = 0; i < letters.Count - 1; i++)
                    {
                        if (Mathf.Abs(initialLetter.gameObject.GetComponent<RectTransform>().GetSiblingIndex() - letters[i].gameObject.GetComponent<RectTransform>().GetSiblingIndex())
                            >
                            Mathf.Abs(initialLetter.gameObject.GetComponent<RectTransform>().GetSiblingIndex() - letters[i + 1].gameObject.GetComponent<RectTransform>().GetSiblingIndex()))
                        {
                            Letter letter = letters[i];

                            letters[i] = letters[i + 1];

                            letters[i + 1] = letter;

                            i = -1;
                        }
                    }

                    letters.Insert(0, initialLetter);

                    if (letters.Count > 1)
                    {
                        if (CheckifHorizontal(letters) || CheckifVertical(letters))
                        {
                            lineRenderer.SetPosition(1, letters[letters.Count - 1].gameObject.GetComponent<RectTransform>().position);

                            string word = "";

                            for (int i = 0; i < letters.Count; i++)
                                word += letters[i].letter;

                            for (int i = 0; i < UIManager.instance.PsoupWords.Length; i++)
                            {
                                if (word == UIManager.instance.PsoupWords[i].Pword)
                                {
                                    GameObject line = UIManager.instance.PsoupWords[i].gameObject.GetComponent<RectTransform>().GetChild(0).gameObject;

                                    if (!line.activeSelf)
                                    {
                                        line.SetActive(true);

                                        UIManager.instance.UpdateSoup();
                                    }
                                    else
                                        Destroy(lineRenderer.gameObject);

                                    Clean();

                                    return;
                                }
                            }

                            Destroy(lineRenderer.gameObject);
                        }
                        else
                        {
                            bool diagonal = true;

                            for (int i = 0; i < letters.Count - 1; i++)
                            {
                                diagonal = CheckifDiagonal(letters[i], letters[i + 1]);

                                if (!diagonal)
                                    break;
                            }

                            if (diagonal)
                            {
                                lineRenderer.SetPosition(1, letters[letters.Count - 1].gameObject.GetComponent<RectTransform>().position);

                                string word = "";

                                for (int i = 0; i < letters.Count; i++)
                                    word += letters[i].letter;

                                for (int i = 0; i < UIManager.instance.PsoupWords.Length; i++)
                                {
                                    if (word == UIManager.instance.PsoupWords[i].Pword)
                                    {
                                        GameObject line = UIManager.instance.PsoupWords[i].gameObject.GetComponent<RectTransform>().GetChild(0).gameObject;

                                        if (!line.activeSelf)
                                        {
                                            line.SetActive(true);

                                            UIManager.instance.UpdateSoup();
                                        }
                                        else
                                            Destroy(lineRenderer.gameObject);

                                        Clean();

                                        return;
                                    }
                                }

                                Destroy(lineRenderer.gameObject);
                            }
                            else
                                Destroy(lineRenderer.gameObject);
                        }
                    }
                    else
                        Destroy(lineRenderer.gameObject);
                }

                Clean();
            }
        }

        bool CheckifHorizontal(List<Letter> letters)
        {
            for (int i = 0; i < letters.Count - 1; i++)
            {
                if (letters[i].position.y != letters[i + 1].position.y)
                    return false;
            }

            return true;
        }

        bool CheckifVertical(List<Letter> letters)
        {
            for (int i = 0; i < letters.Count - 1; i++)
            {
                if (letters[i].position.x != letters[i + 1].position.x)
                    return false;
            }

            return true;
        }

        bool CheckifDiagonal(Letter first, Letter second)
        {
            return ((second.position.x == first.position.x + 1 || second.position.x == first.position.x - 1) 
                    &&
                    (second.position.y == first.position.y + 1 || second.position.y == first.position.y - 1));
        }

        void Clean()
        {
            drag = false;

            initialLetter = null;

            getLineRenderer = null;
        }
    }
}