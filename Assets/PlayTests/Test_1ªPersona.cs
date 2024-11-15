using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test_1ªPersona : MonoBehaviour
{
    private GameObject cameraObject;
    private Camara_Move camaraMove;

    [SetUp]
    public void Setup()
    {
        cameraObject = new GameObject("CamaraMain");
        camaraMove = cameraObject.AddComponent<Camara_Move>();
        GameObject playerBodyObject = new GameObject("PlayerBody");
        camaraMove.playerBody = playerBodyObject.transform;
    }

    [TearDown]
    public void Teardown()
    {
        if (cameraObject != null)
        {
            DestroyImmediate(cameraObject);
        }
        if (camaraMove.playerBody != null)
        {
            DestroyImmediate(camaraMove.playerBody.gameObject);
        }
    }

    [Test]
    public void TestAplicacionSensibilidad()
    {
        Assert.AreEqual(300f, camaraMove.mouseSensitivity, "Por defecto la sensibilidad aplicada es 300f");
    }

    [UnityTest]
    public IEnumerator TestComprobacionLimitesVerticales()
    {
        camaraMove.rotation = -100f;
        camaraMove.Update();
        yield return null;

        Assert.AreEqual(-90f, camaraMove.rotation, "Limite de rotacion en -90f");

        camaraMove.rotation = 100f;
        camaraMove.Update();
        yield return null;

        Assert.AreEqual(90f, camaraMove.rotation, "Limite de rotacion en 90f");
    }

    [UnityTest]
    public IEnumerator TestRotacionEnEjeXeY()
    {
        Quaternion initialRotation = camaraMove.playerBody.rotation;
        camaraMove.useTestInput = true;
        camaraMove.testMouseX = 1.0f;
        camaraMove.testMouseY = 1.0f;
        camaraMove.Update();
        yield return null;

        Assert.AreNotEqual(initialRotation, camaraMove.playerBody.rotation, "El personaje del jugador rota en el eje X e Y");
    }
}

