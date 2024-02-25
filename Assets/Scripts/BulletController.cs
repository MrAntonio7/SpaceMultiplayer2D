using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletController : NetworkBehaviour
{

    
    public static BulletController LocalInstance { get; private set; }

    // Declaración de variables
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private GameObject winCondition;

   

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
            rb = LocalInstance.GetComponent<Rigidbody2D>();
            winCondition = GameObject.FindGameObjectWithTag("ListEnemys");
        }

       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsServer)
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            winCondition.GetComponent<WinCondition>().RestarEnemigo();
            //Debug.Log(collision);
        }


    }

    private void FixedUpdate()
    {
        if (IsOwner)
        {
            rb.MovePosition(transform.position + transform.up * speed * Time.fixedDeltaTime);
            //Debug.Log(rb.position);
        }
       
    }

}
