using UnityEngine;
using UnityEngine.UI;

public class PersonnageBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int nbVie=3;
    public Vector3 respawn;
    public float timerInvicibility = 0;
    public GameObject ecranDeDefaite;
    void Start()
    {
        ecranDeDefaite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerInvicibility > 0)
        {
            timerInvicibility -= Time.deltaTime;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Ennemie" && timerInvicibility<=0)
        {
            nbVie -= 1;
            timerInvicibility = 2 ;
            Debug.Log(nbVie);
            if (nbVie == 0)
            {
                ecranDeDefaite.SetActive(true);
                GameObject respawnButton = GameObject.Find("BoutonRespawn");
                respawnButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    new Respawn(gameObject, respawn).respawnGameObject();
                    nbVie = 3;
                    ecranDeDefaite.SetActive(false);
                });
            }
        }
    }
}
