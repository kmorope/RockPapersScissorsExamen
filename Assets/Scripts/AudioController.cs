using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource _audioSource;

    public string key;

	// Use this for initialization
	void Start () {
        ValidarAudio(); 
    }

    void Update()
    {
        ValidarAudio(); 
    }

    public void ValidarAudio()
    {
        if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 1)
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }

}
