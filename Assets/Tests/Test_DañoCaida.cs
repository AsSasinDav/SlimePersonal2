using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DañoCaida : MonoBehaviour
{
    private GameObject player;
    private Player_Move playerMove;

    [SetUp]
    public void Setup()
    {
        player = new GameObject();
        playerMove = player.AddComponent<Player_Move>();

        playerMove.maxHP = 100;
        playerMove.HP = 100;
        playerMove.dañoPorMetro = 10;
        playerMove.alturaSegura = 5;
        playerMove.umbralCaida = 0.2f;
        playerMove.tiempoActualizacion = 0.1f;
        playerMove.listaVelocidadesVerticales = new float[10];
        playerMove.isSlimed = false;
    }

    [TearDown]
    public void TearDown()
    {
        if (player != null)
        {
            DestroyImmediate(player);
        }
    }

    [Test]
    public void Test_CalculoMetrosCaidos()
    {
        playerMove.alturaInicialYFinal.x = 10f;
        playerMove.alturaInicialYFinal.y = 0f;
        playerMove.caidaTermino = true;
        playerMove.MetodoCaida();

        Assert.AreEqual(10, playerMove.metrosCaidos, "El cálculo de metros caídos debería ser 10");
    }

    [Test]
    public void Test_AplicarDañoCaida()
    {
        playerMove.alturaInicialYFinal.x = 10f;
        playerMove.alturaInicialYFinal.y = 0f;
        playerMove.caidaTermino = true;
        playerMove.metrosCaidos = 10;
        playerMove.velocidadCaidaPromedio = 0.3f;
        playerMove.MetodoCaida();

        int expectedDamage = playerMove.dañoPorMetro * (playerMove.metrosCaidos - playerMove.alturaSegura);
        Assert.AreEqual(playerMove.maxHP - expectedDamage, playerMove.HP, "El daño aplicado no coincide con el esperado");
    }

    [Test]
    public void Test_NoAplicarDañoCaida()
    {
        playerMove.alturaInicialYFinal.x = 10f;
        playerMove.alturaInicialYFinal.y = 0f;
        playerMove.caidaTermino = true;
        playerMove.metrosCaidos = 10;
        playerMove.velocidadCaidaPromedio = 0.1f;
        playerMove.MetodoCaida();

        Assert.AreEqual(playerMove.maxHP, playerMove.HP, "El personaje no debería recibir daño si la velocidad de caída está por debajo del umbral");
    }
}

