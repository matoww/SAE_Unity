using UnityEngine;

public class PersonnageBehaviour : MonoBehaviour
{  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)){
            MouvementScript mouvement = new MouvementScript(this.gameObject,Vector3.forward,5);
            mouvement.Mouvement();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MouvementScript mouvement = new MouvementScript(this.gameObject, Vector3.left,5);
            mouvement.Mouvement();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MouvementScript mouvement = new MouvementScript(this.gameObject, Vector3.back,5);
            mouvement.Mouvement();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MouvementScript mouvement = new MouvementScript(this.gameObject, Vector3.right,5);
            mouvement.Mouvement();
        }
    }
}
