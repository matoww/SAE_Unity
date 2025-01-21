using UnityEngine;
using UnityEngine.AI;

public class RandomAndAttack : MonoBehaviour
{
    public float detectionRange = 10f;           // Distance à laquelle le joueur est détecté
    public GameObject player;                   // Référence au joueur
    public GameObject projectilePrefab;         // Préfab du projectile
    public float projectileSpeed = 20f;         // Vitesse du projectile
    public float fireRate = 1f;                 // Temps entre chaque tir
    public float randomMoveInterval = 3f;       // Temps entre chaque déplacement aléatoire

    private NavMeshAgent agent;
    private float lastFireTime;
    private float lastMoveTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lastFireTime = Time.time;
        lastMoveTime = Time.time;
    }

    void Update()
    {
        // Vérifier si le joueur est à portée
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            FacePlayer();
            ShootPlayer();
        }
        else
        {
            // Déplacement aléatoire si le joueur n'est pas à portée
            if (Time.time - lastMoveTime > randomMoveInterval)
            {
                MoveToRandomPosition();
                lastMoveTime = Time.time;
            }
        }
    }

    void FacePlayer()
    {
        // Tourner vers le joueur
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ShootPlayer()
    {
        // Vérifier le temps entre les tirs
        if (Time.time - lastFireTime >= fireRate)
        {
            // Instancier un projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Appliquer une vitesse au projectile pour qu'il se dirige vers le joueur
                Vector3 direction = (player.transform.position - transform.position).normalized;
                rb.linearVelocity = direction * projectileSpeed;
            }
            else
            {
                Debug.LogWarning("Le projectile n'a pas de Rigidbody !");
            }

            lastFireTime = Time.time;
        }
    }

    void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 100f; // Génère une direction aléatoire dans un rayon de 100 unités
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 100f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
