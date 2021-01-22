using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum NumberPiece
{
    cero,uno,dos,tres,cuatro,cinco,seis
}

namespace Diverdomino
{
    public class Keper : MonoBehaviour
    {
        #region Information 
        [SerializeField] NumberPiece numPiece;
        [SerializeField] LayerMask fichasMask;
        bool isActive;
        bool isKeeped;
        RaycastHit hit;
        #endregion

        #region EncapsulatedField
        public bool IsKeeped { get => isKeeped; set => isKeeped = value; }
        #endregion

        private void OnEnable()
        {
            bool hitDetected = Physics2D.Raycast(this.transform.position, this.transform.forward, float.MaxValue, fichasMask);
            if (hitDetected)
            {
                gameObject.SetActive(false);
            }            
        }


    }

}
