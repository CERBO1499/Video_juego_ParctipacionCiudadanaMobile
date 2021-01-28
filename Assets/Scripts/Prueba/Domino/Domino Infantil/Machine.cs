﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Diverdomino
{
    public class Machine : MonoBehaviour
    {
        #region Information
        const float SECS_BETWEEN_PLAYERS_MIN = 2f;
        const float SECS_BETWEEN_PLAYERS_MAX = 4f;

        [SerializeField] TextMeshProUGUI txtPieces;
        int pieceCant;
        bool enemyTurn;
        GameObject[] openPieces;
        List<PieceDomino> enemyPieces;
        //Coroutine timeBtwPlayers;
        #endregion

        void Awake()
        {
            openPieces = new GameObject[2];

            enemyTurn = true;
        }
        void Start()
        {
            GameManager.instance.OnPassBtn -= PassTurn;
            GameManager.instance.OnPassBtn += PassTurn;

            PieceDomino.OnPieceInPlace -= UpdatePossiblePieces;
            PieceDomino.OnPieceInPlace += UpdatePossiblePieces;

            /*GameManager.instance.OnPassBtn = () =>
            {
                timeBtwPlayers = StartCoroutine(WaitSecsBetweenPlayers()); 
            };*/

            enemyPieces = GameManager.instance.PiecesToMachine;
        }

        public void UpdatePossiblePieces(GameObject pieceNum, Side side, bool userTurn)
        {
            enemyTurn = !userTurn;
            try
            {
                var temp = openPieces[((_Side = side) == Side.Izq ? 0 : 1)];
                if(side == Side.Izq) {
                    if (temp.transform.GetChild(0).GetComponent<Keper>().Sidde == Side.Izq) {
                        temp.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else { 
                        temp.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
                else {
                    if (temp.transform.GetChild(1).GetComponent<Keper>().Sidde == Side.Izq)
                    {
                        temp.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        temp.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
            }
            catch { }

            openPieces[((_Side = side) == Side.Izq ? 0 : 1)] = pieceNum;

            StopAllCoroutines();
            StartCoroutine(WaitSecsBetweenPlayers());
        }

        public List<PieceDomino> VerifyAvailablePieces(Side side) {
            int openNumber = 0;
            List<PieceDomino> availablePieces = new List<PieceDomino>();
            Keper keper;

            keper = openPieces[(int)side].transform.GetChild(0).GetComponent<Keper>();
            
            if(side == Side.Izq) {
                openNumber = (keper.Sidde == Side.Izq) ? (int)keper.NumPiece : (int)openPieces[0].transform.GetChild(1).GetComponent<Keper>().NumPiece;
            }
            else { 
                openNumber = (keper.Sidde == Side.Dere) ? (int)keper.NumPiece : (int)openPieces[1].transform.GetChild(1).GetComponent<Keper>().NumPiece;
            }

            //  Verify if there are available pieces to play with.
            for(int i = 0; i < enemyPieces.Count; i++) {
                for(int h = 0; h < 2; h++) { 
                    if ((int)enemyPieces[i].gameObject.transform.GetChild(h).GetComponent<Keper>().NumPiece == openNumber) {
                        availablePieces.Add(enemyPieces[i]);
                    }
                }
            }

            return availablePieces;
        }

        public void PlayPiece(Side side) {
            try { 
                var selectedPiece = VerifyAvailablePieces(side)[0];

                selectedPiece.gameObject.SetActive(true);
                selectedPiece.transform.SetParent(openPieces[(int)_Side].transform.parent);

                if (side == Side.Izq) {
                //Debug.Log("Piece played " + openPieces[0]);

                    selectedPiece.MachinePlay(
                        openPieces[0].transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde == Side.Izq ?
                            openPieces[0].transform.GetChild(0).gameObject : openPieces[0].transform.GetChild(1).gameObject);
                }
                else {
                    selectedPiece.MachinePlay(
                        openPieces[1].transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde == Side.Izq ?
                            openPieces[1].transform.GetChild(1).gameObject : openPieces[1].transform.GetChild(0).gameObject);
                }

                enemyPieces.Remove(selectedPiece);
            }
            catch { 
            //Debug.Log("No hay más jugadas por el lado " + _Side);
            }

            //StopCoroutine(timeBtwPlayers);
        }

        IEnumerator WaitSecsBetweenPlayers() {
            yield return new WaitForSeconds(Random.Range(SECS_BETWEEN_PLAYERS_MIN, SECS_BETWEEN_PLAYERS_MAX));

            if (enemyTurn == true)
            {
                PlayPiece(Random.Range(0,2) == 0? Side.Izq : Side.Dere);
            }
        }

        void PassTurn() {
            StopAllCoroutines();

            enemyTurn = true;
            StartCoroutine(WaitSecsBetweenPlayers());
        }

        private Side _Side { get; set; }
    }
}

