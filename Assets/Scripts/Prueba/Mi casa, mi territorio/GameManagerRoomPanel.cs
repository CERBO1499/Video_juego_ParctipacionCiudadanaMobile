using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace CasaTerritorio
{
    public class GameManagerRoomPanel : MonoBehaviour
    {
#pragma warning disable CS0649

        #region Constants
        const float TIME_TO_ERASE = 0.1f;
        #endregion


        #region Information
        bool isEraseable;
        #endregion

        #region Components
        [SerializeField] private RectTransform parentCopies;

        [SerializeField] private int panelToActivate;
        [Header("Panel")]
        [SerializeField] private Image roomsPanel;
        [SerializeField] private Image title;
        [SerializeField] private RoomElements[] rooms;
        #endregion

        #region Statics
        public static GameManagerRoomPanel instance;
        #endregion

        #region Properties
        public RoomElements PcurrentRoom { get => rooms[PcurrentRoomIndex]; }

        private int PcurrentRoomIndex { get; set; }
        #endregion

        private void Awake()
        {
            instance = this;

            isEraseable = false;

            roomsPanel.gameObject.SetActive(false);

            ObjectToPut.OnGrabbingObject += UpdateCurrentGrabbedObject;
        }

        public void PanelActivate(int roomIndex)
        {
            PcurrentRoomIndex = roomIndex;

            foreach (Transform obj in roomsPanel.transform)
            {
                if (obj.CompareTag("Images") == false) obj.gameObject.SetActive(false);
            }

            roomIndex = Mathf.Clamp(roomIndex, 0, rooms.Length - 1);

            roomsPanel.gameObject.SetActive(true);
            title.sprite = rooms[roomIndex].title;
            roomsPanel.sprite = rooms[roomIndex].roomBackground;
            rooms[roomIndex].objectsScroll.SetActive(true);
        }

        private void UpdateCurrentGrabbedObject(Image grabbedObject)
        { 
            PcurrentGrabbedObject = grabbedObject.gameObject;
        }

        public void TurnOffCurrentGrabbedObject()
        {
            //Invoke("WaitToErase", TIME_TO_ERASE);
            StartCoroutine(WaitToEraseCoroutine());
        }

        IEnumerator WaitToEraseCoroutine()
        {
            yield return new WaitForSecondsRealtime(TIME_TO_ERASE);

            WaitToErase();
        }

        public void PointerState(bool state)
        {
            isEraseable = state;
        }
        public void PointerState(float time)
        {
            Invoke("TurnOffPointer", time);
        }

        void TurnOffPointer() {
            isEraseable = false;
        }

        void WaitToErase()
        {
            if (Input.touchCount <= 0)
            {
                if (isEraseable == true)
                {
                    isEraseable = false;
                    PcurrentGrabbedObject.SetActive(false);
                }
            }
        }

        public GameObject PcurrentGrabbedObject { get; private set; }
    }
}