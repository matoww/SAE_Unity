using UnityEngine;

public class Tourne : MonoBehaviour
{
    public Vector3 tourne; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(tourne);
    }
}
