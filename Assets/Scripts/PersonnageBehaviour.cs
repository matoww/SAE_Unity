using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PersonnageBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int nbVie = 3;
    Vector3 respawn;
    float timerInvicibility = 0;
    public GameObject ecranDeDefaite;
    float vitesseY = 0;
    Animator animator;
    int isWalkingHash;
    public Mouvement mouvement;
    private float originalSpeed;
    private float originalJumpForce;
    private float maxSpeed;
    private float maxJumpForce;
    private float minSpeed;
    private float minJumpForce;
    private Coroutine currentBonusCoroutine;
    private Coroutine currentMalusCoroutine;
    private GameObject[] coeurs;


    void Start()
    {
        /*animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");*/
        ecranDeDefaite.SetActive(false);
        originalSpeed = mouvement.Speed;
        originalJumpForce = mouvement.JumpForce;
        maxJumpForce = mouvement.MaxJumpForce;
        maxSpeed = mouvement.MaxSpeed;
        minSpeed = mouvement.MinSpeed;
        minJumpForce = mouvement.MinJumpForce;
        respawn = this.transform.position;
        coeurs = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            coeurs[i] = GameObject.Find("Coeur" + (i + 1).ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //bool isWalking = animator.GetBool(isWalkingHash);
        bool keyZInput = Input.GetKey(KeyCode.Z);
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
    private IEnumerator TemporaryBonusEffect()
    {

        if (currentBonusCoroutine != null)
        {
            StopCoroutine(currentBonusCoroutine);

        }

        if (mouvement.Speed * mouvement.BonusEffect < maxSpeed && mouvement.JumpForce * mouvement.BonusEffect < maxJumpForce && currentMalusCoroutine == null)
        {
            mouvement.Speed *= mouvement.BonusEffect;
            mouvement.JumpForce *= mouvement.BonusEffect;

            yield return new WaitForSeconds(3);

            mouvement.Speed = originalSpeed;
            mouvement.JumpForce = originalJumpForce;
        }
        else if (currentMalusCoroutine == null)
        {
            {

                mouvement.Speed = maxSpeed;
                mouvement.JumpForce = maxJumpForce;

                yield return new WaitForSeconds(3);

            }
            if (currentMalusCoroutine != null)
            {
                StopCoroutine(currentMalusCoroutine);
            }
            ResetToOriginalValues();
        }
    }

    private void ResetToOriginalValues()
    {
        mouvement.Speed = originalSpeed;
        mouvement.JumpForce = originalJumpForce;

    }
    public void ActivateBonus()
    {

        currentBonusCoroutine = StartCoroutine(TemporaryBonusEffect());
    }
    private IEnumerator TemporaryMalusEffect() {

        if (currentMalusCoroutine != null)
        {
            StopCoroutine(currentMalusCoroutine);

        }
        if (mouvement.Speed * mouvement.MalusEffect > minSpeed && mouvement.JumpForce * mouvement.MalusEffect > maxJumpForce && currentBonusCoroutine == null)
        {
            mouvement.Speed *= mouvement.MalusEffect;
            mouvement.JumpForce *= mouvement.MalusEffect;

            yield return new WaitForSeconds(3);

            mouvement.Speed = originalSpeed;
            mouvement.JumpForce = originalJumpForce;
        }
        else if (currentBonusCoroutine == null)
        {
            mouvement.Speed = minSpeed;
            mouvement.JumpForce = minJumpForce;

            yield return new WaitForSeconds(3);
        }

        if (currentBonusCoroutine != null)
        {
            StopCoroutine(currentBonusCoroutine);
        }
        ResetToOriginalValues();
    }
    public void ActivateMalus()
    {

        currentMalusCoroutine = StartCoroutine(TemporaryMalusEffect());
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
        if (nbVie == 0)
        {
            ecranDeDefaite.SetActive(true);
            GameObject respawnButton = GameObject.Find("BoutonRespawn");
            respawnButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Respawn.respawnGameObject(gameObject, respawn);
                for (int i = 0; i < 3 - nbVie; i++)
                {
                    coeurs[i + nbVie].SetActive(true);
                }
                nbVie = 3;
                ecranDeDefaite.SetActive(false);
            });
            Debug.Log("onclickActivé");
        }
    }

    public void OnHeadTouched(Collision collision)
    {
        if (collision.gameObject.tag == "Ennemie" && timerInvicibility <= 0)
        {
            GameObject.Find("Coeur" + nbVie.ToString()).SetActive(false);
            nbVie -= 1;
            timerInvicibility = 2;
            ActivateMalus();
            Debug.Log(nbVie);
        }
        if (vitesseY < -10)
        {
            GameObject.Find("Coeur" + nbVie.ToString()).SetActive(false);
            nbVie -= 1;
            timerInvicibility = 2;
            ActivateMalus();
            vitesseY = 0;
        }
    }

    public void OnBottomTouched(Collision collision, GameObject partieEnnemieTouche)
    {
        if(collision.gameObject.tag =="Ennemie" && partieEnnemieTouche.transform.parent.name == "Tete")
        {
            collision.gameObject.SetActive(false);
            ActivateBonus();
        }
        else if (collision.gameObject.tag == "Ennemie" && timerInvicibility <= 0)
        {
            GameObject.Find("Coeur" + nbVie.ToString()).SetActive(false);
            nbVie -= 1;
            timerInvicibility = 2;
            ActivateMalus();
        }
        if (vitesseY < -10)
        {
            GameObject.Find("Coeur" + nbVie.ToString()).SetActive(false);
            nbVie -= 1;
            timerInvicibility = 2;
           ActivateMalus();
            vitesseY = 0;
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
    public void OnBodyTouch(Collision collision)
    {
        if (collision.gameObject.tag == "Ennemie" && timerInvicibility <= 0)
        {
            GameObject.Find("Coeur" + nbVie.ToString()).SetActive(false);
            nbVie -= 1;
            timerInvicibility = 2;
            StartCoroutine(TemporaryMalusEffect());
        }
        if (vitesseY < -10)
        {
            GameObject.Find("Coeur" + nbVie.ToString()).SetActive(false);
            nbVie -= 1;
            timerInvicibility = 2;
            vitesseY = 0;
            StartCoroutine(TemporaryMalusEffect());
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
                for (int i = 0; i < 3 - nbVie; i++)
                {
                    coeurs[i + nbVie].SetActive(true);
                }
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
