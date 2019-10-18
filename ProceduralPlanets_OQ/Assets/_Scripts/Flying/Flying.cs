using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public GameObject Plane;
    public GameObject Fs;

    public float speedRoty = 50.0f;
    public float speedPosy = 10.0f;
    public float speed = 1.0f;

    public float maxTiltAngle = 15.0f;
    Vector3 curRot;
    float maxX = 0.099f;
    float minX = -0.099f;

    private float roty = 0.0f;
    private float posy = 0.0f;
    private float forwardSpeed = 0.0f;

    void Update()
    {
        if (StartEngine.EngineRunning == true)
        {
            roty = (roty + (transform.localPosition.x * speedRoty)) * Time.deltaTime;
            posy = (posy + (transform.localPosition.z * speedPosy)) * Time.deltaTime;

            Plane.transform.Rotate(0.0f, roty, 0.0f, Space.World);
            Plane.transform.Translate(0.0f, posy, 0.0f, Space.Self);

            forwardSpeed = (Fs.transform.localPosition.z * speed) * Time.deltaTime;
            Plane.transform.Translate(0.0f, 0.0f, forwardSpeed, Space.Self);
        }
    }
}
