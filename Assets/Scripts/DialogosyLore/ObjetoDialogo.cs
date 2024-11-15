using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(menuName = "Dialogue/ObjetoDialogo")]
public class ObjetoDialogo : ScriptableObject
{
    [SerializeField] [TextArea] private string[] burbujasDialogo;
    public string[] BurbujasDialogo => burbujasDialogo;
}

