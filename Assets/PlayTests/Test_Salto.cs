using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Test_Salto : MonoBehaviour
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
       
        player.listaVelocidadesVerticales = new float[10]; 
        player.alturaInicialYFinal = new Vector2(0, 0);    
        player.HP = player.maxHP = 100;                    
        player.alturaSegura = 1;                           
        player.umbralCaida = 0.2f;
        player.tiempoActualizacion = 0.1f;
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
    public IEnumerator TestDeSalto()
    {
        player.useTestInput = true;
        player.simulatedJumpInput = true;
        Vector3 initialPosition = playerGameObject.transform.position;
        player.Update();
        yield return null;

        Assert.Greater(playerGameObject.transform.position.y, initialPosition.y,
         "El jugador debería haberse elevado después de saltar");

    }

    [UnityTest]
    public IEnumerator TestDeCaidaTrasSalto()
    {
        player.useTestInput = true;
        player.simulatedJumpInput = true;
        player.Update();
        yield return null;
        player.simulatedJumpInput = false;
        Vector3 initialPosition = playerGameObject.transform.position;

        for (int i = 0; i < 100; i++)
        {
            player.Update();
            yield return null;
        }

        Assert.Less(playerGameObject.transform.position.y, initialPosition.y + player.jH,
            "El jugador debería haber vuelto a caer debido a la gravedad");
    }

    [UnityTest]
    public IEnumerator TestBugDobleSalto()
    {
        player.useTestInput = true;
        player.simulatedJumpInput = true;
        Vector3 initialPosition = playerGameObject.transform.position;
        player.Update();
        yield return null;
        player.simulatedJumpInput = false;
        float jumpPeakY = playerGameObject.transform.position.y;
        while (player.velocity.y > 0)
        {
            player.Update();
            yield return null;
            jumpPeakY = Mathf.Max(jumpPeakY, playerGameObject.transform.position.y);
        }

        Assert.Greater(jumpPeakY, initialPosition.y, "El jugador debería haberse elevado después de saltar");

        for (int i = 0; i < 10; i++)
        {
            player.simulatedJumpInput = true;
            player.Update();
            player.simulatedJumpInput = false;
            yield return null;

            Assert.LessOrEqual(playerGameObject.transform.position.y, jumpPeakY,
                "El jugador no debería poder saltar de nuevo hasta que no toque el suelo");
        }

        for (int i = 0; i < 100; i++)
        {
            player.Update();
            yield return null;
        }

        Assert.LessOrEqual(playerGameObject.transform.position.y, initialPosition.y,
            "El jugador debería haber vuelto a tocar el suelo");

        player.simulatedJumpInput = true;
        player.Update();
        yield return null;

        Assert.Greater(playerGameObject.transform.position.y, initialPosition.y,
            "El jugador debería poder saltar nuevamente después de haber tocado el suelo");
    }
}
