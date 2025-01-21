using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponHUD; // Référence au modèle d'arme sur l'écran
    public Text interactText;   // Référence au texte à afficher
    private bool isPlayerNearby = false; // Vérifie si le joueur regarde l'arme

    void Start()
    {
        weaponHUD.SetActive(false); // Cache l'arme dans le HUD au début
        interactText.gameObject.SetActive(false); // Cache le texte d'interaction au début
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            TakeWeapon();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactText.gameObject.SetActive(false);
        }
    }

    private void TakeWeapon()
    {
        weaponHUD.SetActive(true); // Affiche l'arme sur l'écran
        interactText.gameObject.SetActive(false); // Cache le texte
        Destroy(gameObject); // Supprime l'arme de la scène
    }
}