using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;
    public player _player;

    private void Awake()
    {
        instance = this;
    }


    public void ChangeMouseSensitivity(int value)
    {
        //_player.
    }

    public void EmitEmoticonChange(string emoticon)
    {
        _player.SetEmoticon(emoticon);
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
