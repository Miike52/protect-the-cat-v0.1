using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManagerInstance;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (audioManagerInstance == null){
            audioManagerInstance = this;
        }
        else{
            Destroy(audioManagerInstance); 
        }
    }

}
