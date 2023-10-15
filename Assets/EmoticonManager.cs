using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoticonManager : MonoBehaviour
{
    public GameObject EmoticonPanel;
    public Image EmoticonImage;
    public int CurrentEmoticonIndex;

    public List<Sprite> EmoticonList;


    // Start is called before the first frame update
    void Start()
    {
        CurrentEmoticonIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("이모티콘 변경")]
    void ChangeEmoticon()
    {
        CurrentEmoticonIndex++;
        CurrentEmoticonIndex %= EmoticonList.Count;

        EmoticonImage.sprite = EmoticonList[CurrentEmoticonIndex];
        
    }
}
