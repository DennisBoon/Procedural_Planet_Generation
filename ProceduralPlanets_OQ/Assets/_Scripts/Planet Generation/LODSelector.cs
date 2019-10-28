using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODSelector : MonoBehaviour
{
    public List<GameObject> LODLevelObjects = new List<GameObject>();
    public GameObject player;
    [SerializeField]
    private GameObject activeLOD;
    public int[] LODDistances;

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > LODDistances[0])
        {
            SwapLOD(LODLevelObjects[2]);
        }
        else if (Vector3.Distance(player.transform.position, transform.position) < LODDistances[0] &&
                Vector3.Distance(player.transform.position, transform.position) > LODDistances[1])
        {
            SwapLOD(LODLevelObjects[1]);
        }
        else if (Vector3.Distance(player.transform.position, transform.position) < LODDistances[1] &&
                Vector3.Distance(player.transform.position, transform.position) > LODDistances[2])
        {
            SwapLOD(LODLevelObjects[0]);
        }
    }

    void SwapLOD(GameObject obj)
    {
        foreach (GameObject i in LODLevelObjects)
        {
            i.SetActive(false);
        }
        obj.SetActive(true);
        activeLOD = obj;
    }
}
