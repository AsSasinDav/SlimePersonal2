using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class Test_DispararYRecoger : MonoBehaviour
{
    private FuncionamientoPistola pistola;
    private GameObject bulletPrefab;
    private Transform bulletSpawnPoint;
    private GameObject municion;
    private PoolManager poolManager;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        pistola = gameObject.AddComponent<FuncionamientoPistola>();

        bulletPrefab = new GameObject();
        bulletPrefab.AddComponent<Rigidbody>();
        pistola.bulletPrefab = bulletPrefab;
        bulletSpawnPoint = new GameObject().transform;
        pistola.bulletSpawnPoint = bulletSpawnPoint;

        GameObject poolManagerObject = new GameObject();
        poolManager = poolManagerObject.AddComponent<PoolManager>();
        poolManager.balasPrefab = bulletPrefab;
        poolManager.slimePrefabSuelo = new GameObject("slimePrefabSuelo");
        poolManager.slimePrefabTecho = new GameObject("SlimePrefabTecho");
        poolManager.slimePrefabPared = new GameObject("SlimePrefabPared");
        poolManager.Awake();

        municion = new GameObject();
        municion.AddComponent<Text>();
        pistola.municion = municion;
    }

    [TearDown]
    public void TearDown()
    {
        if (pistola.gameObject != null)
        {
            DestroyImmediate(pistola.gameObject);
        }
        if (bulletPrefab != null)
        {
            DestroyImmediate(bulletPrefab);
        }
        if (bulletSpawnPoint.gameObject != null)
        {
            DestroyImmediate(bulletSpawnPoint.gameObject);
        }
        if (municion != null)
        {
            DestroyImmediate(municion);
        }
        if (poolManager != null)
        {
            DestroyImmediate(poolManager.gameObject);
        }
        foreach (var slime in GameObject.FindGameObjectsWithTag("SlimeS"))
        {
            DestroyImmediate(slime);
        }
        foreach (var slime in GameObject.FindGameObjectsWithTag("SlimeT"))
        {
            DestroyImmediate(slime);
        }
        foreach (var slime in GameObject.FindGameObjectsWithTag("SlimeP"))
        {
            DestroyImmediate(slime);
        }
    }

    [Test]
    public void TestNoHaySlimesEnPool()
    {
        GlobalVariables.cantSlimes = 5;
        GlobalVariables.maxSlimes = 5;
        pistola.testShoot = true;

        LogAssert.Expect(LogType.Log, "No quedan slimes por disparar");
        pistola.Shoot();
    }

    [Test]
    public void TestInstancioSlime()
    {
        GlobalVariables.cantSlimes = 0;
        GlobalVariables.maxSlimes = 5;
        pistola.testShoot = true;

        LogAssert.Expect(LogType.Log, "Se instancio el slime");
        pistola.Shoot();
    }

    [Test]
    public void TestRetornoDelSlimeS()
    {
        GlobalVariables.cantSlimes = 1;
        var slime = new GameObject();
        slime.tag = "SlimeS";
        slime.AddComponent<SlimePegado>();
        pistola.testReturnSlime = true;
        pistola.RetornarSlime();

        Assert.IsFalse(slime.activeSelf, "Se desactivo el slime");
        Assert.AreEqual(0, GlobalVariables.cantSlimes, "Se incremento correctamente el contador de slimes");
    }

    [Test]
    public void TestRetornoDelSlimeT()
    {
        GlobalVariables.cantSlimes = 1;
        var slime = new GameObject();
        slime.tag = "SlimeT";
        slime.AddComponent<SlimePegado>();
        pistola.testReturnSlime = true;
        pistola.RetornarSlime();

        Assert.IsFalse(slime.activeSelf, "Se desactivo el slime");
        Assert.AreEqual(0, GlobalVariables.cantSlimes, "Se incremento correctamente el contador de slimes");
    }

    [Test]
    public void TestRetornoDelSlimeP()
    {
        GlobalVariables.cantSlimes = 1;
        var slime = new GameObject();
        slime.tag = "SlimeP";
        slime.AddComponent<SlimePegado>();
        pistola.testReturnSlime = true;
        pistola.RetornarSlime();

        Assert.IsFalse(slime.activeSelf, "Se desactivo el slime");
        Assert.AreEqual(0, GlobalVariables.cantSlimes, "Se incremento correctamente el contador de slimes");
    }

    [Test]
    public void TestNoHaySlimeQueRetornar()
    {
        GlobalVariables.cantSlimes = 0;
        pistola.testReturnSlime = true;

        LogAssert.Expect(LogType.Log, "No hay slimes en la escena");
        pistola.RetornarSlime();
    }
}

