using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject colorPickerPanel;

    [SerializeField] private GameObject buttonPanel;

    [SerializeField] private CreateLine createLine;

    private bool isOpen = false;    // colorPicker를 열고 닫을때 현재 상태 체크를 위한 변수 


    // 컬러 Picker버튼  
    public void OpenColorPicker()
    {
        Debug.Log("ColorPicker 버튼 클릭!");
        if (!isOpen)
        {
            isOpen = true;
            createLine.isDrawingEnabled = false;
            colorPickerPanel.SetActive(true);
        }
        else
        {
            isOpen = false;
            createLine.isDrawingEnabled = true;
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
        StartCoroutine(MakeScreenShot());
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
            ShowToast("화면 캡처 완료!");
        });

        // 더 이상 필요하지 않은 텍스처를 메모리에서 삭제하여 메모리 누수 방지 
        Object.Destroy(texture);
    }

    // 캡처가 완료되었을때 보여줄 토스트 메시지
    private void ShowToast(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        curActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject toast = new AndroidJavaObject("android.widget.Toast", curActivity);
            toast.CallStatic<AndroidJavaObject>("makeText", curActivity, message, 0).Call("show");
        }));
    }

    private IEnumerator MakeScreenShot()
    {
        // 버튼 패널 비활성화 
        buttonPanel.SetActive(false);

        // 프레임이 끝날때 까지 대기 
        yield return new WaitForEndOfFrame();

        // 저장할 이름과 저장하는 시간 
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "ScreenShotTest-" + timestamp + ".png";

#if UNITY_IPHONE || UNITY_ANDROID
        CaptureScreenForMobile(fileName);
#else
        CaptureScreenForPC(fileName);
#endif

        // 캡처 후 버튼 패널 다시 활성화 
        buttonPanel.SetActive(true);
    }
}