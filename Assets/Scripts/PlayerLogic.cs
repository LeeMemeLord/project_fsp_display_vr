using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLogic : MonoBehaviour
{
    private CharacterController characterController;
    private float characterSpeed = 15f;
    private WeaponLogic currentWeaponLogic;
    private float health = 100f;
    private float throwForce = 18f;
    private TextMeshProUGUI textForHealth;
    private string currentWeapon;
    private int totalAmmo = 0;

    private List<string> inventaire;

    [SerializeField]
    GameObject pistolet;

    [SerializeField]
    GameObject smg;

    [SerializeField]
    GameObject riffle;

    [SerializeField]
    GameObject grenade;

    private WeaponLogic pistolLogic;
    private WeaponLogic smgLogic;
    private WeaponLogic riffleLogic;


    void Start()
    {
        currentWeapon = "pistol";
        characterController = GetComponent<CharacterController>();

        pistolLogic = pistolet.GetComponent<WeaponLogic>();
        smgLogic = smg.GetComponent<WeaponLogic>();
        riffleLogic = riffle.GetComponent<WeaponLogic>();

        currentWeaponLogic = pistolLogic;

        smg.SetActive(false);
        riffle.SetActive(false);

        textForHealth = GameObject.Find("healthText").GetComponent<TextMeshProUGUI>();
        inventaire = new List<string> { "pistol" };
    }


    void Update()
    {
        // Méthode pour mettre à jour le mouvement du joueur
        UpdateMovment();
        GestionTire();
        UpdateHealth();
        CheckIfDead();
        TryTrowGrenade();
        GestionWeapon();
        GestionRealod();
        UpdateAmmoText();
    }

    public void UpdateMovment()
    {

        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xMove + transform.forward * yMove;

        characterController.Move(move * characterSpeed * Time.deltaTime);
    }


    // Méthode pour définir l'arme actuelle du joueur
    public void SetCurrentWeapon(string weapon)
    {
        if (weapon.Equals("pistol"))
        {
            pistolet.SetActive(true);
            smg.SetActive(false);
            riffle.SetActive(false);
            currentWeaponLogic = pistolLogic;
        }
        if (weapon.Equals("smg"))
        {
            smg.SetActive(true);
            pistolet.SetActive(false);
            riffle.SetActive(false);
            currentWeaponLogic = smgLogic;
        }
        if (weapon.Equals("riffle"))
        {
            riffle.SetActive(true);
            smg.SetActive(false);
            pistolet.SetActive(false);
            currentWeaponLogic = riffleLogic;
        }
    }

    // Méthode pour gérer le tir du joueur
    public void GestionTire()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0f)
        {
            if (currentWeaponLogic.GetCurrentAmmo() > 0)
            {
                SFXmanager.Instance.PlaySound(this.transform.position, "gunShot");
                currentWeaponLogic.Shoot();
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Collider hitZone = hit.collider;
                    Tirer(hitZone, currentWeaponLogic);
                }
            }
            else if (currentWeaponLogic.GetCurrentAmmo() == 0)
            {
                GameManager.Instance.StartReloading(currentWeaponLogic, totalAmmo);

            }
        }
    }

    // Méthode pour gérer le changement d'arme du joueur
    public void GestionWeapon()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            SFXmanager.Instance.PlaySound(this.transform.position, "gunSwitch");
            currentWeapon = GetNextWeapon("right", currentWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SFXmanager.Instance.PlaySound(this.transform.position, "gunSwitch");
            currentWeapon = GetNextWeapon("left", currentWeapon);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentWeapon = GetNextWeapon("right", currentWeapon);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentWeapon = GetNextWeapon("left", currentWeapon);
        }
        SetCurrentWeapon(currentWeapon);
    }


    // Méthode pour gérer le rechargement de l'arme actuelle du joueur
    public void GestionRealod()
    {
        if (Input.GetKeyDown(KeyCode.R) && !currentWeaponLogic.IsReloading() && currentWeaponLogic.GetCurrentAmmo() < currentWeaponLogic.GetMag())
        {
            GameManager.Instance.StartReloading(currentWeaponLogic, totalAmmo);

        }
    }

    // Méthode pour obtenir l'arme suivante dans l'inventaire du joueur
    public string GetNextWeapon(string switchDirection, string curentWeapon)
    {
        List<string> availableWeapons = new List<string> { "pistol" };

        if (inventaire.Contains("smg"))
        {
            availableWeapons.Add("smg");
        }
        if (inventaire.Contains("riffle"))
        {
            availableWeapons.Add("riffle");
        }

        int currentIndex = availableWeapons.IndexOf(curentWeapon);

        if (switchDirection == "right")
        {
            currentIndex = (currentIndex + 1) % availableWeapons.Count;
        }
        else if (switchDirection == "left")
        {
            currentIndex = (currentIndex - 1 + availableWeapons.Count) % availableWeapons.Count;
        }

        return availableWeapons[currentIndex];
    }

    // Méthode pour mettre à jour le texte d'affichage des munitions
    public void UpdateAmmoText()
    {
        int currentAmmo = currentWeaponLogic.GetCurrentAmmo();
        GameManager.Instance.UpdateAmmoText(currentAmmo, totalAmmo);
    }

    // Méthode pour tirer sur une zone spécifique avec une arme donnée
    public void Tirer(Collider hitZone, WeaponLogic weapon)
    {
        if (!hitZone.name.Equals("zoneDetection"))
        {
            GameManager.Instance.HitEnemy(hitZone, weapon);
        }

    }

    // Méthode pour obtenir la santé actuelle du joueur
    public float GetHealth()
    {
        return health;
    }

    // Méthode pour définir la santé du joueur
    public void SetHealth(float newHealth)
    {
        this.health = newHealth;
        if (health < 0)
        {
            health = 0;
        }
    }

    // Méthode pour définir le nombre total de munitions du joueur
    public int GetTotalAmmo()
    {
        return totalAmmo;
    }

    // Méthode pour définir le nombre total de munitions du joueur
    public void SetTotalAmmo(int newValue)
    {
        totalAmmo = newValue;
        if (totalAmmo > 100)
        {
            totalAmmo = 100;
        }
    }

    // Méthode pour obtenir la taille de l'inventaire du joueur
    public int GetInventorySize()
    {
        return inventaire.Count;
    }

    // Méthode pour ajouter un objet à l'inventaire du joueur
    public void AddToInventory(string pickUp)
    {
        inventaire.Add(pickUp);
    }

    // Méthode pour mettre à jour l'affichage de la santé du joueur
    public void UpdateHealth()
    {
        textForHealth.text = Mathf.Round(health).ToString() + " HP";
    }

    // Méthode pour vérifier si le joueur est mort
    public void CheckIfDead()
    {
        if (health <= 0)
        {
            GameManager.Instance.SetGameToDeathMenu();
        }
    }

    // Méthode pour essayer de lancer une grenade
    public void TryTrowGrenade()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (inventaire.Contains("grenade"))
            {
                TrowGrenade();
            }
            inventaire.Remove("grenade");
        }
    }

    // Méthode pour lancer effectivement une grenade
    public void TrowGrenade()
    {
        float spawnOffset = 2.0f;
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnOffset;
        GameObject grenadeInstance = Instantiate(grenade, spawnPosition, Camera.main.transform.rotation);

        Rigidbody grenadeRigidbody = grenadeInstance.GetComponent<Rigidbody>();
        if (grenadeRigidbody != null)
        {
            grenadeRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

        }
    }


    public void OnTriggerEnter(Collider other)
    {

        if (CheckForPickUp(other))
        {
            GameManager.Instance.PickUpIteam(other);
        }
        if (other.name.Equals("zoneFin"))
        {
            GameManager.Instance.TryToEnd(inventaire);
        }
    }

    // Méthode pour vérifier si un objet est ramassable
    public bool CheckForPickUp(Collider other)
    {
        PickUpsLogic pickUpLogic = other.gameObject.GetComponent<PickUpsLogic>();
        if (pickUpLogic != null && !inventaire.Contains(pickUpLogic.GetPickUpType()))
        {
            return true;
        }
        else
        {
            PickUpsLogic pickUpsLogic2 = other.gameObject.GetComponentInParent<PickUpsLogic>();
            if (pickUpsLogic2 != null && !inventaire.Contains(pickUpsLogic2.GetPickUpType()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }



    public void OnTriggerStay(Collider other)
    {

        if (other.name.Equals("zoneDetection") || other.name.Equals("zoneDetectionDiff"))
        {
            GameManager.Instance.StayOnHimTiger(other);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("zoneDetection") || other.name.Equals("zoneDetectionDiff"))
        {
            GameManager.Instance.DontGetHimTiger(other);
        }
    }
}
