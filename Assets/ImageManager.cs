using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public static ImageManager instance;
    public List<GameObject> ObjectList;
    public List<Mesh> MeshList;

    public List<GameObject> PromotionPanelList;

    public List<Sprite> SpriteList;

    [ContextMenu("이미지 변경")]
    public void ChangeImage()
    {
        foreach(GameObject panel in PromotionPanelList)
        {
            panel.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().sprite = SpriteList[0];
        }
    }

    public void ChangeImage(int index)
    {
        foreach (GameObject panel in PromotionPanelList)
        {
            panel.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().sprite = SpriteList[index];
        }
    }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
