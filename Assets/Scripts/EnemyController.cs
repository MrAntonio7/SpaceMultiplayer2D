using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class EnemyController : NetworkBehaviour
{


    public static EnemyController LocalInstance { get; private set; }

    // Declaración de variables
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    public float velocidad = 1.0f;
    public static bool limiteAlcanzado = false;
    public Transform limiteDerecho;
    public Transform limiteIzquierdo;

    private bool moverDerecha = true;
    public float cadenciaDeDisparo = 0.25f; // Tiempo en segundos entre disparos
    private float tiempoUltimoDisparo;
    [SerializeField] private GameObject bulletSpawner;
    [SerializeField] private GameObject bulletPrefab;
    public bool dispara = false;
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
            rb = LocalInstance.GetComponent<Rigidbody2D>();
        }


    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (IsServer)
    //    {
    //        Destroy(gameObject);
    //    }

    //}

    void OnCollisionEnter(Collision collision)
    {
        // Si colisiona con un límite, cambia la dirección de movimiento
        if (collision.gameObject.CompareTag("Limite"))
        {
            moverDerecha = !moverDerecha;
        }
    }

    private void FixedUpdate()
    {
        if (IsOwner)
        {
            if (dispara)
            {
                DisparoContinuo();
            }
            
            if (limiteAlcanzado)
            {
                moverDerecha = !moverDerecha;
                limiteAlcanzado = false;
            }

            if (moverDerecha)
            {
                transform.Translate(Vector3.right * velocidad * Time.deltaTime);

                if (transform.position.x >= limiteDerecho.position.x)
                {
                    moverDerecha = false;
                }
            }
            else
            {
                transform.Translate(Vector3.left * velocidad * Time.deltaTime);

                if (transform.position.x <= limiteIzquierdo.position.x)
                {
                    moverDerecha = true;
                }
            }

        }
    }
    void DisparoContinuo()
    {
        
            if (Time.time > tiempoUltimoDisparo + cadenciaDeDisparo)
            {
                CheckFire();
                tiempoUltimoDisparo = Time.time;
            }
        
    }
    // Método para disparar
    public void CheckFire()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        FireServerRpc(transform.rotation, bulletSpawner.transform.position);
    }

    // Método que instancia en el servidor la bala. 
    [ServerRpc(RequireOwnership = false)]
    private void FireServerRpc(Quaternion rotation, Vector3 position, ServerRpcParams serverRpcParams = default)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);

        NetworkObject bulletNetwork = bullet.GetComponent<NetworkObject>();
        bulletNetwork.SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);

        Destroy(bullet, 2f);
    }

}