using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform _transform;
    private BoxCollider _collider;
    void Start()
    {
        _transform = GetComponent<Transform>();
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // if collider tag bullet then destroy this 
        if (_collider.tag == "bullet")
        {
            Destroy(this);
        }
    }
}
