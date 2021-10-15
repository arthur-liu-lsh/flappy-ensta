using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollision : MonoBehaviour
{
    GameManager gm;
    
    public AudioClip hitSound;

    public float verticalBounds;
    private Transform tr;



    void Start()
    {
        tr = transform;
        gm = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D()
    {
        AudioSource.PlayClipAtPoint(hitSound, this.gameObject.transform.position);
        gm.StopTime();
    }

    void FixedUpdate()
    {
        if (tr.position.y < -verticalBounds || tr.position.y > verticalBounds) { //Out of vertical bounds
            AudioSource.PlayClipAtPoint(hitSound, this.gameObject.transform.position);
            gm.StopTime();
        } 
    }
}
