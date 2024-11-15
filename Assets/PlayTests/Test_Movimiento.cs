using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test_Movimiento : MonoBehaviour
{
    private GameObject playerGameObject;
    private Player_Move player;
    private CharacterController controller;

    [SetUp]
    public void Setup()
    {
        playerGameObject = new GameObject("Player_Move");
        player = playerGameObject.AddComponent<Player_Move>();
        controller = playerGameObject.AddComponent<CharacterController>();
        player.controller = controller;


        GameObject groundCheckObject = new GameObject("GroundCheck");
        player.groundCheck = groundCheckObject.transform;

        GameObject slimeCheckObject = new GameObject("SlimeCheck");
        player.slimeCheck = slimeCheckObject.transform;

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.position = Vector3.zero;


        ground.layer = LayerMask.NameToLayer("Ground");
        player.groundMask = LayerMask.GetMask("Ground");
        player.slimeMask = LayerMask.GetMask("Slime");
    }
    [TearDown]
    public void Teardown()
    {
        if (playerGameObject != null)
        {
            DestroyImmediate(playerGameObject);
        }
        if (player.groundCheck != null)
        {
            DestroyImmediate(player.groundCheck.gameObject);
        }
        if (player.slimeCheck != null)
        {
            DestroyImmediate(player.slimeCheck.gameObject);
        }
        var ground = GameObject.Find("Plane");
        if (ground != null)
        {
            DestroyImmediate(ground);
        }
    }

    [UnityTest]
    public IEnumerator TestMovimientoHaciaDelante()
    {
        player.useTestInput = true;
        player.simulatedVerticalInput = 1f;
        Vector3 initialPosition = playerGameObject.transform.position;
        player.Update();
        yield return null;

        Assert.Greater(playerGameObject.transform.position.z, initialPosition.z, "El jugador debería moverse hacia adelante al la tecla presionar W");
    }

    [UnityTest]
    public IEnumerator TestMovimientoHaciaLaDerecha()
    {
        player.useTestInput = true;
        player.simulatedHorizontalInput = 1f;
        Vector3 initialPosition = playerGameObject.transform.position;
        player.Update();
        yield return null;

        Assert.Greater(playerGameObject.transform.position.x, initialPosition.x, "El jugador debería moverse hacia la derecha al la tecla presionar D");
    }

    [UnityTest]
    public IEnumerator TestMovimientoHaciaLaIzquierda()
    {
        player.useTestInput = true;
        player.simulatedHorizontalInput = -1f;
        Vector3 initialPosition = playerGameObject.transform.position;
        player.Update();
        yield return null;

        Assert.Less(playerGameObject.transform.position.x, initialPosition.x, "El jugador debería moverse hacia la izquierda al presionar la tecla A");
    }

    [UnityTest]
    public IEnumerator TestMovimientoHaciaAtras()
    {
        player.useTestInput = true;
        player.simulatedVerticalInput = -1f;
        Vector3 initialPosition = playerGameObject.transform.position;
        player.Update();
        yield return null;

        Assert.Less(playerGameObject.transform.position.z, initialPosition.z, "El jugador debería moverse hacia atrás al presionar la tecla S");
    }

}

