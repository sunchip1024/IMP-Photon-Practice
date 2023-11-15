using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    private void Awake()
    {
        instance = this;
    }
    public Canvas[] canvases; // 캔버스 배열

    // 캔버스와 Event Camera를 초기화
    [ContextMenu("이벤트 카메라 셋")]
    public void SetCamera(Camera eventCamera)
    {
        Debug.Log("카메라 세팅");

        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].worldCamera = eventCamera;
        }
    }

    public void ZoomInImage()
    {
        Debug.Log("줌인!");
        UIManager.Instance.ShowZoomInCanvas();
    }

    // 캔버스를 활성화 또는 비활성화
    //public void SetCanvasActive(int canvasIndex, bool isActive)
    //{
    //    if (canvasIndex >= 0 && canvasIndex < canvases.Length)
    //    {
    //        canvases[canvasIndex].enabled = isActive;
    //    }
    //}
}
