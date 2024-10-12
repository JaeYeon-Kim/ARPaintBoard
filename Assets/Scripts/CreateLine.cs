using System.Collections.Generic;
using UnityEngine;



/*
AR 공간에 선을 그리는 스크립트 
*/
public class CreateLine : MonoBehaviour
{
    private Camera cam;
    public GameObject linePrefab;
    private LineRenderer line;

    private Vector3 mousePos;
    private Vector3 newPos;
    public List<Vector3> linePositions = new List<Vector3>();

    private List<GameObject> lineList = new List<GameObject>();        // 그려지는 라인들을 담을 수 있는 리스트

    public bool isDrawingEnabled = true;       // 그리기 활성화 여부 -> Color Picker가 켜진 상태에서는 그림을 못그리도록 설정하는 변수


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;    // mainCamera 할당 
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawingEnabled)
        {
            // 마우스(터치)를 누르기 시작
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
                mousePos.z = cam.nearClipPlane + 1f;
                newPos = cam.ScreenToWorldPoint(mousePos);
                linePositions.Add(newPos);

                GameObject obj = Instantiate(linePrefab);
                lineList.Add(obj);
                
                line = obj.GetComponent<LineRenderer>();

                line.positionCount = 1;
                line.SetPosition(0, linePositions[0]);  // 선을 그으려면 점이 두개 필요, 첫번째 점 할당 첫번째는 인덱스로는 0 
            }
            else if (Input.GetMouseButton(0))      // 마우스(터치)를 누르는 중 
            {
                mousePos = Input.mousePosition;
                mousePos.z = cam.nearClipPlane + 1f;
                newPos = cam.ScreenToWorldPoint(mousePos);
                linePositions.Add(newPos);

                line.positionCount++;
                line.SetPosition(line.positionCount - 1, linePositions[line.positionCount - 1]);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                linePositions.Clear();
            }
        }

    }


    // 그린것을 지우는 기능 
    public void ClearLines()
    {
        foreach (GameObject line in lineList)
        {
            Destroy(line);
        }
        lineList.Clear();
    }

}
