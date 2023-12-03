using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private int counter = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur a pénétré dans la zone
            // Démarre l'incrémentation du compteur chaque seconde
            InvokeRepeating("IncrementCounter", 0f, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur a quitté la zone
            // Arrête l'incrémentation du compteur
            CancelInvoke("IncrementCounter");
        }
    }

    private void IncrementCounter()
    {
        // Incrémente le compteur chaque seconde
        counter++;

        // Faites quelque chose avec le compteur ici (par exemple, imprimez-le dans la console)
        Debug.Log("Counter: " + counter);
    }
}

