using Unity.VisualScripting;
using UnityEngine;

/*
Color Picker용 스크립트 
*/
public class ApplyColorPicker : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker fcp;
    [SerializeField] private CreateLine createLine;

    void Update()
    {
        if (createLine.LineList.Count > 0)
        {
            var currentLine = createLine.LineList[createLine.LineList.Count - 1];
            LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
            lineRenderer.material.color = fcp.color; // 현재 그리는 선의 색상 변경
        }

    }

}
