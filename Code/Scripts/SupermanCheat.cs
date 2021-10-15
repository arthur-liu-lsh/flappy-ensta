using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupermanCheat : MonoBehaviour
{

    public float speed;

    Rigidbody2D rb;
    BirdJump jumpScript;
    Transform tr;

    bool active = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpScript = GetComponent<BirdJump>();
        tr = transform;
    }

    void Update()
    {
        if (active) {
            if (Input.GetKeyDown(KeyCode.K)) {
                active = false;
                Debug.Log("Superman mode off");
                rb.gravityScale = 1.5f;
            }

            if (Input.GetKey(KeyCode.UpArrow)) {
                transform.position = transform.position + speed * Vector3.up * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow)) {
                transform.position = transform.position + speed * Vector3.down * Time.deltaTime;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.K)) {
                // Remove score POST
                active = true;
                Debug.Log("Superman mode on");
                rb.gravityScale = 0f;
                rb.velocity = Vector3.zero;
            }
        }
    }
}
