using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public void PlayScene(string scene)
    { 
        SceneChange(scene);
    }

    public void PlaySound()
    {
        if (PlayerPrefs.GetInt("audio") == 1)
            this.GetComponent<AudioSource>().Play(); 
    }

    private void SceneChange(string scene)
    {
        SceneLoader.LoadScene(scene);
    }

}
