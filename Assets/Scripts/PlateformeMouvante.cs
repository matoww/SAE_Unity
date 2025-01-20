using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float velocity;
    Vector3 positionBase;
    public Vector3 positionArrive;
    Boolean aller = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionBase = transform.position;
        if (positionArrive.y == positionBase.y)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        }
        if (positionArrive.z == positionBase.z)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        }
        if (positionArrive.x == positionBase.x)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aller)
        {
            float distanceY = this.positionArrive.y - this.transform.position.y;
            float distanceZ = this.positionArrive.z - this.transform.position.z;
            float distanceX = this.positionArrive.x - this.transform.position.x;
            if (distanceY + distanceZ + distanceX < 0.03 && distanceY + distanceZ + distanceX > -0.03)
            {
                transform.position = positionArrive;
                aller = false;
            }
            else
            {
                Vector3 movement = new Vector3(distanceX, distanceY, distanceZ) * velocity * Time.deltaTime;
                transform.Translate(movement);
            }
        }
        else
        {
            float distanceY = this.positionBase.y - this.transform.position.y;
            float distanceZ = this.positionBase.z - this.transform.position.z;
            float distanceX = this.positionBase.x - this.transform.position.x;
            if (distanceY + distanceZ + distanceX < 0.0003 && distanceY + distanceZ + distanceX > -0.03)
            {
                transform.position = positionBase;
                aller = true;  
            }
            else
            {
                Vector3 movement = new Vector3(distanceX, distanceY, distanceZ) * velocity * Time.deltaTime;
                transform.Translate(movement);
            }
        }
    }
}
