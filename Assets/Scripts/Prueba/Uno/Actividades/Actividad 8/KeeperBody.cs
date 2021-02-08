using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uno
{
    public class KeeperBody : MonoBehaviour
    {
        #region Information
        public bool isKeeped;
        #endregion

        #region EncapsulatedFields
        public bool IsKeeped { get => isKeeped; set => isKeeped = value; }
        #endregion
    }
}