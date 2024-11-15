using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test_Pooling : MonoBehaviour
{
    private PoolManager poolManager;

    [SetUp]
    public void Setup()
    {
        GameObject poolManagerObject = new GameObject();
        poolManager = poolManagerObject.AddComponent<PoolManager>();
        poolManager.balasPrefab = new GameObject("balasPrefab");
        poolManager.slimePrefabSuelo = new GameObject("slimePrefabSuelo");
        poolManager.slimePrefabTecho = new GameObject("slimePrefabTecho");
        poolManager.slimePrefabPared = new GameObject("slimePrefabPared");
    }

    [TearDown]
    public void Teardown()
    {
        if (poolManager != null)
        {
            DestroyImmediate(poolManager.gameObject);
        }
    }

    [Test]
    public void TestComprobarSizeBulletPool()
    {
        poolManager.Awake();
        Assert.AreEqual(20, poolManager.bulletPool.Count);
    }

    [Test]
    public void TestComprobarSizeSlimePool()
    {
        poolManager.Awake();
        Assert.AreEqual(GlobalVariables.maxSlimes, poolManager.slimePoolSuelo.Count);
    }

    [Test]
    public void TestDevuelveBulletInactiva()
    {
        poolManager.Awake();
        var bullet = poolManager.GetBullet();
        Assert.IsNotNull(bullet);
        Assert.IsFalse(bullet.activeInHierarchy);
    }

    [Test]
    public void TestDevuelveSlimeSInactivo()
    {
        poolManager.Awake();
        var slime = poolManager.GetSlimeSuelo();
        Assert.IsNotNull(slime);
        Assert.IsFalse(slime.activeInHierarchy);
    }

    [Test]
    public void TestBulletPoolFullActiva()
    {
        poolManager.Awake();
        foreach (var bullet in poolManager.bulletPool)
        {
            bullet.SetActive(true);
        }

        Assert.IsNull(poolManager.GetBullet());
    }

    [Test]
    public void TestSlimeSPoolFullActiva()
    {
        poolManager.Awake();
        foreach (var slime in poolManager.slimePoolSuelo)
        {
            slime.SetActive(true);
        }

        Assert.IsNull(poolManager.GetSlimeSuelo());
    }

    [Test]
    public void TestSizeSNegativo()
    {
        poolManager.BulletPoolSize = -3;
        poolManager.SlimePoolSize = 10;
        poolManager.Awake();
        Assert.AreEqual(10, poolManager.slimePoolSuelo.Count);
        Assert.IsEmpty(poolManager.bulletPool);
    }
}
