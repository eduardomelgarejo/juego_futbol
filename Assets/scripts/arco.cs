using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arco : MonoBehaviour
{
    public float velocidad = 2f;
    public float rangoMovimiento = 3f;

    private Vector3 posicionInicial;
    private bool moverArco;

    void Start()
    {
        posicionInicial = transform.position;

        // Lee si el toggle fue activado desde PlayerPrefs
        moverArco = PlayerPrefs.GetInt("val_moverArco", 0) == 1;
    }

    void Update()
    {
        if (moverArco)
        {
            float desplazamiento = Mathf.Sin(Time.time * velocidad) * rangoMovimiento;
            transform.position = new Vector3(posicionInicial.x + desplazamiento, posicionInicial.y, posicionInicial.z);
        }
    }
}
