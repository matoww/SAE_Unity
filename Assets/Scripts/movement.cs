using UnityEngine;



public class Mouvement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    private bool isGrounded = true;
    public float bonusEffect = 2.0f; // Facteur multiplicateur pour les bonus
    public float malusEffect = 0.5f; // Facteur multiplicateur pour les malus
    public float headBounceForce = 50.0f; // Force de rebond pour les objets "Head"
    public int banane = 0;
    public Point point; 

    // Paramètres pour le contrôle de la caméra
    public float mouseSensitivity = 100.0f;
    private float rotationX = 0.0f;

 

    void Update()
    {
        // Mouvement du joueur
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);
        

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }


        // Rotation avec la souris
        RotateWithMouse();
    }

    private void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation horizontale (gauche/droite)
        transform.Rotate(Vector3.up * mouseX);

        // Rotation verticale (haut/bas), limitée pour éviter de tourner complètement la tête
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("plateformeMouvante"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("head"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * headBounceForce, ForceMode.Impulse);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
      

        if (other.CompareTag("bonus"))
        {
            speed *= bonusEffect;
            jumpForce *= bonusEffect;
            Destroy(other.gameObject); // Supprime l'objet après l'effet
        }

        if (other.CompareTag("malus"))
        {
            speed *= malusEffect;
            jumpForce *= malusEffect;
            Destroy(other.gameObject); // Supprime l'objet après l'effet
        }

        if (other.CompareTag("banane"))
        {
          
            banane += 1;
            point.setBanane(banane);
            Debug.Log(banane);
            Destroy(other.gameObject); // Supprime l'objet après l'effet


        }
    }
}
