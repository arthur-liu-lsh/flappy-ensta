using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdJump : MonoBehaviour
{
    public float jumpVelocity;

    public AudioClip jumpSound;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {
            rb.velocity = new Vector3(0f,jumpVelocity,0f);
            AudioSource.PlayClipAtPoint(jumpSound, this.gameObject.transform.position);
        }
    }
}
