using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uno
{
    public class ActivitySuperhero : Activity
    {
        #region Components
        private KeeperBody[] options;
        #endregion

        private void Awake()
        {
            options = GetComponentsInChildren<KeeperBody>();
        }

        public override bool VerifyWinCondition()
        {
            int keepedCount = 0;

            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].IsKeeped == true) keepedCount++;
            }

            return keepedCount == options.Length;
        }
    }
}