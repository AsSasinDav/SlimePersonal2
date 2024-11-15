using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock_Door : MonoBehaviour
{
    private bool is_lock = true;
    public int requiredSlimes = 3; // Número de balas necesarias para abrir la puerta
    private Renderer doorRenderer; // Para controlar la transparencia de la puerta
    private Collider doorCollider; // Para manejar el paso
    private int remainingSlimes;
    private TextMesh text;

    void Start()
    {
        doorRenderer = GetComponent<Renderer>();
        doorCollider = GetComponent<Collider>();
        text = GetComponentInChildren<TextMesh>();
        remainingSlimes = requiredSlimes;
    }

    void Update(){
        int remainingAux = requiredSlimes - GlobalVariables.maxSlimes + GlobalVariables.cantSlimes;
        if (remainingSlimes != remainingAux){
            remainingSlimes = remainingAux;
            if (remainingSlimes < 0){
                text.text = -remainingSlimes + "";
            } else {
                text.text = remainingSlimes + "";
            }
            
            if (is_lock && remainingSlimes <= 0){   

                Color doorColor = doorRenderer.material.color;
                doorColor.a = 0f; // Cambiar transparencia
                doorRenderer.material.color = doorColor;

                // Desactivar el collider para dejar pasar
                doorCollider.isTrigger = true;
                is_lock = false;

            } else if (!is_lock && remainingSlimes > 0){

                Color doorColor = doorRenderer.material.color;
                doorColor.a = 1f; // Totalmente opaca
                doorRenderer.material.color = doorColor;

                doorCollider.isTrigger = false;
                is_lock = true;

            }
        }
    }
}