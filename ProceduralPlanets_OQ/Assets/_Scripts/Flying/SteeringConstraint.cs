using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringConstraint : MonoBehaviour
{
    public GameObject StangVert;
    public GameObject StangHor;

    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;

    public float minX = -0.015f;
    public float maxX = 0.015f;
    public float minZ = -0.015f;
    public float maxZ = 0.015f;

    private float lockPos = 0.0f;

    void Update()
    {
        if (transform.localPosition.x < minX)
        {
            transform.localPosition = new Vector3(minX, lockPos, transform.localPosition.z);
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else transform.localPosition = new Vector3(transform.localPosition.x, lockPos, transform.localPosition.z);

        if (transform.localPosition.x > maxX)
        {
            transform.localPosition = new Vector3(maxX, lockPos, transform.localPosition.z);
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else transform.localPosition = new Vector3(transform.localPosition.x, lockPos, transform.localPosition.z);

        if (transform.localPosition.z < minZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, lockPos, minZ);
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else transform.localPosition = new Vector3(transform.localPosition.x, lockPos, transform.localPosition.z);

        if (transform.localPosition.z > maxZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, lockPos, maxZ);
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else transform.localPosition = new Vector3(transform.localPosition.x, lockPos, transform.localPosition.z);

        StangVert.transform.localPosition = new Vector3(transform.localPosition.x, 0.0f, 0.0f);
        StangHor.transform.localPosition = new Vector3(0.0f, 0.0f, transform.localPosition.z);

        if (transform.localPosition.z > 0.003f)
        {
            upArrow.GetComponent<Animator>().Play("BlinkingArrow");
        }
        else
        {
            upArrow.GetComponent<Animator>().Play("BlinkingArrow_Idle");
        }

        if (transform.localPosition.z < -0.003f)
        {
            downArrow.GetComponent<Animator>().Play("BlinkingArrow");
        }
        else
        {
            downArrow.GetComponent<Animator>().Play("BlinkingArrow_Idle");
        }

        if (transform.localPosition.x > 0.003f)
        {
            rightArrow.GetComponent<Animator>().Play("BlinkingArrow");
        }
        else
        {
            rightArrow.GetComponent<Animator>().Play("BlinkingArrow_Idle");
        }

        if (transform.localPosition.x < -0.003f)
        {
            leftArrow.GetComponent<Animator>().Play("BlinkingArrow");
        }
        else
        {
            leftArrow.GetComponent<Animator>().Play("BlinkingArrow_Idle");
        }
    }
}
