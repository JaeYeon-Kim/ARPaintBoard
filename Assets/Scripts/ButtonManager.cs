using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject colorPickerPanel;

    private bool isOpen = false;    // colorPicker를 열고 닫을때 현재 상태 체크를 위한 변수 


    // 컬러 Picker버튼  
    public void OpenColorPicker()
    {
        Debug.Log("ColorPicker 버튼 클릭!");
        if (!isOpen)
        {
            isOpen = true;
            colorPickerPanel.SetActive(true);
        }
        else
        {
            isOpen = false;
            colorPickerPanel.SetActive(false);
        }
    }


    // 공유 버튼 
    public void ShareContent()
    {
        Debug.Log("공유하기!!");
    }



    // 캡처 버튼 
    public void CaptureContent()
    {
        Debug.Log("화면 캡처!");

        // 저장할 이름과 저장하는 시간 
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "ScreenShotTest-" + timestamp + ".png";

#if UNITY_IPHONE || UNITY_ANDROID
        CaptureScreenForMobile(fileName);
#else
        CaptureScreenForPC(fileName);
#endif
    }

    // PC 저장용 
    private void CaptureScreenForPC(string fileName)
    {
        ScreenCapture.CaptureScreenshot("~/Downloads/" + fileName);
    }

    // 모바일 용 
    private void CaptureScreenForMobile(string fileName)
    {
        // 화면 캡처 후에 Texture 2D 형식의 텍스처 생성 
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

        // 저장될 앨범의 이름 
        string albumName = "ARPhoto";


        // 앨범에 저장 : 저장 성공 여부와 파일 경로를 콜백 함수로 반환
        NativeGallery.SaveImageToGallery(texture, albumName, fileName, (success, path) =>
        {
            Debug.Log(success);
            Debug.Log(path);
        });

        // 더 이상 필요하지 않은 텍스처를 메모리에서 삭제하여 메모리 누수 방지 
        Object.Destroy(texture);
    }
}