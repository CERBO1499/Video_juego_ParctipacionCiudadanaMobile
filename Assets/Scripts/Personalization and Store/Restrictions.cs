using UnityEngine;

namespace Personalization
{
    public class Restrictions : MonoBehaviour
    {
        #region Enum
        public enum Sex
        {
            H, 
            F
        }
        public enum Type
        {
            pelo,
            cara,
            accesorios,
            camisa,
            pantalon,
            zapatos
        }
        #endregion

        public Items items;

        public bool this[Sex sex, Type type, int index]
        {
            get 
            {
                switch (sex)
                {
                    case Sex.H:
                        switch (type)
                        {
                            case Type.pelo:
                                return items.H.pelo.Contains(index.ToString());
                            case Type.cara:
                                return items.H.cara.Contains(index.ToString());
                            case Type.accesorios:
                                return items.H.accesorios.Contains(index.ToString());
                            case Type.camisa:
                                return items.H.camisa.Contains(index.ToString());
                            case Type.pantalon:
                                return items.H.pantalon.Contains(index.ToString());
                            default:
                                return items.H.zapatos.Contains(index.ToString());
                        }
                    default:
                        switch (type)
                        {
                            case Type.pelo:
                                return items.M.pelo.Contains(index.ToString());
                            case Type.cara:
                                return items.M.cara.Contains(index.ToString());
                            case Type.accesorios:
                                return items.M.accesorios.Contains(index.ToString());
                            case Type.camisa:
                                return items.M.camisa.Contains(index.ToString());
                            case Type.pantalon:
                                return items.M.pantalon.Contains(index.ToString());
                            default:
                                return items.M.zapatos.Contains(index.ToString());
                        }
                }
            }
        }
    } 
}