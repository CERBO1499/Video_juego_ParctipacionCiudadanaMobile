using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        bool areQuestionsAnswered;
        #endregion

        #region Components
        [SerializeField] private Image initialPanel;
        [SerializeField] private GameObject fedbackPanel;
        [Header("Room panel")]
        [SerializeField] private Image title;
        [SerializeField] private Image roomsPanel;
        [SerializeField] private Image messagesPanel;
        [SerializeField] private RoomElements[] rooms;
        [SerializeField] public GameObject end;
        #endregion

        #region Statics
        public static GameManagerRoomPanel instance;
        #endregion

        #region Properties
        public RoomElements PcurrentRoom { get => rooms[PcurrentRoomIndex]; }
        public GameObject PcurrentGrabbedObject { get; private set; }


        private int PcurrentRoomIndex { get; set; }
        #endregion

        private void Awake()
        {
            instance = this;

            isEraseable = false;
            areQuestionsAnswered = false;

            roomsPanel.gameObject.SetActive(false);
            initialPanel.gameObject.SetActive(true);

            ObjectToPut.OnGrabbingObject += UpdateCurrentGrabbedObject;
        }

        public void PanelActivate(int roomIndex)
        {
            areQuestionsAnswered = false;

            foreach (RoomElements room in rooms)
            {
                room.copiesPanel.gameObject.SetActive(false);
            }

            TurnOffChildrenGameObjects(roomsPanel.transform, "Images");
            TurnOffChildrenGameObjects(messagesPanel.transform, "Images");


            messagesPanel.gameObject.SetActive(true);

            var introID = messagesPanel.transform.GetChild(0).CompareTag("Finish") == true ? 1 : 0;
            var intro = messagesPanel.transform.GetChild(introID);
            var introImgs = intro.GetComponentsInChildren<Image>();
            intro.gameObject.SetActive(true);
            introImgs[1].sprite = rooms[roomIndex].intro;
            introImgs[0].sprite = rooms[roomIndex].introBackground;


            PcurrentRoomIndex = roomIndex;

            roomIndex = Mathf.Clamp(roomIndex, 0, rooms.Length - 1);

            roomsPanel.gameObject.SetActive(true);
            title.sprite = rooms[roomIndex].title;
            roomsPanel.sprite = rooms[roomIndex].roomBackground;
            rooms[roomIndex].objectsScroll.SetActive(true);
            rooms[roomIndex].copiesPanel.gameObject.SetActive(true);
        }

        private void UpdateCurrentGrabbedObject(Image grabbedObject)
        { 
            PcurrentGrabbedObject = grabbedObject.gameObject;
        }

        public void TurnOffCurrentGrabbedObject()
        {
            Invoke("WaitToErase", TIME_TO_ERASE);
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
                    if(PcurrentGrabbedObject != null) PcurrentGrabbedObject.SetActive(false);
                }
            }
        }

        void TurnOffChildrenGameObjects(Transform parent) { 
            foreach(Transform obj in parent.transform) {
                obj.gameObject.SetActive(false);
            }
        }
        void TurnOffChildrenGameObjects(Transform parent, string dontTurnOffTag)
        {
            foreach (Transform obj in parent)
            {
                if(obj.CompareTag(dontTurnOffTag) != true) obj.gameObject.SetActive(false);
            }
        }

        public void EndRoom() {
            areQuestionsAnswered = true;

            int outroID = messagesPanel.transform.GetChild(0).CompareTag("Finish") == true ? 0 : 1;

            messagesPanel.gameObject.SetActive(true);
            messagesPanel.transform.GetChild(outroID).gameObject.SetActive(true);
            messagesPanel.transform.GetChild(outroID == 0 ? 1 : 0).gameObject.SetActive(false);
        }

        public void SendAnsweredQuestions(GameObject outro) {
            var inputFields = outro.GetComponentsInChildren<TMP_InputField>();
            int completedFields = 0;

            if (areQuestionsAnswered) { 
                for(int i = 0; i < inputFields.Length; i++) { 
                    if(inputFields[i].text.Length >= 1) {
                        completedFields++;
                    }
                }

                areQuestionsAnswered = completedFields == inputFields.Length;

                if (areQuestionsAnswered)
                {
                    initialPanel.gameObject.SetActive(true);
                    roomsPanel.gameObject.SetActive(false);

                    for (int i = 0; i < inputFields.Length; i++)
                    {
                        inputFields[i].text = "";
                    }

                    int roomsCount = 0;

                    for (int i = 0; i < rooms.Length; i++)
                    {
                        if (!rooms[i].btn.gameObject.activeSelf)
                            roomsCount++;
                    }

                    if (roomsCount == 4)
                        end.SetActive(true);
                }
                else {
                    fedbackPanel.SetActive(true);
                }
            }
            else {
                messagesPanel.gameObject.SetActive(false);
            }

            areQuestionsAnswered = true;
        }

        public void SetAnsweringQuestionsFalse() {
            areQuestionsAnswered = false;
        }

        public void ToMain()
        {
            GetComponent<Scenemanager>().ToMainMenuWithSemilla(40);
        }
    }
}