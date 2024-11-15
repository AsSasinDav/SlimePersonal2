using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDAMenu : MonoBehaviour
{
    public GameObject ObjetoPDA;
    public bool pdaActive = false;
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        //cuando presionas P
        if(Input.GetKeyDown(KeyCode.P))
        {
            //si no estabas pausado, pausa
            if(!pdaActive)
            {
                ObjetoPDA.SetActive(true);
                pdaActive = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            //si estabas ya pausado, resume
            else if(pdaActive)
            {
                Resumir();
            }
        }
    }
    //funcion auxiliar, publica para que la pueda utilizar el boton tambien
    public void Resumir()
    {
        ObjetoPDA.SetActive(false);
        pdaActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
