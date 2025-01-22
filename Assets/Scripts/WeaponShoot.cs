using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Le prefab de la balle
    public Transform firePoint; // Point d'où part la balle
    public float bulletSpeed = 20f; // Vitesse de la balle
    public Camera playerCamera; // Référence à la caméra du joueur

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clique gauche
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null && playerCamera != null)
        {
            // Calcule la direction en utilisant un Raycast depuis le centre de l'écran
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Vector3 shootDirection;

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                // Si le raycast touche quelque chose, la balle va dans cette direction
                shootDirection = (hit.point - firePoint.position).normalized;
            }
            else
            {
                // Sinon, la balle suit le raycast dans le vide
                shootDirection = ray.direction;
            }

            // Crée une balle au point de tir
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Applique la direction et la vitesse à la balle
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * bulletSpeed;
            }

            // Optionnel : Détruire la balle après un certain temps
            Destroy(bullet, 5f);
        }
    }
}