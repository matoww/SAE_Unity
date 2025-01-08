using UnityEngine;

public class EnnemieBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ZoneDeMort")
        {
            this.gameObject.SetActive(false);
        }
    }
}
