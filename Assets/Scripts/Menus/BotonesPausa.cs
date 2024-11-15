using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesPausa : MonoBehaviour
{
    public GameObject panelPausa; // panel de pausa
    public void Despasuar(){ //este método te devuelve a donde estabas antes de pausar
        Time.timeScale = 1f; // reanudo el tiempo
        panelPausa.SetActive(false); // desactivo el panel de pausa
        Pausa.pausado = false; // cambio la variable de pausa a falso
    }
    public void Salir(){
        Application.Quit(); // cierro la aplicación
    }
    public void Reiniciar(){ // este método reinicia la escena
        Time.timeScale = 1f; // reanudo el tiempo
        panelPausa.SetActive(false); // desactivo el panel de pausa
        Pausa.pausado = false; // cambio la variable de pausa a falso
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // recargo la escena actual
    }
}