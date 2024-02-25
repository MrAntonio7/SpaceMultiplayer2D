using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletEnemyController : NetworkBehaviour
{

    
    public static BulletEnemyController LocalInstance { get; private set; }

    // Declaración de variables
    private Rigidbody2D rb;
    [SerializeField] private float speed;

   

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
            rb = LocalInstance.GetComponent<Rigidbody2D>(); 
        }

       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsServer)
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("MenuScene");
            //Debug.Log(collision);
        }

    }

    private void FixedUpdate()
    {
        if (IsOwner)
        {
            rb.MovePosition(transform.position + (transform.up*-1) * speed * Time.fixedDeltaTime);
            //Debug.Log(rb.position);
        }
       
    }

}
