using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void EmpezarJuego(string NombreJuego)
    {
        // Cargar la escena del juego
        SceneManager.LoadScene(NombreJuego);

    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Fin del Juego");
    }
    
}
