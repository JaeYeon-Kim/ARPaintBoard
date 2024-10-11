using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Color Picker용 스크립트 
*/
public class ApplyColorPicker : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker fcp;
    [SerializeField] private Material material;
    
    void Update()
    {
        material.color = fcp.color;
    }
}
