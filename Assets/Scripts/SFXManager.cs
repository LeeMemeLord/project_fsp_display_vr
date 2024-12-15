using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXmanager : MonoBehaviour
{
    public static SFXmanager Instance;

    [SerializeField]
    AudioClip bgMusic;

    [SerializeField]
    GameObject musicPrefab;

    [SerializeField]
    GameObject soundPrefab;

    [SerializeField]
    AudioClip gunShot;

    [SerializeField]
    AudioClip gunReload;

    [SerializeField]
    AudioClip grenadeExplosion;

    [SerializeField]
    AudioClip gunSwitch;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameObject BgMusicObj = Instantiate(musicPrefab, this.transform);

        if (BgMusicObj != null)
        {
            AudioSource audioSource = BgMusicObj.GetComponent<AudioSource>();
            audioSource.volume = AudioManager.instance.getMusicVolume();
            audioSource.clip = bgMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlaySound(Vector3 position, string soundName)
    {
        GameObject soundObj = Instantiate(soundPrefab, position, Quaternion.identity, this.transform);
        AudioSource audioSource = soundObj.GetComponent<AudioSource>();
        audioSource.volume = AudioManager.instance.getSFVolume();
        AudioClip clipToPlay = null;

        switch (soundName)
        {
            case "gunShot":
                clipToPlay = gunShot;
                break;
            case "gunReload":
                clipToPlay = gunReload;
                break;
            case "grenadeExplosion":
                clipToPlay = grenadeExplosion;
                break;
            case "gunSwitch":
                clipToPlay = gunSwitch;
                break;
            default:
                Destroy(soundObj);
                return;
        }

        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay);
            StartCoroutine(DestroyAudioSourceAfterPlay(soundObj, clipToPlay.length));
        }
    }

    private IEnumerator DestroyAudioSourceAfterPlay(GameObject soundObj, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(soundObj);
    }
}
