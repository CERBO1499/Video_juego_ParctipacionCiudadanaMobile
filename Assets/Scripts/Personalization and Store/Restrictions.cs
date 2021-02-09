using UnityEngine;

namespace Personalization
{
    public class Restrictions : MonoBehaviour
    {
        #region Enum
        public enum Sex
        {
            male, 
            female
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
                    case Sex.male:
                        switch (type)
                        {
                            case Type.pelo:
                                return items.male.pelo.Contains(index.ToString());
                            case Type.cara:
                                return items.male.cara.Contains(index.ToString());
                            case Type.accesorios:
                                return items.male.accesorios.Contains(index.ToString());
                            case Type.camisa:
                                return items.male.camisa.Contains(index.ToString());
                            case Type.pantalon:
                                return items.male.pantalon.Contains(index.ToString());
                            default:
                                return items.male.zapatos.Contains(index.ToString());
                        }
                    default:
                        switch (type)
                        {
                            case Type.pelo:
                                return items.female.pelo.Contains(index.ToString());
                            case Type.cara:
                                return items.female.cara.Contains(index.ToString());
                            case Type.accesorios:
                                return items.female.accesorios.Contains(index.ToString());
                            case Type.camisa:
                                return items.female.camisa.Contains(index.ToString());
                            case Type.pantalon:
                                return items.female.pantalon.Contains(index.ToString());
                            default:
                                return items.female.zapatos.Contains(index.ToString());
                        }
                }
            }
        }
    } 
}