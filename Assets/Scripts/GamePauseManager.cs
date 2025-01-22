using UnityEngine;
using UnityEngine.UI;
using TMPro;  // N'oublie pas d'importer TMP

public class GamePauseManager : MonoBehaviour
{
    private bool isPaused = false;

    // Références aux éléments UI
    public Image grayFilter;
    public TMP_Text pauseText;  // Remplace Text par TMP_Text

    void Start()
    {
        // Cache l'UI de pause au démarrage
        grayFilter.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Vérifie si le joueur appuie sur la touche Échap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        // Met le temps à zéro pour figer tout ce qui dépend du Time.deltaTime
        Time.timeScale = 0f;
        isPaused = true;

        // Active le filtre gris et le texte "Pause"
        grayFilter.gameObject.SetActive(true);
        pauseText.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        // Remet le temps à la normale
        Time.timeScale = 1f;
        isPaused = false;

        // Désactive le filtre gris et le texte "Pause"
        grayFilter.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);
    }
}