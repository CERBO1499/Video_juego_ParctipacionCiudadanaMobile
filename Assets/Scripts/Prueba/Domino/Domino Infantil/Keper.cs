using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum NumberPiece
{
    cero,uno,dos,tres,cuatro,cinco,seis,firstPiece
}
public enum Side
{
    Izq,Dere
}

namespace Diverdomino
{
    public class Keper : MonoBehaviour
    {
        #region Information 
        [SerializeField] NumberPiece numPiece;
        [SerializeField] LayerMask fichasMask;
        [SerializeField] Side sidde;
        PieceDomino pieceDomino;
        bool isKeeped;
        #endregion

        #region EncapsulatedField
        public bool IsKeeped { get => isKeeped; set => isKeeped = value; }
        public NumberPiece NumPiece { get => numPiece; set => numPiece = value; }
        public Side Sidde { get => sidde; set => sidde = value; }
        #endregion


        void OnEnable()
        {

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position,new Vector3(0f,0f,100f), -float.MaxValue, fichasMask);
            RaycastHit2D hit2D2 = Physics2D.Raycast(transform.position, new Vector3(0f, 0f, 100f), float.MaxValue, fichasMask);

            if (hit2D.collider != null || hit2D2.collider != null)
            {
                print("Collision with ficha" + hit2D.collider.name);

                gameObject.SetActive(false);
            }
        }
    }

}
