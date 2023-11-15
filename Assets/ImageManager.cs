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

    public List<GameObject> ModelList;

    public GameObject HongboPanel;
    public Text HongboText;


    public void ClearModels()
    {
        foreach(GameObject model in ModelList)
        {
            model.SetActive(false);
        }
    }

    [ContextMenu("이미지 변경")]
    public void ChangeImage()
    {
        foreach(GameObject panel in PromotionPanelList)
        {
            panel.GetComponent<Canvas>().GetComponentInChildren<Image>().sprite = SpriteList[0];
        }
    }

    //public void ChangeImage(int index)
    //{
    //    foreach (GameObject panel in PromotionPanelList)
    //    {
    //        panel.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().sprite = SpriteList[index];
    //    }
    //}

    public void ChangeTeam(int index)
    {
        Debug.Log($"팀 변경이래요! index {index}");
        Debug.Log($"팀 변경 : {index}({index % 3})");
        index %= 3;
        foreach(GameObject canvas in PromotionPanelList)
        {
            canvas.GetComponentInChildren<Image>().sprite = SpriteList[index];
        }
        //HongboPanel.GetComponent<Image>().sprite = SpriteList[index];
        //HongboText.text = index + "번째 팀 홍보공간";

        ClearModels();
        ModelList[index].SetActive(true);
        
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
