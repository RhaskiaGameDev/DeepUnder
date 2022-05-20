using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDunk : MonoBehaviour
{
    public float speed;
    public float sizeFavor;
    public float minDist;
    public Disturbance[] disturbs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (closestDist().Item1 != null)
        {
            transform.LookAt(closestDist().Item1.transform);
            transform.position += speed * transform.forward * Time.deltaTime * closestDist().Item2;
        }



    }

    (Disturbance, float) closestDist()
    {
        var distance = 0f;
        Disturbance closest = null;
        disturbs = FindObjectsOfType<Disturbance>();

        foreach (var dist in disturbs)
        {
            var totalDist = (dist.size * sizeFavor) + (1 / Vector3.Distance(transform.position, dist.transform.position));
            if (totalDist > distance)
            {
                distance = totalDist;
                closest = dist;
            }
        }

        return (closest, distance);
    }
}
