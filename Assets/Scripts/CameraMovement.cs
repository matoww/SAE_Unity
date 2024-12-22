using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    private Transform player; // Référence au joueur

    void Start()
    {
        // Trouver l'objet avec le tag "joueur" au démarrage
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Aucun objet avec le tag 'joueur' trouvé !");
        }
    }

    void LateUpdate()
    {
        // Si le joueur a été trouvé, suivre sa position
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y; // Garder la hauteur constante
            transform.position = newPosition;

            // Optionnel : orienter la minimap en fonction de la rotation du joueur
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }
}