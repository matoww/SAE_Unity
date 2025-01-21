using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float velocity;
    Vector3 positionBase;
    public Vector3 positionArrive;
    Boolean aller = true;
    public float approximation;
    public Vector3 rotationVoulu;
    Vector3 rotationBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionBase = transform.position;
        rotationBase = transform.localRotation.eulerAngles;
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
            if (distanceY + distanceZ + distanceX < approximation && distanceY + distanceZ + distanceX > -approximation)
            {
                transform.position = positionArrive;
                aller = false;
            }
            else
            {
                Vector3 movement = new Vector3(distanceX, distanceY, distanceZ) * velocity * Time.deltaTime;
                //à améliorer avant utilisation
                Quaternion rotation = Quaternion.Slerp(this.transform.localRotation,Quaternion.Euler(rotationVoulu),Time.deltaTime*velocity);
                transform.rotation = rotation;

                transform.Translate(movement);
            }
        }
        else
        {
            float distanceY = this.positionBase.y - this.transform.position.y;
            float distanceZ = this.positionBase.z - this.transform.position.z;
            float distanceX = this.positionBase.x - this.transform.position.x;
            if (distanceY + distanceZ + distanceX < approximation && distanceY + distanceZ + distanceX > -approximation)
            {
                transform.position = positionBase;
                aller = true;  
            }
            else
            {
                Vector3 movement = new Vector3(distanceX, distanceY, distanceZ) * velocity * Time.deltaTime;
                //à améliorer avant utilisation
                Quaternion rotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(rotationBase), Time.deltaTime*velocity);
                transform.rotation = rotation;

                transform.Translate(movement);
            }
        }
    }
}
