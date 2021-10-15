using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreponRotater : MonoBehaviour
{

    public float speed;
    
    private float angle = 0;

    Transform tr;

    void Start()
    {
        tr = transform;
    }

    void Update()
    {
        angle += Time.deltaTime * speed;
        tr.rotation = Quaternion.Euler(0,0,angle);
    }
}
