using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PersonnageBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int nbVie=3;
    public Vector3 respawn;
    float timerInvicibility = 0;
    public GameObject ecranDeDefaite;
    float vitesseY = 0;
    Animator animator;
    int isWalkingHash;
    void Start()
    {
        /*animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");*/
        ecranDeDefaite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //bool isWalking = animator.GetBool(isWalkingHash);
        bool keyZInput=Input.GetKey(KeyCode.Z);
        if (timerInvicibility > 0)
        {
            timerInvicibility -= Time.deltaTime;
        }
        if (GetComponent<Rigidbody>().linearVelocity.y < vitesseY) {
            vitesseY = GetComponent<Rigidbody>().linearVelocity.y;
        }
        /*
        if (keyZInput && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        if (!keyZInput && isWalking)
        { 
            animator.SetBool("isWalking",false);
        }*/
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

        if (collision.gameObject.tag == "plateformeMouvante")
        {
            if (transform.parent == null)
            {
                GameObject vide = new GameObject("Vide");
                vide.transform.SetParent(collision.gameObject.transform);
                Vector3 vector3 = transform.localScale;
                transform.SetParent(vide.transform);
                transform.localScale = vector3;
            }
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
        if (nbVie == 0)
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
        if (nbVie == 0)
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
        if (nbVie == 0)
        {
            ecranDeDefaite.SetActive(true);
            GameObject respawnButton = GameObject.Find("BoutonRespawn");
            respawnButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Respawn.respawnGameObject(gameObject, respawn);
                nbVie = 3;
                ecranDeDefaite.SetActive(false);
            });
            Debug.Log("onclickActivé");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ZoneDeMort")
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
        if(other.gameObject.tag == "SpawnPoint")
        {
            respawn = other.gameObject.transform.position;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "plateformeMouvante")
        {
            Transform tempParent=transform.parent;
            if (tempParent != null)
            {
                transform.SetParent(null);
                Destroy(tempParent.gameObject);
            }
        }
    }
}
