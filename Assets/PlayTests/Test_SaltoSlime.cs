using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class Test_SaltoSlime : MonoBehaviour
{
    private GameObject playerGameObject;
    private Player_Move player;
    private CharacterController controller;
    private GameObject slimeCheckObject;

    [SetUp]
    public void Setup()
    {
        playerGameObject = new GameObject("Player_Move");
        player = playerGameObject.AddComponent<Player_Move>();
        controller = playerGameObject.AddComponent<CharacterController>();
        player.controller = controller;

        GameObject groundCheckObject = new GameObject("GroundCheck");
        player.groundCheck = groundCheckObject.transform;

        slimeCheckObject = new GameObject("SlimeCheck");
        player.slimeCheck = slimeCheckObject.transform;

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.position = Vector3.zero;
        ground.layer = LayerMask.NameToLayer("Ground");
        player.groundMask = LayerMask.GetMask("Ground");

        GameObject slime = GameObject.CreatePrimitive(PrimitiveType.Cube);
        slime.transform.position = new Vector3(0, 0.5f, 0);
        slime.layer = LayerMask.NameToLayer("Slime");
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
        if (slimeCheckObject != null)
        {
            DestroyImmediate(slimeCheckObject);
        }
        var ground = GameObject.Find("Plane");
        if (ground != null)
        {
            DestroyImmediate(ground);
        }
        var slime = GameObject.Find("Slime");
        if (slime != null)
        {
            DestroyImmediate(slime);
        }
    }

    [UnityTest]
    public IEnumerator TestSaltoSobreSlime()
    {
        Vector3 initialPosition = slimeCheckObject.transform.position;
        player.Update();
        yield return null;

        Assert.IsTrue(player.isSlimed, "El jugador debería estar en contacto con el slime");
        player.useTestInput = true;
        player.simulatedJumpInput = true;
        player.Update();
        yield return null;

        Assert.Greater(playerGameObject.transform.position.y, initialPosition.y,
            "El jugador debería poder saltar sobre el slime");
    }
}
