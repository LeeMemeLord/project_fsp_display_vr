using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMAnager : MonoBehaviour
{
    // Start is called before the first frame update
   private  GameObject menuTitre;
   private  GameObject menuPrincipal;
   private  GameObject menuJouer;
   private  GameObject menuOption;

    [SerializeField]
    AudioClip clip;

    private AudioSource audioSourceMusic;
    private AudioSource audioSourceSound;

    private Slider SliderSound;
    private TextMeshProUGUI SliderSoundText;

    private Slider SliderMusic;
    private TextMeshProUGUI SliderMusicText;


    void Start()
    {
        menuTitre = GameObject.Find("MenuTitre");
        menuPrincipal = GameObject.Find("MenuPrincipal");
        menuJouer = GameObject.Find("MenuJouer");
        menuOption = GameObject.Find("MenuOption");
        initialiserLesSons();
        menuTitre.SetActive(true);
        menuPrincipal.SetActive(false);
        menuJouer.SetActive(false);
        menuOption.SetActive(false);
        

    }

    public void initialiserLesSons()
    {
        audioSourceMusic = GameObject.Find("MusicSource").GetComponent<AudioSource>();

        if (audioSourceMusic != null)
        {
            audioSourceMusic.clip = clip;
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();
        }
        audioSourceSound = GameObject.Find("SoundSource").GetComponent<AudioSource>();
        initierSliders();
    }
    public void initierSliders()
    {
      
        SliderSound = GameObject.Find("SliderSound").GetComponent<Slider>();
        SliderSoundText = GameObject.Find("TextSonCurent").GetComponent<TextMeshProUGUI>();
        SliderSoundText.text = Mathf.CeilToInt(SliderSound.value * 100).ToString();

        SliderMusic = GameObject.Find("MusiqueSliderr").GetComponent<Slider>();
        SliderMusicText = GameObject.Find("TextMusiqueCurent").GetComponent<TextMeshProUGUI>();
        SliderMusicText.text = Mathf.CeilToInt(SliderMusic.value * 100).ToString();
    }

    public void OnSoundSliderChange()
    {
        
        SliderSoundText.text = Mathf.CeilToInt(SliderSound.value * 100).ToString();
        audioSourceSound.volume = SliderSound.value;
        AudioManager.instance.setSFVolume(SliderSound.value); 
    }

    public void OnMusicSliderChange()
    {
        SliderMusicText.text = Mathf.CeilToInt(SliderMusic.value * 100).ToString();
        audioSourceMusic.volume = SliderMusic.value;
        AudioManager.instance.setMusiqueVolume(SliderMusic.value);
    }

    public void goToMenuPrincipal() {
        menuTitre.SetActive(false);
        menuPrincipal.SetActive(true);
    }

    public void goToMenuJouer() { 
        menuPrincipal.SetActive(false);
        menuJouer.SetActive(true);
    }

    public void goToMenuOption() {
        menuPrincipal.SetActive(false);
        menuOption.SetActive(true);
    }

    public void goBackToMenuPrincipalFromMenuJouer() { 
        menuJouer.SetActive(false);
        menuPrincipal.SetActive(true);
    }

    public void goBackToMenuPrincipalFromMenuOption() {
        menuOption.SetActive(false);
        menuPrincipal.SetActive(true);
    }
    public void changeSceneNormal() {
        DiffManager.instance.setIsDiff(false);
        SceneManager.LoadScene("Level_01");
    }

    public void changeSceneDiff()
    {
        DiffManager.instance.setIsDiff(true);
        SceneManager.LoadScene("Level_01");
    }


    // Update is called once per frame
    void Update()
    {
     
    }

  
}
