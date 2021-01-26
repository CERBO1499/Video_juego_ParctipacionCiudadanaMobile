using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diverdomino
{
    public class DominoEnemy : MonoBehaviour
    {
        public PieceDomino[] pieces;

        private void Awake()
        {
            pieces = new PieceDomino[2];

            PieceDomino.OnPieceInPlace += SetPiece;
        }

        public void SetPiece(PieceDomino pieceNum, Side side)
        {
            pieces[(side == Side.Izq ? 0 : 1)] = pieceNum;
            Debug.Log("Piece Num " + pieceNum + "side " + side);
        }
    }
}
