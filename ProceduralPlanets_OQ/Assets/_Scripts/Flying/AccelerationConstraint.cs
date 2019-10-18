using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationConstraint : MonoBehaviour
{
    private float lockPos = 0.0f;

    public float minZ = 0.0f;
    public float maxZ = 0.127f;

    void Update()
    {
        if (transform.localPosition.z < minZ)
        {
            transform.localPosition = new Vector3(lockPos, lockPos, minZ);
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else transform.localPosition = new Vector3(lockPos, lockPos, transform.localPosition.z);

        if (transform.localPosition.z > maxZ)
        {
            transform.localPosition = new Vector3(lockPos, lockPos, maxZ);
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else transform.localPosition = new Vector3(lockPos, lockPos, transform.localPosition.z);
    }
}
