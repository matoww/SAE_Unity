using UnityEngine;

public class Respawn
{
    private GameObject GameObject;
    private Vector3 respawnPoint;
    
    public Respawn(GameObject gameObject,Vector3 respawnPoint)
    {
        this.GameObject = gameObject;
        this.respawnPoint = respawnPoint;
    }

    public void respawnGameObject()
    {
        this.GameObject.GetComponent<Transform>().position = respawnPoint;
        Debug.Log("respawn");
    }
}
