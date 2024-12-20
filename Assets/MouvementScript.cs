using Unity.VisualScripting;
using UnityEngine;

public class MouvementScript
{
    private GameObject GameObject;
    private Vector3 vector3;
    private Vector4 vector4;
    public int vitesse;
    public MouvementScript(GameObject obj,Vector3 vector3,int vitesse)
    {
        this.GameObject = obj;
        this.vector3 = vector3;
        this.vitesse = vitesse;
    }
    public MouvementScript(GameObject obj, Vector4 vector4)
    {
        this.GameObject = obj;
        this.vector4 = vector4;
    }

    public void Mouvement()
    {
        Rigidbody rb = this.GameObject.GetComponent<Rigidbody>();
        rb.AddForce(vector3*vitesse,ForceMode.Force);
    }

}
