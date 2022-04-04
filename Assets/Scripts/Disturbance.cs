using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disturbance : MonoBehaviour
{
    public float size;
    public float decrease;
    SphereCollider coll;

    void Start()
    {
        coll = GetComponent<SphereCollider>();
    }

    void Update()
    {
        size -= decrease * Time.deltaTime;
        coll.radius = size;

        if (size < 0.05f)
        {
            FindObjectOfType<DisturbanceManager>().disturbances.Remove(this);
            Destroy(gameObject);
        }
    }
}
