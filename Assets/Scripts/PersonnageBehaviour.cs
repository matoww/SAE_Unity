using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PersonnageBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int nbVie=3;
    public Vector3 respawn;
    public float timerInvicibility = 0;
    public GameObject ecranDeDefaite;
    public float vitesseY = 0;
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
        if (GetComponent<Rigidbody>().linearVelocity.y < vitesseY) {
            vitesseY = GetComponent<Rigidbody>().linearVelocity.y;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        GameObject partieTouche = collision.contacts[0].thisCollider.gameObject;
        GameObject partieEnnemi = collision.contacts[0].otherCollider.gameObject;

        if (partieTouche.transform.parent.name == "Bas")
        {
            OnBottomTouched(collision,partieEnnemi);
        }
        else if (partieTouche.transform.parent.name == "Tete")
        {
            OnHeadTouched(collision);
        }
        else if(partieTouche.transform.parent.name == "Corps")
        {
            OnBodyTouch(collision);
        }
    }

    public void OnHeadTouched(Collision collision)
    {
        if (collision.gameObject.tag == "Ennemie" && timerInvicibility <= 0)
        {
            nbVie -= 1;
            timerInvicibility = 2;
            Debug.Log(nbVie);
        }
        if (vitesseY < -10)
        {
            nbVie -= 1;
            timerInvicibility = 2;
            vitesseY = 0;
        }
        if (nbVie == 0 || collision.gameObject.tag == "ZoneDeMort")
        {
            ecranDeDefaite.SetActive(true);
            GameObject respawnButton = GameObject.Find("BoutonRespawn");
            respawnButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Respawn.respawnGameObject(gameObject, respawn);
                nbVie = 3;
                ecranDeDefaite.SetActive(false);
            });
        }
    }

    public void OnBottomTouched(Collision collision, GameObject partieEnnemieTouche)
    {
        if(collision.gameObject.tag =="Ennemie" && partieEnnemieTouche.transform.parent.name == "Tete")
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Ennemie" && timerInvicibility <= 0)
        {
            nbVie -= 1;
            timerInvicibility = 2;
        }
        if (vitesseY < -10)
        {
            nbVie -= 1;
            timerInvicibility = 2;
            vitesseY = 0;
        }
        if (nbVie == 0 || collision.gameObject.tag == "ZoneDeMort")
        {
            ecranDeDefaite.SetActive(true);
            GameObject respawnButton = GameObject.Find("BoutonRespawn");
            respawnButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Respawn.respawnGameObject(gameObject, respawn);
                nbVie = 3;
                ecranDeDefaite.SetActive(false);
            });
        }
    }
    public void OnBodyTouch(Collision collision)
    {
        if (collision.gameObject.tag == "Ennemie" && timerInvicibility <= 0)
        {
            nbVie -= 1;
            timerInvicibility = 2;
        }
        if (vitesseY < -10)
        {
            nbVie -= 1;
            timerInvicibility = 2;
            vitesseY = 0;
        }
        if (nbVie == 0 || collision.gameObject.tag == "ZoneDeMort")
        {
            ecranDeDefaite.SetActive(true);
            GameObject respawnButton = GameObject.Find("BoutonRespawn");
            respawnButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Respawn.respawnGameObject(gameObject, respawn);
                nbVie = 3;
                ecranDeDefaite.SetActive(false);
            });
        }
    }
}
