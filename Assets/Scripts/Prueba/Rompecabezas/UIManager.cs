using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Rompecabezas2
{
    public class UIManager : MonoBehaviour
    {
        #region Informartion
        [SerializeField] RectTransform instructionsPanel;
        [SerializeField] RectTransform rompecabezasPanel;
        [SerializeField] GameObject btnFinalizar;
        [SerializeField] GameObject finishPanel;

        [SerializeField] GameObject[] imgQuestions;
        [SerializeField] RectTransform[] Pieces;
        [SerializeField] Button[] btnsRompecabezas;
        [SerializeField] AnimationCurve curve;

        int counterPieces;
        #endregion
        public void NextPanel()
        {
            instructionsPanel.gameObject.SetActive(false);
            rompecabezasPanel.gameObject.SetActive(true);
        }

        public void FinishActivitie()
        {
            rompecabezasPanel.gameObject.SetActive(false);
            finishPanel.SetActive(true);
        }        
        public void SelectPiece(int indexQuestion) 
        {
            switch (indexQuestion)
            {
                case 0:
                    imgQuestions[0].SetActive(true);
                    btnsRompecabezas[0].interactable = false;
                    break;

                case 1:
                    imgQuestions[1].SetActive(true);
                    btnsRompecabezas[1].interactable = false;
                    break;

                case 2:
                    imgQuestions[2].SetActive(true);
                    btnsRompecabezas[2].interactable = false;

                    break;

                case 3:
                    imgQuestions[3].SetActive(true);
                    btnsRompecabezas[3].interactable = false;
                    break;

                case 4:
                    imgQuestions[4].SetActive(true);
                    btnsRompecabezas[4].interactable = false;
                    break;

                case 5:
                    imgQuestions[5].SetActive(true);
                    btnsRompecabezas[5].interactable = false;
                    break;

                case 6:
                    imgQuestions[6].SetActive(true);
                    btnsRompecabezas[6].interactable = false;
                    break;

                case 7:
                    imgQuestions[7].SetActive(true);
                    btnsRompecabezas[7].interactable = false;
                    break;                
                default:
                    break;
            }
        }

        public void ResponseQuestion(int indexQuestion)
        {
            switch (indexQuestion)
            {
                case 0:
                    imgQuestions[0].transform.GetChild(1).gameObject.SetActive(true);
                    imgQuestions[0].transform.GetChild(2).gameObject.SetActive(true);
                    break;

                case 1:
                    imgQuestions[1].transform.GetChild(1).gameObject.SetActive(true);
                    imgQuestions[1].transform.GetChild(2).gameObject.SetActive(true);
                    break;

                case 2:
                    imgQuestions[2].transform.GetChild(1).gameObject.SetActive(true);
                    imgQuestions[2].transform.GetChild(2).gameObject.SetActive(true);
                    break;

                case 3:
                    imgQuestions[3].transform.GetChild(1).gameObject.SetActive(true);
                    imgQuestions[3].transform.GetChild(2).gameObject.SetActive(true);
                    break;

                case 4:
                    imgQuestions[4].transform.GetChild(1).gameObject.SetActive(true);
                    imgQuestions[4].transform.GetChild(2).gameObject.SetActive(true);
                    break;

                case 5:
                    imgQuestions[5].transform.GetChild(1).gameObject.SetActive(true);
                    imgQuestions[5].transform.GetChild(2).gameObject.SetActive(true);
                    break;

                case 6:
                    imgQuestions[6].transform.GetChild(1).gameObject.SetActive(true);
                    imgQuestions[6].transform.GetChild(2).gameObject.SetActive(true);
                    break;

                case 7:
                    imgQuestions[7].transform.GetChild(1).gameObject.SetActive(true);
                    imgQuestions[7].transform.GetChild(2).gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        public void ApearPiece(int indexQuestion)
        {
            switch (indexQuestion)
            {
                case 0:
                    imgQuestions[0].SetActive(false);
                    StartCoroutine(CreecePiece(Pieces[0]));
                    break;

                case 1:
                    imgQuestions[1].SetActive(false);
                    StartCoroutine(CreecePiece(Pieces[1]));
                    break;

                case 2:
                    imgQuestions[2].SetActive(false);
                    StartCoroutine(CreecePiece(Pieces[2]));
                    break;

                case 3:
                    imgQuestions[3].SetActive(false);
                    StartCoroutine(CreecePiece(Pieces[3]));
                    break;

                case 4:
                    imgQuestions[4].SetActive(false);
                    StartCoroutine(CreecePiece(Pieces[4]));
                    break;

                case 5:
                    imgQuestions[5].SetActive(false);
                    StartCoroutine(CreecePiece(Pieces[5]));
                    break;

                case 6:
                    imgQuestions[6].SetActive(false);
                    StartCoroutine(CreecePiece(Pieces[6]));
                    break;

                case 7:
                    imgQuestions[7].SetActive(false);
                    StartCoroutine(CreecePiece(Pieces[7]));
                    break;
                default:
                    break;
            }
            counterPieces++;
            if(counterPieces == 8)
            {
                btnFinalizar.SetActive(true);
            }
        }

        
        IEnumerator CreecePiece(RectTransform piece)
        {
            piece.gameObject.SetActive(true);

            Vector2 iniPosi = Vector2.zero;

            Vector2 finiPosi = Vector2.one;

            float t = Time.time;

            while (Time.time <= t + 0.75f)
            {
                piece.localScale = iniPosi + ((finiPosi - iniPosi) * curve.Evaluate((Time.time) - t) / 0.75f);

                yield return null;
            }

            piece.localScale = finiPosi;
        }
    }
}

