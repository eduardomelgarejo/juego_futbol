using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pegarPelota : MonoBehaviour
{
    public Rigidbody rb;
    public float maxHoldTime = 3f;
    public float forceMultiplier = 10f;
    public Transform puntoInicial;

    private float holdTime = 0f;
    private bool isCharging = false;
    private bool puedePatear = true;
    private int goles = 0;
    private const int golesParaGanar = 3;
    private bool esperandoReinicio = false;

    void Start()
    {
        ReiniciarPelota();
    }

    void Update()
    {
        if (puedePatear && !esperandoReinicio)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isCharging = true;
                holdTime = 0f;
            }

            if (Input.GetKey(KeyCode.Space) && isCharging)
            {
                holdTime += Time.deltaTime;
                holdTime = Mathf.Clamp(holdTime, 0f, maxHoldTime);
            }

            if (Input.GetKeyUp(KeyCode.Space) && isCharging)
            {
                isCharging = false;
                float finalForce = holdTime * forceMultiplier;

                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(transform.forward * finalForce, ForceMode.Impulse);

                puedePatear = false;
                esperandoReinicio = true;
                StartCoroutine(EsperarSiFalla(3f));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arco") && esperandoReinicio)
        {
            goles++;

            if (goles >= golesParaGanar)
            {
                // panel victoria
            }
            else
            {
               // panel gool
            }

            esperandoReinicio = false;
            StartCoroutine(ReiniciarPelotaDespuesDe(2f));
        }
    }


    IEnumerator EsperarSiFalla(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        
        if (esperandoReinicio)
        {
            esperandoReinicio = false;
            ReiniciarPelota();
        }
    }

    IEnumerator ReiniciarPelotaDespuesDe(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        ReiniciarPelota();
    }

    void ReiniciarPelota()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = puntoInicial.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
        puedePatear = true;
        esperandoReinicio = false;
    }
}

