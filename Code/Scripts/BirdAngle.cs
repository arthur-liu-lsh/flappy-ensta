using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAngle : MonoBehaviour
{
    public float xSpeed;

    Rigidbody2D rb;
    Transform tr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Rad2Deg * Mathf.Atan(rb.velocity.y/xSpeed);
        rb.rotation = angle;
    }
}
