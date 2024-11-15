using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class FuncionamientoPistola : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    private bool slimeFuera = false; //sliem
    public GameObject municion;
    private List<GameObject> slimes;
    public bool testShoot = false;
    public bool testReturnSlime = false;
    void Update()
    {
        Shoot();
        RetornarSlime();
        UpdateMunicion();
    }
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && !slimeFuera || testShoot)
        {
            if (GlobalVariables.cantSlimes >= GlobalVariables.maxSlimes)
            {
                Debug.Log("No quedan slimes por disparar");
                return;
            }
            else
            {
                Debug.Log("Se instancio el slime");
                var bullet = PoolManager.Instance.GetBullet();
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            }
        }
    }

    public void RetornarSlime()
    {
        if (Input.GetKeyDown(KeyCode.R) || testReturnSlime)
        {
            slimes = HacerListaSLimes("SlimeS", "SlimeT", "SlimeP");
            if (slimes.Count == 0)
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
    public List<GameObject> HacerListaSLimes(params string[] tags)
    {
        List<GameObject> objects = new List<GameObject>();
        foreach (string tag in tags)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
            objects.AddRange(taggedObjects);
        }
        objects = objects.OrderBy(obj => obj.GetComponent<SlimePegado>().GetMomentoActivacion()).ToList();
        return objects;
    }

    // funcion auxiliar de UI
    public void UpdateMunicion()
    {
        municion.GetComponent<Text>().text = (GlobalVariables.maxSlimes - GlobalVariables.cantSlimes) + " / " + (GlobalVariables.maxSlimes);
    }
}
