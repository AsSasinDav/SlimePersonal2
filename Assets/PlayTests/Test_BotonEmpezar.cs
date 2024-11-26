using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Test_BotonEmpezar : MonoBehaviour
{
    private Menu menu;

    [SetUp]
    public void SetUp()
    {
        GameObject menuObject = new GameObject();
        menu = menuObject.AddComponent<Menu>();
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
    public IEnumerator Test_EmpezarPartida()
    {
        SceneManager.LoadScene("MenuPrincipal");
        yield return null;
        menu.Jugar();
        yield return null;
        Assert.AreEqual("Tuturial_Alvaro", SceneManager.GetActiveScene().name);
    }
}
