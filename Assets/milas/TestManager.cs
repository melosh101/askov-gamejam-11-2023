using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    private AudioManager _audioManager;

    public TextMeshProUGUI loudnessText;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GetComponent<AudioManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        loudnessText.text = _audioManager.loudness.ToString(CultureInfo.CurrentCulture);
    }
}
