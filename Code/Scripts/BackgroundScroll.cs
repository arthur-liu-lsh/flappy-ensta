using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    public float speed;

    private Transform tr;

    void Start()
    {
        tr = transform;
    }

    void Update()
    {
        tr.position = new Vector3(tr.position.x - speed*Time.deltaTime, 0f, 0f);
        if (tr.position.x < -10.24f) {
            tr.position = new Vector3(10.24f, 0f, 0f);
        }
    }
}
