using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    private PlayerLogic playerLogic;

    private Canvas canvas;

    private Button buttonContiuer;
    private Button buttonQuitter;

    private TextMeshProUGUI textAmmo;
    private TextMeshProUGUI textTime;
    private TextMeshProUGUI finalTimerText;

    [SerializeField]
    AudioClip grenadeEplosion;

    [SerializeField]
    GameObject blood;

    private AudioSource musicSource;
    private AudioSource soundSource;

    private GameObject MenuOfDeath;
    private GameObject MenuOfVictory;
    private GameObject MenuNotify;
    private GameObject FinalTimerTimeContaienr;
    private GameObject Reticle;

    private bool canPressEsc = true;
    private bool isGameFinished = false;
    private bool isPlayerDead = false;
    private bool isDifficult;

    private int ammoBoxValue = 10;

    private float elapsedTime = 0f;
    private float ennemyPrecision;




    void Start()
    {
        SetUpDifficulty();
        SetUpButtons();
        SetUpPlayer();
        SetUpCanvas();
        SetUpText();
        StartCoroutine(UpdateTimer());

        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        musicSource.volume = AudioManager.instance.getMusicVolume();

        soundSource = GameObject.Find("SoundSource").GetComponent<AudioSource>();
        soundSource.volume = AudioManager.instance.getSFVolume();




    }

    // Configure la difficulté du jeu
    public void SetUpDifficulty()
    {
        isDifficult = DiffManager.instance.getIsDiff();

        if (isDifficult)
        {
            ennemyPrecision = 0.50f;
        }
        else
        {
            ennemyPrecision = 0.33f;
        }

    }

    // Configure le canvas et les menus
    public void SetUpCanvas()
    {
        canvas = GameObject.Find("MenuDuJeu").GetComponent<Canvas>();
        canvas.enabled = false;
        Cursor.visible = true;
        Time.timeScale = 1f;
        MenuOfDeath = GameObject.Find("DeathMenuContainer");
        MenuOfDeath.SetActive(false);
        Reticle = GameObject.Find("CenterOfScreenContainer");
        MenuOfVictory = GameObject.Find("VictoryMenuContainer");
        MenuOfVictory.SetActive(false);
        MenuNotify = GameObject.Find("containerMessage");
        MenuNotify.SetActive(false);
    }

    // Configure le joueur et sa logique
    public void SetUpPlayer()
    {
        player = GameObject.Find("player");
        playerLogic = player.GetComponent<PlayerLogic>();
    }

    // Configure les textes de l'interface utilisateur
    public void SetUpText()
    {
        textTime = GameObject.Find("textTime").gameObject.GetComponent<TextMeshProUGUI>();
        textAmmo = GameObject.Find("currentAmmoText").gameObject.GetComponent<TextMeshProUGUI>();
        FinalTimerTimeContaienr = GameObject.Find("textFinalTimer");
        finalTimerText = GameObject.Find("textFinalTimer").gameObject.GetComponent<TextMeshProUGUI>();
        FinalTimerTimeContaienr.SetActive(false);
    }

    // Configure les boutons du menu
    public void SetUpButtons()
    {
        buttonContiuer = GameObject.Find("ButtonContinuer").GetComponent<Button>();
        buttonQuitter = GameObject.Find("ButtonQuitter").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfMenuToggle();
        CheckIfGameHasEnded();
        CheckIfPlayerIsDead();
    }

    // Met à jour le minuteur chaque seconde
    IEnumerator UpdateTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
            UpdateTimerText();
        }
    }

    // Met à jour le texte du minuteur
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        textTime.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    // Bascule le menu pause
    public void TogglePauseMenu()
    {
        if (Time.timeScale > 0f)
        {
            canvas.enabled = true;
            Time.timeScale = 0f;
        }
        else
        {
            canvas.enabled = false;
            Time.timeScale = 1f;
        }
    }

    // Vérifie si le menu doit être basculé
    public void CheckIfMenuToggle()
    {
        if (canPressEsc)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu();
            }
        }
    }

    // Vérifie si le jeu est terminé
    public void CheckIfGameHasEnded()
    {
        if (isGameFinished)
        {
            MenuOfVictory.SetActive(true);
            Reticle.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            FinalTimerTimeContaienr.SetActive(true);
            finalTimerText.text = "vous avez fini le jeu en " + textTime.text;

        }
    }

    // Vérifie si le joueur est mort
    public void CheckIfPlayerIsDead()
    {
        if (isPlayerDead)
        {
            MenuOfDeath.SetActive(true);
            Reticle.SetActive(false);
            Time.timeScale = 0f;
            FinalTimerTimeContaienr.SetActive(true);
            finalTimerText.text = "vous etes mort apres " + textTime.text;
        }
    }

    // Reprend le jeu après une pause
    public void ResumeGame()
    {
        TogglePauseMenu();
    }

    // Retourne au menu principal
    public void RetrunToMainMenu()
    {
        canvas.enabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Renvoie l'état d'activation du canvas
    public bool GetCanvasEnable(
        )
    {
        return canvas.enabled;
    }

    // Gère les impacts des tirs sur les ennemis
    public void HitEnemy(Collider membre, WeaponLogic weapon)
    {
        EnemyLogic enemyLogic = membre.GetComponentInParent<EnemyLogic>();
        Instantiate(blood,membre.transform);
        string membreTouche = membre.name;
        if (enemyLogic != null)
        {
            enemyLogic.HitFromPLayer(membreTouche, weapon.Hit());
        }
    }

    // Déclenche l'attaque des ennemis vers le joueur
    public void GoGetHimTigger(Collider collider)
    {
        EnemyLogic enemyLogic = collider.gameObject.GetComponentInParent<EnemyLogic>();
        if (VerifiyIfPlayerIsSeen(enemyLogic))
        {
            enemyLogic.LookAtPLayer();
        }
    }

    // Maintient l'ennemi en train de suivre le joueur
    public void StayOnHimTiger(Collider collider)
    {
        EnemyLogic enemyLogic = collider.gameObject.GetComponentInParent<EnemyLogic>();

        if (VerifiyIfPlayerIsSeen(enemyLogic))
        {
            enemyLogic.LookAtPLayer();
            NavigationLogic navLogic = collider.gameObject.GetComponentInParent<NavigationLogic>();
            if (navLogic != null)
            {
                navLogic.SetPosition(player.transform.position);
            }


        }
        else
        {
            enemyLogic.DontLookAtPLayer();
        }
    }

    // Arrête l'ennemi de suivre le joueur
    public void DontGetHimTiger(Collider collider)
    {
        EnemyLogic enemyLogic = collider.gameObject.GetComponentInParent<EnemyLogic>();
        enemyLogic.DontLookAtPLayer();
    }

    // Pointe l'ennemi vers le joueur
    public void PointEnemyAtPayer(EnemyLogic enemyLogic)
    {
        GameObject enemy = enemyLogic.gameObject;
        enemy.transform.LookAt(player.transform.position);
    }

    // Vérifie si le joueur est visible par l'ennemi
    public bool VerifiyIfPlayerIsSeen(EnemyLogic enemyLogic)
    {
        GameObject detecteur = enemyLogic.GetDetecteur();

        if (detecteur != null)
        {
            Ray ray = new Ray(detecteur.transform.position, detecteur.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.name.Equals("player"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        return false;
    }

    // Met à jour le détecteur de l'ennemi pour suivre le joueur
    public void UpdateDetecteur(GameObject detecteur)
    {
        detecteur.transform.LookAt(player.transform.position);
    }

    // Tire sur le joueur
    public void ShootPlayer(float dps)
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue < ennemyPrecision)
        {
            float health = playerLogic.GetHealth();
            health -= dps;
            playerLogic.SetHealth(health);
        }
    }

    // Met le jeu en mode menu de mort, désactive la touche ESC et met le jeu en pause.
    public void SetGameToDeathMenu()
    {
        canPressEsc = false;
        isPlayerDead = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        MenuOfDeath.SetActive(true);
        Reticle.SetActive(false);
        Time.timeScale = 0f;
    }

    // Retourne si le jeu est terminé ou non.
    public bool GetGameState()
    {
        return isGameFinished;
    }

    // Retourne si le joueur est mort ou non.
    public bool GetIsPlayerDead()
    {
        return isPlayerDead;
    }

    // Réinitialise la scène du jeu en chargeant le premier niveau.
    public void ResetGameScence()
    {
        SceneManager.LoadScene("Level_01");
    }

    // Gère la logique de ramassage d'un objet.
    public void PickUpIteam(Collider collider)
    {
        PickUpsLogic pickUpsLogic = collider.gameObject.GetComponentInParent<PickUpsLogic>();
        if (pickUpsLogic != null)
        {
            if (pickUpsLogic.GetisPickUpable())
            {
                UpdatePlayerInvetory(pickUpsLogic);
            }
        }
    }

    // Met à jour l'inventaire du joueur avec l'objet ramassé.
    public void UpdatePlayerInvetory(PickUpsLogic pickUpsLogic)
    {
        pickUpsLogic.SetisPickUpable(false);
        string typeOfPickUp = pickUpsLogic.GetPickUpType();
        if (!typeOfPickUp.Equals("ammoBox"))
        {
            AddToInventory(typeOfPickUp);
        }
        else
        {
            int totalAmmo = playerLogic.GetTotalAmmo();
            playerLogic.SetTotalAmmo(totalAmmo + ammoBoxValue);
        }
    }

    // Ajoute un objet ramassé à l'inventaire du joueur.
    public void AddToInventory(string pickUp)
    {
        playerLogic.AddToInventory(pickUp);
    }

    // Commence le rechargement de l'arme.
    public void StartReloading(WeaponLogic weapon, int totalAmmo)
    {
        weapon.SetIsReloading(true);
        SFXmanager.Instance.PlaySound(weapon.transform.position, "gunReload");
        int currentMag = weapon.GetCurrentAmmo();
        int fullMag = weapon.GetMag();
        int reloadValue = fullMag - currentMag;

        if (totalAmmo < reloadValue)
        {
            reloadValue = totalAmmo;
        }

        weapon.SetCurrentAmmo(currentMag + reloadValue);
        totalAmmo -= reloadValue;

        playerLogic.SetTotalAmmo(totalAmmo);
        weapon.SetIsReloading(false);
    }

    // Tente de terminer le jeu si tous les objets requis sont ramassés.
    public void TryToEnd(List<string> inventary)
    {
        if (CheckIfAllDocumentsArePickedUp(inventary))
        {
            EndGame();
        }
        else
        {
            NotifyPlayer(inventary);
        }
    }

    // Vérifie si tous les documents requis sont ramassés.
    public bool CheckIfAllDocumentsArePickedUp(List<string> inventaire)
    {
        return inventaire.Contains("key") && inventaire.Contains("case1") && inventaire.Contains("case2");
    }

    // Termine le jeu en définissant l'état du jeu à terminé.
    public void EndGame()
    {
        isGameFinished = true;
    }

    // Notifie le joueur des objets manquants.
    public void NotifyPlayer(List<string> inventary)
    {
        string textDeNotification = "Object(s) à ramasser: \n";
        if (!inventary.Contains("key"))
        {
            textDeNotification += "clé \n";
        }
        if (!inventary.Contains("case1"))
        {
            textDeNotification += "Document 1 \n";
        }
        if (!inventary.Contains("case2"))
        {
            textDeNotification += "Document 2 \n";
        }
        SetNotifyMessege(textDeNotification);
        Invoke("SetNotifyContainerOff", 4f);
    }

    // Met à jour l'affichage du texte de munitions.
    public void UpdateAmmoText(int currentAmmo, int totalAmmo)
    {
        textAmmo.text = currentAmmo.ToString() + "/" + totalAmmo.ToString();
    }

    // Définit le message de notification.
    public void SetNotifyMessege(string textDeNotification)
    {
        MenuNotify.SetActive(true);
        TextMeshProUGUI text = MenuNotify.GetComponentInChildren<TextMeshProUGUI>();
        text.text = textDeNotification;
    }

    // Désactive le conteneur de notification.
    public void SetNotifyContainerOff()
    {
        MenuNotify.SetActive(false);
    }

    // Applique des dégâts par seconde (DPS) d'une grenade à un ennemi ou un joueur.
    public void GrenadeDps(Collider collider, float dps)
    {
        if (collider.name.Equals("enemy"))
        {
            EnemyLogic enemyLogic = collider.GetComponent<EnemyLogic>();
            enemyLogic.ApplyDps(1, dps);
        }
        if (collider.name.Equals("player"))
        {
            PlayerLogic playerLogic = collider.GetComponent<PlayerLogic>();
            float health = playerLogic.GetHealth();
            playerLogic.SetHealth(health - dps);
        }
    }

    // Vérifie si un ennemi est suffisamment proche pour être tiré.
    public bool GetIsEnemyCloseToShoot(EnemyLogic enemy)
    {
        Vector3 enemyPosition = enemy.transform.position;
        Vector3 playerPosition = player.transform.position;
        float distance = Vector3.Distance(enemyPosition, playerPosition);
        return distance <= 5f;
    }

    // Joue le son de tir.


    // Joue le son de rechargement.

    // Joue le son de l'explosion de grenade.
    public void PlayGrenade()
    {
        soundSource.PlayOneShot(grenadeEplosion);
    }


}
