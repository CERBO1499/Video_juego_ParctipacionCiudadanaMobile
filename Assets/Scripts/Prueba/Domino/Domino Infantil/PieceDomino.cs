﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public enum TypePiece
{
    Double,
    Single
}

namespace Diverdomino
{
    public class PieceDomino : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        #region Information
        [SerializeField] TypePiece typeOfPiece;
        int lastIndex;
        bool drag;
        bool isMachinePiece;
        public bool isInPosition = true;
        Image rayCastToUnactive;
        GameObject posibilty;
        Coroutine dragCoroutine;
        #endregion

        #region Components
        RectTransform rect;
        public RectTransform Prect { get => rect; set => rect = value; }
        public TypePiece TypeOfPiece { get => typeOfPiece; set => typeOfPiece = value; }

        List<Transform> difPiece = new List<Transform>();

        Image img;

        #endregion

        #region Events
        public System.Action OnFirstPiece;
        public static System.Action OnWrongPiece;
        public static System.Action<GameObject, Side, bool> OnPieceInPlace;
        #endregion

        void Awake()
        {
            rayCastToUnactive = GetComponent<Image>();

            rect = GetComponent<RectTransform>();

            img = GetComponent<Image>();

            isMachinePiece = false;

            gameObject.SetActive(false);

            foreach (Transform item in rect.gameObject.transform.GetChild(2))
            {
                difPiece.Add(item);
            }
            for (int i = 0; i < difPiece.Count; i++)
            {
                difPiece[i].GetComponent<DiferentationPiece>();
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           /* if (isInPosition)
                img.color = new Color(1f, 1f, 1f, 0.5f);*/
        }

        public void OnPointerExit(PointerEventData eventData)
        {
         /*   img.color = Color.white;*/
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GameManager.instance.drag && isInPosition)
            {
                lastIndex = rect.GetSiblingIndex();

                GameManager.instance.ScrollToUnactive.enabled = false;

                rect.SetParent(GameManager.instance.ParentToPieces);

                dragCoroutine = StartCoroutine(DragCoroutine(eventData));
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

                bool found = false;

                for (int i = 0; i < resoults.Count; i++)
                {
                    if (resoults[i].gameObject.tag == "Piece Domino")
                    {

                        found = true;

                        Posibility = posibilty = resoults[i].gameObject;

                        gameObject.transform.localEulerAngles = posibilty.transform.localEulerAngles;

                        if (posibilty.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[0].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString()
                            || posibilty.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[1].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString()
                            || posibilty.gameObject.GetComponent<Keper>().NumPiece == NumberPiece.firstPiece)
                        {
                            switch (TypeOfPiece)
                            {
                                case TypePiece.Double:
                                    gameObject.transform.localEulerAngles = Vector3.zero;
                                    break;
                                case TypePiece.Single:
                                    switch (posibilty.gameObject.GetComponent<Keper>().Sidde)
                                    {
                                        case Side.Izq:
                                            if (posibilty.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[0].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString())
                                            {
                                                gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                                                gameObject.transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde = Side.Dere;
                                                gameObject.transform.GetChild(1).gameObject.GetComponent<Keper>().Sidde = Side.Izq;
                                            }

                                            if (posibilty.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[1].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString())
                                            {
                                                gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                                                gameObject.transform.GetChild(1).gameObject.GetComponent<Keper>().Sidde = Side.Dere;
                                                gameObject.transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde = Side.Izq;
                                            }

                                            break;
                                        case Side.Dere:
                                            if (posibilty.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[0].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString())
                                            {
                                                gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                                                gameObject.transform.GetChild(1).gameObject.GetComponent<Keper>().Sidde = Side.Dere;
                                                gameObject.transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde = Side.Izq;
                                            }

                                            if (posibilty.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[1].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString())
                                            {
                                                gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                                                gameObject.transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde = Side.Dere;
                                                gameObject.transform.GetChild(1).gameObject.GetComponent<Keper>().Sidde = Side.Izq;
                                            }
                                            break;
                                        default:
                                            break;
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            posibilty = null;
                        }

                        break;
                    }
                }

                if (!found)
                    posibilty = null;

                yield return null;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            drag = false;

            GameManager.instance.ScrollToUnactive.enabled = true;

            if (isInPosition)
            {
                if (posibilty == null)
                {
                    rect.SetParent(GameManager.instance.Ppieces);

                    rect.SetSiblingIndex(lastIndex);

                    rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);

                    rect.localEulerAngles = Vector3.zero;

                    OnWrongPiece();

                }
                else
                {

                    Vector3 finalPositionleft = new Vector3(posibilty.gameObject.transform.position.x + 7.5f, posibilty.gameObject.transform.position.y, posibilty.gameObject.transform.position.z);

                    Vector3 finalPositionRigth = new Vector3(posibilty.gameObject.transform.position.x - 7.5f, posibilty.gameObject.transform.position.y, posibilty.gameObject.transform.position.z);

                    if (posibilty.gameObject.GetComponent<Keper>().NumPiece == NumberPiece.firstPiece)
                        rect.position = posibilty.GetComponent<RectTransform>().position;

                    else
                    {
                        switch (TypeOfPiece)
                        {
                            case TypePiece.Double:
                                switch (posibilty.gameObject.GetComponent<Keper>().Sidde)
                                {
                                    case Side.Izq:
                                        rect.gameObject.transform.position = finalPositionleft;
                                        break;
                                    case Side.Dere:
                                        rect.gameObject.transform.position = finalPositionRigth;
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case TypePiece.Single:
                                rect.position = posibilty.GetComponent<RectTransform>().position;
                                break;
                            default:
                                break;
                        }

                    }

                    posibilty.gameObject.GetComponent<Image>().raycastTarget = false;

                    posibilty = null;

                    OnFirstPiece?.Invoke();

                    PieceInPosition();
                }
            }

        }

        void PieceInPosition()
        {
            if(isInPosition == true || isMachinePiece == true) OnPieceInPlace?.Invoke(gameObject, Posibility.GetComponent<Keper>().Sidde, isMachinePiece);

            try { 
                StopCoroutine(dragCoroutine);
            }
            catch {}

            rayCastToUnactive.raycastTarget = false;

            foreach (Transform item in transform) item.gameObject.SetActive(true);

            int i = (TypeOfPiece == TypePiece.Double) ? 2 : ((Posibility.GetComponent<Keper>().Sidde == Side.Dere) ? 1 : 0);

            
            switch (i)
            {
                case 0:
                    if (transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde == Side.Dere)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                        transform.GetChild(1).gameObject.SetActive(false);
                    break;

                case 1:
                    if (transform.GetChild(1).gameObject.GetComponent<Keper>().Sidde == Side.Izq)
                        transform.GetChild(1).gameObject.SetActive(false);
                    else
                        transform.GetChild(0).gameObject.SetActive(false);
                    break;

                case 2:
                    if (Posibility.GetComponent<Keper>().NumPiece != NumberPiece.firstPiece)
                    {
                        if (Posibility.GetComponent<Keper>().Sidde == Side.Dere)
                        {
                            if (transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde == Side.Izq)
                                transform.GetChild(0).gameObject.SetActive(false);
                            else
                                transform.GetChild(1).gameObject.SetActive(false);
                        }
                        else
                        {
                            if (transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde == Side.Dere)
                                transform.GetChild(0).gameObject.SetActive(false);
                            else
                                transform.GetChild(1).gameObject.SetActive(false);
                        }
                    }
                    break;
            }
            
            isInPosition = false;

            Posibility.gameObject.SetActive(false);
        }

        public void SetAsMachinePiece()
        {
            isInPosition = false;
            isMachinePiece = true;
        }
        public void MachinePlay(GameObject placeForPiece)
        {
            Posibility = placeForPiece;

            Vector3 finalPositionleft = new Vector3(Posibility.transform.position.x + 7.5f, Posibility.transform.position.y, Posibility.transform.position.z);

            Vector3 finalPositionRigth = new Vector3(Posibility.transform.position.x - 7.5f, Posibility.transform.position.y, Posibility.transform.position.z);

            if (Posibility.GetComponent<Keper>().NumPiece == NumberPiece.firstPiece)
            {
                rect.position = Posibility.GetComponent<RectTransform>().position;
            }

            else
            {
                switch (TypeOfPiece)
                {
                    case TypePiece.Double:
                        gameObject.transform.localEulerAngles = Vector3.zero;

                        switch (Posibility.GetComponent<Keper>().Sidde)
                        {
                            case Side.Izq:
                                rect.gameObject.transform.position = finalPositionleft;
                                break;
                            case Side.Dere:
                                rect.gameObject.transform.position = finalPositionRigth;
                                break;
                            default:
                                break;
                        }
                        break;
                    case TypePiece.Single:
                        rect.position = Posibility.GetComponent<RectTransform>().position;

                        switch (Posibility.GetComponent<Keper>().Sidde) {
                            case Side.Izq:
                                if (Posibility.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[0].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString())
                                {
                                    gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                                    gameObject.transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde = Side.Dere;
                                    gameObject.transform.GetChild(1).gameObject.GetComponent<Keper>().Sidde = Side.Izq;
                                }

                                if (Posibility.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[1].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString())
                                {
                                    gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                                    gameObject.transform.GetChild(1).gameObject.GetComponent<Keper>().Sidde = Side.Dere;
                                    gameObject.transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde = Side.Izq;
                                }
                                break;

                            case Side.Dere:
                                if (Posibility.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[0].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString())
                                {
                                    gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                                    gameObject.transform.GetChild(1).gameObject.GetComponent<Keper>().Sidde = Side.Dere;
                                    gameObject.transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde = Side.Izq;
                                }

                                if (Posibility.gameObject.GetComponent<Keper>().NumPiece.ToString() == difPiece[1].gameObject.GetComponent<DiferentationPiece>().PieceNumber1.ToString())
                                {
                                    gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                                    gameObject.transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde = Side.Dere;
                                    gameObject.transform.GetChild(1).gameObject.GetComponent<Keper>().Sidde = Side.Izq;
                                }
                                break;
                        }
                        break;
                    default:
                        break;
                }
                
            }

            placeForPiece.GetComponent<Image>().raycastTarget = false;

            placeForPiece = null;

            OnFirstPiece?.Invoke();
            PieceInPosition();
        }

        public void SetBlock(bool state) {
            isInPosition = !state;
            img.color = state? new Color(1f, 1f, 1f, 0.5f) : new Color(1f, 1f, 1f, 1f);
        }

        GameObject Posibility { get; set; }
        }
    }