using UnityEngine;

public class ControlLimite : MonoBehaviour
{
    public static ControlLimite instancia; // Singleton para el control del límite

    void Start()
    {
        instancia = this; // Asigna la instancia al inicio
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limite"))
        {
            EnemyController.limiteAlcanzado = true; // Marca el límite como alcanzado
        }
    }
}