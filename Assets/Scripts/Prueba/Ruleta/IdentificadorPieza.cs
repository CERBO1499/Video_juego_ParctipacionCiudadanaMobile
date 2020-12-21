using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentificadorPieza : MonoBehaviour
{
    #region Components
    [SerializeField]
    ETipoDeImagen tipoDeImagen;
    #endregion 

    bool selected = false;

    public bool Selected { get => selected; set => selected = value; }
    public ETipoDeImagen TipoDeImagen { get => tipoDeImagen; set => tipoDeImagen = value; }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      