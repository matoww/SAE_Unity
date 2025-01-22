using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet touché a le tag "Ennemi"
        if (other.CompareTag("Ennemie"))
        {
            Destroy(other.gameObject); // Détruit l'ennemi
            Destroy(gameObject); // Détruit la balle
        }
        else
        {
            // Détruit la balle si elle touche autre chose
            Destroy(gameObject, 3f); // Optionnel : détruit après 3 secondes si pas d'ennemi
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        // Vérifie si l'objet touché a le tag "Ennemi"
        if (other.gameObject.CompareTag("Ennemie"))
        {
            Destroy(other.gameObject); // Détruit l'ennemi
            Destroy(gameObject); // Détruit la balle
        }
        else
        {
            // Détruit la balle si elle touche autre chose
            Destroy(gameObject, 3f); // Optionnel : détruit après 3 secondes si pas d'ennemi
        }
    }
}