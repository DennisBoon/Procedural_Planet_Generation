using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float rotateTime = 5.0f;

    void Update()
    {
        transform.Rotate(0, (360 / (rotateTime * 60 * 60)) * Time.deltaTime, 0, Space.Self);
    }
}
