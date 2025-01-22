using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform Player; // Référence au joueur
    public GameObject projectilePrefab; // Préfab pour la boule à tirer
    public Transform firePoint; // Point d'où le projectile est tiré
    public float detectionRange = 15f; // Distance de détection du joueur
    public float attackRange = 10f; // Distance d'attaque
    public float fireRate = 1f; // Intervalle entre les tirs
    private float nextFireTime; // Temps pour le prochain tir

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Player == null) return;

        // Vérifie si le joueur est sur "terre"
        if (IsOnTag(Player, "terre"))
        {
            float distance = Vector3.Distance(transform.position, Player.position);

            // Détection et suivi du joueur
            if (distance <= detectionRange)
            {
                agent.SetDestination(Player.position);

                // Attaque si dans la portée
                if (distance <= attackRange && Time.time >= nextFireTime)
                {
                    Attack();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
        else
        {
            // Stoppe le mouvement si le joueur n'est pas sur "terre"
            agent.ResetPath();
        }
    }

    // Fonction pour vérifier si un objet est sur un tag donné
    private bool IsOnTag(Transform target, string tag)
    {
        if (Physics.Raycast(target.position, Vector3.down, out RaycastHit hit))
        {
            return hit.collider.CompareTag(tag);
        }
        return false;
    }

    // Fonction pour tirer un projectile
    private void Attack()
    {
        if (firePoint != null && projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = (Player.position - firePoint.position).normalized * 10f; // Ajustez la vitesse selon vos besoins
            }
        }
    }
}
