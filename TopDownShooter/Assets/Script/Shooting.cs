using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject gun;
    [SerializeField] float speed;

    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            GameObject newProjectile = Instantiate(projectile, gun.transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody>().velocity = gun.transform.forward * speed;
        }
    }
}
