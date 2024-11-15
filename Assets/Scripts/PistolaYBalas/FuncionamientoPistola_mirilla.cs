using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FuncionamientoPistola_mirilla : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public GameObject personaje;
    private GameObject[] slimes;
    void Update()
    {
        Shoot();
        RetornarSlime();
    }
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GlobalVariables.cantSlimes >= GlobalVariables.maxSlimes)
            {
                Debug.Log("No hay slimes en el pool");
                return;
            }
            else
            {
                var bullet = PoolManager.Instance.GetBullet();
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.SetActive(true);
                // Vector3 shootpos = bulletSpawnPoint.forward + GameObject.FindGameObjectsWithTag("CamaraPrincipal"). * 1f + new Vector3(0, -0.1f, 0);
                // bullet.GetComponent<Rigidbody>().velocity=shootpos;
            }
        }
    }
    public void RetornarSlime()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            slimes = GameObject.FindGameObjectsWithTag("SlimeS");
            if (slimes.Length == 0)
            {
                Debug.Log("No hay slimes en la escena");
                return;
            }
            else
            {
                slimes[0].SetActive(false);
                GlobalVariables.cantSlimes--;
                Debug.Log("Slime retornado");
            }
        }
    }
}
