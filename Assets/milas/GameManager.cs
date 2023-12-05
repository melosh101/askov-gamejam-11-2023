using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool AudioActive = false;
    public Slider soundBar;
    private AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        soundBar.maxValue = 1;
        soundBar.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        soundBar.value = audioManager.loudness;
    }
    
}

public interface IGameEventTarget : IEventSystemHandler
{
    public void AudioTrigger()
    {
        
    }
}
