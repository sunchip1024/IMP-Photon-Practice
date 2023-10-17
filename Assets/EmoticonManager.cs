using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using static EmoticonManager;

public class EmoticonManager : MonoBehaviour
{
    public PhotonView PV;

    public GameObject EmoticonPanel;
    public Image EmoticonImage;
    public int CurrentEmoticonIndex;

    public List<Sprite> EmoticonList;

    public enum Emoticon
    {
        ANGRY,
        HAPPY,
        SAD
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentEmoticonIndex = 0;
        DisableEmoticonImage();
    }

    public void SetEmoticon(string emoticon)
    {
        EmoticonImage.enabled = true;
        Debug.Log(emoticon);
        Emoticon _emoticon;
        if (Enum.TryParse(emoticon, out _emoticon))
        {
            int index = (int)_emoticon;
            EmoticonImage.sprite = EmoticonList[index];
            CurrentEmoticonIndex = index;
        }
        else
        {
            Debug.Log("잘못된 string입니다");
        }

        //int index = (int)(Emoticon)Enum.Parse(typeof(Emoticon), emoticon);
        //string to index
        Invoke("DisableEmoticonImage", 2f);

    }

    private void DisableEmoticonImage()
    {
        EmoticonImage.enabled = false;
    }


}
