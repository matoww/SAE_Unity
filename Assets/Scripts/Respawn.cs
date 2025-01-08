using UnityEngine;

public class Respawn
{
    public static void respawnGameObject(GameObject gameObject, Vector3 respawnPoint)
    {
        gameObject.GetComponent<Transform>().position = respawnPoint;
    }
}
