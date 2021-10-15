using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float movementSpeed;
    public float despawnPosition;

    private Rigidbody2D rb;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = transform;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(-movementSpeed, 0f, 0f);
    }


    void FixedUpdate()
    {
        if (tr.position.x < despawnPosition) {
            Destroy(gameObject);
        }
    }
}
