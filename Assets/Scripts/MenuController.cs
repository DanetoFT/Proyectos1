using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string nombreEscena;

    public void Play()
    {
        Debug.Log("cambio de escena a " + nombreEscena);

        if (nombreEscena != null)
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }

    public void Quit()
    {
        Debug.Log("El juego se cierra");

        Application.Quit();
    }
}
