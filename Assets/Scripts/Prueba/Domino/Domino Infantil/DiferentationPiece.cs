using UnityEngine;


namespace Diverdomino
{
    public class DiferentationPiece : MonoBehaviour
    {
        public enum PieceNumber
        {
            cero, uno, dos, tres, cuatro, cinco, seis
        }

        [SerializeField] PieceNumber pieceNumber;

        public PieceNumber PieceNumber1 { get => pieceNumber; set => pieceNumber = value; }

       
    }
}


