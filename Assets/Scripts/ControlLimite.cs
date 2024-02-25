using UnityEngine;

public class ControlLimite : MonoBehaviour
{
    public static ControlLimite instancia; // Singleton para el control del l�mite

    void Start()
    {
        instancia = this; // Asigna la instancia al inicio
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limite"))
        {
            EnemyController.limiteAlcanzado = true; // Marca el l�mite como alcanzado
        }
    }
}