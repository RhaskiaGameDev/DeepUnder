using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject foundation;
    public GameObject[] structures;
    public GameObject[] modules;
    public GameObject hologram;

    public int current;
    public int rotation;

    public Vector3[] rotations;

    void Start()
    {

    }

    void Update()
    {
        ManageBuilding();
        if (Input.GetKeyDown(KeyCode.R)) rotation = (rotation + 1) % 4;
    }

    void ManageBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!(Physics.Raycast(ray, out hit) && hit.collider.tag == "Grid"))
            return;

        var pos = hit.collider.transform.position + Vector3.up;
        var rot = Quaternion.Euler(rotations[rotation]);

        if (hologram != null)
        {
            hologram.transform.position = pos;
            hologram.transform.rotation = rot;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(structures[current], pos, rot);
            hologram.transform.position = Vector3.one * 5678278578f;
        }
    }

}
