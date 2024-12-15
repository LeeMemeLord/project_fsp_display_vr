using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseSensitivity = 100f;

    [SerializeField]
    Transform playerbody;

    [SerializeField]
    float xRotation = 0f;

    // Start est appel�e avant le premier frame update
    void Start()
    {

    }

    // Update est appel�e � chaque frame
    void Update()
    {
        // R�cup�re les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ajuste la rotation en fonction du mouvement vertical de la souris
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Applique la rotation au transform local
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerbody.Rotate(Vector3.up * mouseX);

        // V�rifie si le joueur est mort
        if (!GameManager.Instance.GetIsPlayerDead())
        {
            // V�rifie si le jeu est en pause ou non
            if (!GameManager.Instance.GetGameState())
            {
                // V�rifie si l'interface du jeu est active
                if (!GameManager.Instance.GetCanvasEnable())
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
