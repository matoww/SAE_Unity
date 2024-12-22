using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    private GameObject panel; // Référence au Panel

    void Start()
    {
        // Cherche l'objet avec le tag "Panel"
        panel = GameObject.FindGameObjectWithTag("Panel");

        // S'assure que le Panel est désactivé au démarrage
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Aucun objet avec le tag 'Panel' trouvé.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Vérifie si "P" est pressé
        {
            if (panel != null)
            {
                // Alterne l'état actif du Panel
                panel.SetActive(!panel.activeSelf);

                // Met le jeu en pause si le Panel est actif
                if (panel.activeSelf)
                {
                    Time.timeScale = 0f; // Pause du jeu
                }
                else
                {
                    Time.timeScale = 1f; // Reprise du jeu
                }
            }
        }
    }
}