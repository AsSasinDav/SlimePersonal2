using NUnit.Framework;
using UnityEngine;
using System.Reflection;

public class PlayerSlimeTests : MonoBehaviour
{
    private GameObject player;
    private RealentizacionSlime playerSlime;
    private GameObject slime;

    [SetUp]
    public void Setup()
    {
        player = new GameObject();
        playerSlime = player.AddComponent<RealentizacionSlime>();
        slime = new GameObject();
        slime.AddComponent<BoxCollider>();

        GlobalVariables.defaultPlayerSpeed = 5f;
        GlobalVariables.defaultJumpHeight = 2f;
        GlobalVariables.playerSpeed = GlobalVariables.defaultPlayerSpeed;
        GlobalVariables.jumpHeight = GlobalVariables.defaultJumpHeight;
        GlobalVariables.slime_collision = false;
    }

    [TearDown]
    public void Teardown()
    {
        DestroyImmediate(player);
        DestroyImmediate(slime);
    }

    [Test]
    public void TestActivarReduccionSaltoYVelocidadEntrarEnSlimeS()
    {
        slime.tag = "SlimeS";
        ControllerColliderHit hit = CreateControllerColliderHit(
            player.GetComponent<CharacterController>(),
            slime.GetComponent<Collider>()
        );
        playerSlime.OnControllerColliderHit(hit);

        Assert.AreEqual(1f, GlobalVariables.playerSpeed, "Se redujo la velocidad del jugador");
        Assert.AreEqual(0.3f, GlobalVariables.jumpHeight, "Se redujo la potencia de salto del jugador");
        Assert.IsTrue(playerSlime.collisionSlime, "Se activo la collisionSlimeS");
    }

    [Test]
    public void TestActivarReduccionSaltoYVelocidadEntrarEnSlimeT()
    {
        slime.tag = "SlimeT";
        ControllerColliderHit hit = CreateControllerColliderHit(
            player.GetComponent<CharacterController>(),
            slime.GetComponent<Collider>()
        );
        playerSlime.OnControllerColliderHit(hit);

        Assert.AreEqual(1f, GlobalVariables.playerSpeed, "Se redujo la velocidad del jugador");
        Assert.AreEqual(0.3f, GlobalVariables.jumpHeight, "Se redujo la potencia de salto del jugador");
        Assert.IsTrue(playerSlime.collisionSlime, "Se activo la collisionSlimeS");
    }

    [Test]
    public void TestActivarReduccionSaltoYVelocidadEntrarEnSlimeP()
    {
        slime.tag = "SlimeP";
        ControllerColliderHit hit = CreateControllerColliderHit(
            player.GetComponent<CharacterController>(),
            slime.GetComponent<Collider>()
        );
        playerSlime.OnControllerColliderHit(hit);

        Assert.AreEqual(1f, GlobalVariables.playerSpeed, "Se redujo la velocidad del jugador");
        Assert.AreEqual(0.3f, GlobalVariables.jumpHeight, "Se redujo la potencia de salto del jugador");
        Assert.IsTrue(playerSlime.collisionSlime, "Se activo la collisionSlimeS");
    }

    [Test]
    public void TestDesactivarReduccionSaltoTVelocidadFueraSlime()
    {
        GlobalVariables.slime_collision = true;
        GlobalVariables.playerSpeed = 1f;
        GlobalVariables.jumpHeight = 0.3f;
        playerSlime.collisionSlime = false;
        playerSlime.Update();

        Assert.AreEqual(GlobalVariables.defaultPlayerSpeed, GlobalVariables.playerSpeed, "Se restauro la velocidad del personaje");
        Assert.AreEqual(GlobalVariables.defaultJumpHeight, GlobalVariables.jumpHeight, "Se restauro la potencia de salto del jugador");
        Assert.IsFalse(GlobalVariables.slime_collision, "Se desactivo la collisionSlimeS");
    }

    [Test]
    public void TestMantenerActivoReduccionSaltoYVelocidadEnSlime()
    {
        GlobalVariables.slime_collision = true;
        GlobalVariables.playerSpeed = 1f;
        GlobalVariables.jumpHeight = 0.3f;
        playerSlime.collisionSlime = true;
        playerSlime.Update();

        Assert.AreEqual(1f, GlobalVariables.playerSpeed, "Se mantiene la velocidad reducida del personaje");
        Assert.AreEqual(0.3f, GlobalVariables.jumpHeight, "Se mantiene la potencia de salto reducida del personaje");
        Assert.IsTrue(GlobalVariables.slime_collision, "Se mantiene activada la collisionSlimeS");
    }

    private ControllerColliderHit CreateControllerColliderHit(CharacterController controller, Collider collider)
    {
        var hitInstance = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(ControllerColliderHit)) as ControllerColliderHit;

        typeof(ControllerColliderHit).GetField("m_Controller", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(hitInstance, controller);
        typeof(ControllerColliderHit).GetField("m_Collider", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(hitInstance, collider);
        typeof(ControllerColliderHit).GetField("m_Point", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(hitInstance, Vector3.zero);
        typeof(ControllerColliderHit).GetField("m_Normal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(hitInstance, Vector3.up);
        typeof(ControllerColliderHit).GetField("m_MoveDirection", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(hitInstance, Vector3.forward);
        typeof(ControllerColliderHit).GetField("m_MoveLength", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(hitInstance, 0f);
        typeof(ControllerColliderHit).GetField("m_Push", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(hitInstance, 0);

        return hitInstance;
    }

}


