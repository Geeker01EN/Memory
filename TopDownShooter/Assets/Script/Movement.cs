using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //Movement Variable
    [SerializeField] float speed;
    [SerializeField] LayerMask floor;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    RaycastHit hit;

    void FixedUpdate()
    {
        //Player Follow Mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, floor)) {
            int quadrant;

            if (transform.position.z > hit.point.z) quadrant = -1;
            else quadrant = 1;

            transform.rotation = Quaternion.Euler(0, quadrant * Mathf.Acos((transform.position.x - hit.point.x) / (Mathf.Sqrt(Mathf.Pow((transform.position.x - hit.point.x), 2) + Mathf.Pow((transform.position.z - hit.point.z), 2)))) * Mathf.Rad2Deg - 90, 0);
        }

        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);
    }
}
