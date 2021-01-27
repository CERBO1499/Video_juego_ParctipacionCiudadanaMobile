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
        int pieces;
        #endregion

        private void Start()
        {
            StartCoroutine(ActualicePiecesUI());
        }
        IEnumerator ActualicePiecesUI()
        {
            yield return new WaitForSeconds(1f);
            pieces = GameManager.instance.PiecesToMachine.Count;
            txtPieces.text = pieces.ToString();
        }

        void PickPieceMachine()
        {

        }

    }
}

