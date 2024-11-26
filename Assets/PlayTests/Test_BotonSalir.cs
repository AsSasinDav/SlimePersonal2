using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Test_BotonSalir : MonoBehaviour
{
    private Menu menu;
    private bool quit;

    [SetUp]
    public void SetUp()
    {
        GameObject menuObject = new GameObject();
        menu = menuObject.AddComponent<Menu>();
        quit = false;
    }

    [TearDown]
    public void TearsDown()
    {
        if (menu != null)
        {
            DestroyImmediate(menu.gameObject);
        }
    }

    [UnityTest]
    public IEnumerator Test_SalirPartida()
    {
        menu.Salir();
        yield return null;
        Assert.IsTrue(menu.quit, "Salio de la aplicacion");
    }
}
