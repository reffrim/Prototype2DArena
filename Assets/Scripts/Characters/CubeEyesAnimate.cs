using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEyesAnimate : MonoBehaviour
{
    GameObject ShereReference;

    void Start()
    {
        ShereReference = GameObject.Find("Sphere");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sphereRefPos = ShereReference.transform.position;

        Vector3 lookPos = sphereRefPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 145f;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
