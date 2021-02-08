using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CasaTerritorio
{
    public class GameManager : MonoBehaviour
    {
        #region Static
        public static GameManager instance;
        #endregion

        #region Information
        [Header("Parents")]
        [SerializeField] Transform parentCopyes;


        [Header("Initial Images")]
        [SerializeField] RectTransform initialPanel;
        [SerializeField] RectTransform RooomPanel;
        
        [SerializeField] Sprite inicioHabitacion;
        [SerializeField] Sprite inicioCocina;
        [SerializeField] Sprite inicioPatio;
        [SerializeField] Sprite inicioSalacomedor;

        [Header("Initial leters")]
        [SerializeField] Sprite letrasHabitacion;
        [SerializeField] Sprite letrasCocina;
        [SerializeField] Sprite letrasPatio;
        [SerializeField] Sprite letrasSalacomedor;

        [Header("Images Base and titles")]
        [SerializeField] Sprite Habitacion;
        [SerializeField] Sprite Cocina;
        [SerializeField] Sprite Patio;
        [SerializeField] Sprite SalaComedor;
        [SerializeField] Sprite TitleHabitacion;
        [SerializeField] Sprite TitleCocina;
        [SerializeField] Sprite TitlePatio;
        [SerializeField] Sprite TitleSalaComedor;

        [Header("Puntos a poner")]
        [SerializeField] Image panelInitial;
        [SerializeField] Image letersInitial;
        [SerializeField] Image imageBaseRoom;
        [SerializeField] Image title;
        
        


        #endregion
        #region Encapsulatedfields
        public Transform PparentCopyes { get => parentCopyes; set => parentCopyes = value; }
        #endregion
        private void Awake()
        {
            instance = this;
        }

        public void SelectRoom()
        {
            initialPanel.gameObject.SetActive(false);
            RooomPanel.gameObject.SetActive(true);

            panelInitial.sprite = inicioHabitacion;
            letersInitial.sprite = letrasHabitacion;
            imageBaseRoom.sprite = Habitacion;
            title.sprite = TitleHabitacion;
        }
        public void SelectCocina()
        {

            initialPanel.gameObject.SetActive(false);
            RooomPanel.gameObject.SetActive(true);

            panelInitial.sprite = inicioCocina;
            letersInitial.sprite = letrasCocina;
            imageBaseRoom.sprite = Cocina;
            title.sprite = TitleCocina;
        }
        public void SelectPatio()
        {

            initialPanel.gameObject.SetActive(false);
            RooomPanel.gameObject.SetActive(true);

            panelInitial.sprite = inicioPatio;
            letersInitial.sprite = letrasPatio;
            imageBaseRoom.sprite = Patio;
            title.sprite = TitlePatio;
        }
        public void SelectSalacomedor()
        {
            initialPanel.gameObject.SetActive(false);
            RooomPanel.gameObject.SetActive(true);

            panelInitial.sprite = inicioSalacomedor;
            letersInitial.sprite = letrasSalacomedor;
            imageBaseRoom.sprite = SalaComedor;
            title.sprite = TitleSalaComedor;
        }

        public void BackToMenu()
        {
            RooomPanel.gameObject.SetActive(false);
            initialPanel.gameObject.SetActive(true);
        }
    }
}

