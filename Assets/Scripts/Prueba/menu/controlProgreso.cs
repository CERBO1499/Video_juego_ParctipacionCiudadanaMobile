using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class controlProgreso : MonoBehaviour
{
    private static controlProgreso _instance;
    [SerializeField] Slider sliderProgreso;
    [SerializeField] int cantMaxActividades;
    [SerializeField] int actividadesrealizadas;

    public int Actividadesrealizadas { get => actividadesrealizadas; set => actividadesrealizadas = value; }

    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            if (sliderProgreso == null)
            {
                sliderProgreso = GameObject.Find("SliderProgres").GetComponent<Slider>();
            }

        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        sliderProgreso.value = calculatePercentActivities();
    }
    float  calculatePercentActivities()
    {
        return Actividadesrealizadas / cantMaxActividades;
    }

    public void SumarActividad()
    {

        sliderProgreso.value = calculatePercentActivities();
    }
}
