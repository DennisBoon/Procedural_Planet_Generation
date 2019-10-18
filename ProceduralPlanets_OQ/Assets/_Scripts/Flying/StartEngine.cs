using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEngine : MonoBehaviour
{
    public static bool EngineRunning = false;
    private Animator anim;
    public GameObject throttle;
    public GameObject steer;
    public Material[] engineOnMaterials;
    public Material[] engineOffMaterials;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (EngineRunning == false)
            {
                EngineRunning = true;
                Debug.Log("ENGINE RUNNING: " + EngineRunning);
                StartCoroutine(PressButton());
                gameObject.GetComponent<Renderer>().materials = engineOnMaterials;
            }
            else if (EngineRunning == true)
            {
                EngineRunning = false;
                Debug.Log("ENGINE RUNNING: " + EngineRunning);
                StartCoroutine(PressButton());
                throttle.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                steer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                gameObject.GetComponent<Renderer>().materials = engineOffMaterials;
            }
        }
    }

    private IEnumerator PressButton()
    {
        anim.Play("StartEngine_Push_V2");
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        anim.Play("StartEngine_Idle_V2");
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
