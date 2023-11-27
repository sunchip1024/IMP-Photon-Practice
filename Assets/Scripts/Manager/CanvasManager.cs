using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    private void Awake()
    {
        instance = this;
    }
    public Canvas[] canvases; // ĵ���� �迭

    // ĵ������ Event Camera�� �ʱ�ȭ
    [ContextMenu("�̺�Ʈ ī�޶� ��")]
    public void SetCamera(Camera eventCamera)
    {
        Debug.Log("ī�޶� ����");

        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].worldCamera = eventCamera;
        }
    }

    public void ZoomInImage()
    {
        Debug.Log("����!");
        UIManager.Instance.ShowZoomInCanvas();
    }

    // ĵ������ Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
    //public void SetCanvasActive(int canvasIndex, bool isActive)
    //{
    //    if (canvasIndex >= 0 && canvasIndex < canvases.Length)
    //    {
    //        canvases[canvasIndex].enabled = isActive;
    //    }
    //}
}
