using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float range = 3f; // Distance maximale pour interagir
    private WeaponPickup currentWeapon;

    void Update()
    {
        CheckForWeapon();
    }

    private void CheckForWeapon()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            WeaponPickup weapon = hit.collider.GetComponent<WeaponPickup>();
            if (weapon != null)
            {
                currentWeapon = weapon;
                return;
            }
        }

        currentWeapon = null;
    }
}