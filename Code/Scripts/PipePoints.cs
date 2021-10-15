using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePoints : MonoBehaviour
{
    public AudioClip pointSound;

    GameManager gm;
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        gm = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (tr.position.x < -3f) {
            gm.AddPoint();
            AudioSource.PlayClipAtPoint(pointSound, this.gameObject.transform.position);
            this.enabled = false;
        }
    }
}
