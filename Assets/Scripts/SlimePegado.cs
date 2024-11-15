using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePegado : MonoBehaviour
{
    private float momentoActivacion = 0f;
    void OnEnable(){
        momentoActivacion = Time.time;
    }
    public float GetMomentoActivacion(){
        return momentoActivacion;
    } 
}
