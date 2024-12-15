using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

// Classe pour définir les multiplicateurs de dégâts pour chaque membre
public class Membres
{
    public float DpsToTete { get; set; } = 4f;
    public float DpsToCorps { get; set; } = 1f;
    public float DpsToBrasDroit { get; set; } = 0.25f;
    public float DpsToBrasGauche { get; set; } = 0.25f;
    public float DpsToJambeDroite { get; set; } = 0.25f;
    public float DpsToJambeGauche { get; set; } = 0.25f;
}

public class EnemyLogic : MonoBehaviour
{
    private float health = 100f;
    bool isLookingAt = false;
    bool canShoot = true;

    // Référence au pistolet
    [SerializeField]
    GameObject pistolet;

    WeaponLogic pistolLogic;

    // Référence au détecteur
    [SerializeField]
    GameObject dectecteur;

    // Référence à l'objet qui sera lâché
    [SerializeField]
    GameObject drop;

    Membres membreEnnemi;

    GameObject zoneDetection;
    GameObject zoneDetectionDiff;

    NavMeshAgent NavMeshAgent { get; set; }
    string zoneType;

    // Start est appelée avant le premier frame update
    void Start()
    {
        NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        zoneDetection = GameObject.Find("zoneDetection");
        zoneDetectionDiff = GameObject.Find("zoneDetectionDiff");
        if (DiffManager.instance.getIsDiff())
        {
            NavMeshAgent.speed = NavMeshAgent.speed * 1.25f;
            zoneType = "zoneDetectionDiff";
        }
        else
        {
            zoneType = "zoneDetection";
        }
        SetZoneType(zoneType);
        pistolLogic = pistolet.GetComponent<WeaponLogic>();
        membreEnnemi = new Membres();
    }

    // Update est appelée à chaque frame
    void Update()
    {
        CheckIfDead();
        CheckIfPlayerIsHisZone();
        UpdateDetecteur();
    }

    // Applique les dégâts reçus par le joueur en fonction du membre touché
    public void HitFromPLayer(string membreTouche, float dps)
    {
        float dpsToMembre = GetDpsMultiplicator(membreTouche);
        ApplyDps(dpsToMembre, dps);
    }

    // Vérifie si l'ennemi est mort
    public void CheckIfDead()
    {
        if (health <= 0f)
        {
            if (drop != null)
            {
                Instantiate(drop, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    // Retourne le multiplicateur de dégâts en fonction du membre touché
    public float GetDpsMultiplicator(string membreTouche)
    {
        return membreTouche switch
        {
            "tete" => membreEnnemi.DpsToTete,
            "corps" => membreEnnemi.DpsToCorps,
            "brasDroit" => membreEnnemi.DpsToBrasDroit,
            "brasGauche" => membreEnnemi.DpsToBrasGauche,
            "jambeDroite" => membreEnnemi.DpsToJambeDroite,
            "jameGauche" => membreEnnemi.DpsToJambeGauche,
            _ => 0f,
        };
    }

    // Applique les dégâts calculés à l'ennemi
    public void ApplyDps(float membreMultiplicator, float dps)
    {
        float totalDps = dps * membreMultiplicator;
        health -= totalDps;
    }

    // Commence à regarder le joueur
    public void LookAtPLayer()
    {
        if (!isLookingAt)
        {
            SetIsLookingAt(true);
        }
    }

    // Trouve le détecteur dans la hiérarchie de l'objet
    public void FindDetecter()
    {
        Transform parent = gameObject.transform.Find("detecteur");
        if (parent != null)
        {
            dectecteur = parent.gameObject;
        }
    }

    // Arrête de regarder le joueur
    public void DontLookAtPLayer()
    {
        if (isLookingAt)
        {
            SetIsLookingAt(false);
        }
    }

    // Définit l'état de "regarder" du joueur
    public void SetIsLookingAt(bool isLookingAt)
    {
        this.isLookingAt = isLookingAt;
    }

    // Retourne le détecteur
    public GameObject GetDetecteur()
    {
        return dectecteur;
    }

    // Vérifie si le joueur est dans la zone de l'ennemi
    public void CheckIfPlayerIsHisZone()
    {
        if (isLookingAt)
        {
            PointAndShoot();
        }
    }

    // Met à jour le détecteur dans le GameManager
    public void UpdateDetecteur()
    {
        GameManager.Instance.UpdateDetecteur(dectecteur);
    }

    // Vise et tire sur le joueur
    public void PointAndShoot()
    {
        GameManager.Instance.PointEnemyAtPayer(this);
        if (GameManager.Instance.GetIsEnemyCloseToShoot(this))
        {
            if (canShoot)
            {
                canShoot = false;
                Shoot();
                Invoke("ReActivateCanShoot", 2f);
            }
        }
    }

    // Réactive la capacité de tirer
    public void ReActivateCanShoot()
    {
        canShoot = true;
    }

    // Tire sur le joueur
    public void Shoot()
    {
        SFXmanager.Instance.PlaySound(this.transform.position, "gunShot");
        float dps = pistolLogic.Hit();
        GameManager.Instance.ShootPlayer(dps);
    }

    // Définit le type de zone de détection
    public void SetZoneType(string zoneType)
    {
        switch (zoneType)
        {
            case "zoneDetectionDiff":
                zoneDetection.SetActive(false);
                break;

            case "zoneDetection":
                zoneDetectionDiff.SetActive(false);
                break;
        }
    }
}
