using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float musicVolume;
    private float sfVolume;
    public static AudioManager instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setMusiqueVolume(float volume)
    {
        musicVolume = volume;
    }

    public float getMusicVolume()
    {
        return musicVolume;
    }

    public void setSFVolume(float volume)
    {
        sfVolume = volume;
    }

    public float getSFVolume()
    {
        return sfVolume;
    }
}
