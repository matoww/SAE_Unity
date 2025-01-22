using UnityEngine;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    public TMP_Text interactText; // Texte "Press E to Take"
    public GameObject weaponInHand; // Référence à l’arme dans la main du joueur
    private bool isPlayerNearby = false; // Vérifie si le joueur est proche

    void Start()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false); // Cache le texte au début
        }

        if (weaponInHand != null)
        {
            weaponInHand.SetActive(false); // Cache l’arme dans la main au début
        }
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
            interactText.gameObject.SetActive(true); // Affiche le texte
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactText.gameObject.SetActive(false); // Cache le texte
        }
    }

    private void TakeWeapon()
    {
        if (weaponInHand != null)
        {
            weaponInHand.SetActive(true); // Active l’arme dans la main
        }

        interactText.gameObject.SetActive(false); // Cache le texte
        Destroy(gameObject); // Supprime l’arme de la scène
    }
}