using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Método llamado cuando otro Collider entra en el gatillo (el área de colisión)
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró en el gatillo es el jugador
        if (other.CompareTag("Player"))
        {
            // Desactiva la moneda (la hace desaparecer)
            gameObject.SetActive(false);
        }
    }
}
