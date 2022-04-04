using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbanceManager : MonoBehaviour
{
    public List<Disturbance> disturbances;
    public float distance;
    public GameObject prefab;

    void Start()
    {

    }

    void Update()
    {

    }

    public void CreateDisturbance(float size, float decrease, Vector3 position)
    {
        if (GetClosest(position) != null)
        {
            GetClosest(position).size += size;
        }
        else
        {
            Disturbance dist = Instantiate(prefab, position, Quaternion.identity).GetComponent<Disturbance>();
            disturbances.Add(dist);

            dist.transform.parent = transform;
            dist.size = size;
            dist.decrease = decrease;
        }


    }

    Disturbance GetClosest(Vector3 pos)
    {
        foreach (var dist in disturbances)
        {
            if (Vector3.Distance(pos, dist.transform.position) < distance)
                return dist;
        }

        return null;
    }
}
