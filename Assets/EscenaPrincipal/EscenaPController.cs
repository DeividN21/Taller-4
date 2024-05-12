using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaPController : MonoBehaviour
{
    public void RegresarMenu(string Menu)
    {
        SceneManager.LoadScene(Menu);
    }
}
