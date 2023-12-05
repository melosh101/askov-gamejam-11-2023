using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField, Range(0, 1000)]
    public int sampleWindow;
    public float loudness;
    
    private AudioClip _audioClip;
    private string _device;
    private AudioSource _audioSource;
    private static bool _isRecording;
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _device = PlayerPrefs.GetString("microphone");
        _audioSource = GetComponent<AudioSource>();
        _audioClip = Microphone.Start(_device, true, 20, AudioSettings.outputSampleRate);
        
        _gameManager = GetComponent<GameManager>();
    }
    
    void Update()
    {
        int dec = 128;
        float[] waveData = new float[dec];
        int micPosition = Microphone.GetPosition(null)-(dec+1); //Null tager den første mic i Microphone.devices 
        _audioClip.GetData(waveData, micPosition);
        
        float levelMax = 0;
        for (int i = 0; i < dec; i++) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        loudness = Mathf.Sqrt(Mathf.Sqrt(levelMax));
    }

    void start()
    {
        _audioClip = Microphone.Start(_device,true,999,44100);
        _isRecording = true;
    }

    #region deviceMGNT
    
    public string[] GetDevices()
    {
        return Microphone.devices;
    }

    public void SetDevice(string device)
    {
        if (Microphone.devices.Contains(device)) _device = device;
    }

    public string GetCurrentDevice()
    {
        return _device;
    }
    #endregion
}
