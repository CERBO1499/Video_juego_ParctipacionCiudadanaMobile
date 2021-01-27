using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Diverdomino
{
    public class Machine : MonoBehaviour
    {
        #region Information
        [SerializeField] TextMeshProUGUI txtPieces;
        int pieceCant;
        bool enemyTurn;
        GameObject[] openPieces;
        List<PieceDomino> enemyPieces;
        #endregion

        void Awake()
        {
            openPieces = new GameObject[2];

            enemyTurn = true;

            PieceDomino.OnPieceInPlace += UpdatePossiblePieces;
        }
        void Start()
        {
            enemyPieces = GameManager.instance.PiecesToMachine;
            //StartCoroutine(ActualicePiecesUI());
        }

        public void UpdatePossiblePieces(GameObject pieceNum, Side side)
        {
            openPieces[((_Side = side) == Side.Izq ? 0 : 1)] = pieceNum;

            PlayPiece(_Side);
            enemyTurn = false;
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

            //  Recorre la lista de fichas que tiene el enemigo, verificando que cuenten al menos con un número válido para jugar.
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
            if (enemyTurn == true) {
                try { 
                    var selectedPiece = VerifyAvailablePieces(side)[0];

                    selectedPiece.gameObject.SetActive(true);
                    selectedPiece.transform.SetParent(null);
                    selectedPiece.transform.SetParent(openPieces[(int)_Side].transform.parent);
                    selectedPiece.transform.localPosition = openPieces[(int)_Side].transform.localPosition;
                    selectedPiece.transform.localPosition = new Vector3(selectedPiece.transform.localPosition.x + (_Side == Side.Izq? -600f : 600f), selectedPiece.transform.localPosition.y, selectedPiece.transform.localPosition.z);


                    if (side == Side.Izq)
                    {
                    Debug.Log("Piece played " + openPieces[0]);
                        selectedPiece.MachinePlay(openPieces[0].transform.GetChild(0).GetComponent<Keper>().Sidde == Side.Izq ?
                            openPieces[0].transform.GetChild(0).gameObject : openPieces[0].transform.GetChild(1).gameObject);
                    }
                    else
                    {
                        selectedPiece.MachinePlay(selectedPiece.transform.GetChild(0).gameObject.GetComponent<Keper>().Sidde == Side.Izq ?
                            openPieces[1].transform.GetChild(1).gameObject : openPieces[1].transform.GetChild(0).gameObject);
                    }

                    enemyPieces.Remove(selectedPiece);
                    }
                    catch { 
                Debug.Log("No hay más jugadas por el lado " + _Side);
                    }
                }
            }

        void PickPieceMachine()
        {

        }

        /*
        IEnumerator ActualicePiecesUI()
        {
            yield return new WaitForSeconds(1f);
            pieceCant = GameManager.instance.PiecesToMachine.Count;
            txtPieces.text = openPieces.ToString();
        }
        */

        private Side _Side { get; set; }
    }
}

