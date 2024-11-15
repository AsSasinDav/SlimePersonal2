using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionamientoBalas : MonoBehaviour
{
    public float vida;
    private float nacimiento;

    void OnEnable()
    {
        nacimiento = Time.time; // guardo el tiempo de nacimiento de la bala
    }
    void Update()
    {
        if (Time.time > nacimiento + vida)
        { // si el tiempo actual es mayor al tiempo de nacimiento más el tiempo de vida
            gameObject.SetActive(false); // desactivo la bala
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SueloNormal") || collision.gameObject.CompareTag("TechoNormal") || collision.gameObject.CompareTag("ParedNormal"))
        {
            gameObject.SetActive(false);
            Debug.Log("Bala destruida");

            var slime = GetSlime(collision.gameObject.tag);
            if (slime == null)
            {
                Debug.Log("Max Slimes alcanzado");
                return;
            }
            ContactPoint contact = collision.GetContact(0);
            slime.transform.position = contact.point;
            slime.transform.rotation = Quaternion.LookRotation(contact.normal);
            slime.SetActive(true);
            GlobalVariables.cantSlimes++;
        }
        else if (collision.gameObject.CompareTag("SlimeS") || collision.gameObject.CompareTag("SlimeT") || collision.gameObject.CompareTag("SlimeP"))
        {
            gameObject.SetActive(false);
            Debug.Log("Bala destruida");
        }
    }
    GameObject GetSlime(String tag)
    {
        if (tag == "SueloNormal")
        {
            return PoolManager.Instance.GetSlimeSuelo();
        }
        else if (tag == "TechoNormal")
        {
            return PoolManager.Instance.GetSlimeTecho();
        }
        else if (tag == "ParedNormal")
        {
            return PoolManager.Instance.GetSlimePared();
        }
        return null;
    }

}
