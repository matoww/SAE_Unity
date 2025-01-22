using UnityEngine;
using UnityEngine.AI;

public class IA_FonceSurJoueur : MonoBehaviour
{
    public Transform joueur; 
    private NavMeshAgent agent; 
    private bool joueurDansZone = false; 

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();

        if (joueur == null)
        {
            Debug.LogError("Le joueur n'est pas assigné!");
        }
    }

    void Update()
    {
       
        if (joueurDansZone)
        {
            agent.SetDestination(joueur.position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogError("Le joueur n'est pas rentré!");
            joueurDansZone = true;
        }
    }

 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joueurDansZone = false;
            agent.ResetPath(); 
        }
    }
}