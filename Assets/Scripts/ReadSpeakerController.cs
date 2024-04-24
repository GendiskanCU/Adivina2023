using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ReadSpeaker;

public class ReadSpeakerController : MonoBehaviour
{
    private TTSSpeaker _speaker;

    // Start is called before the first frame update
    void Start()
    {
        TTS.Init();
        _speaker = GetComponent<TTSSpeaker>();        
    }

    public void SpeakText(string texto)
    {
        TTS.SayAsync(texto, _speaker);
    }

    
}
