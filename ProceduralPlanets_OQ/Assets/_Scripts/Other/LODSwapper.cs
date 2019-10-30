using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODSwapper : MonoBehaviour
{
    public List<GameObject> LODObjects = new List<GameObject>();
    public int[] LODRanges;
    public GameObject player;

    private void Update()
    {
        if (LODObjects != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < LODRanges[0])
            {
                Swap(LODObjects[LODObjects.Count - 1]);
            }
            else if (Vector3.Distance(transform.position, player.transform.position) > LODRanges[0] &&
                     Vector3.Distance(transform.position, player.transform.position) < LODRanges[1])
            {
                Swap(LODObjects[LODObjects.Count - 2]);
            }
            else if (Vector3.Distance(transform.position, player.transform.position) > LODRanges[1] &&
                     Vector3.Distance(transform.position, player.transform.position) < LODRanges[2])
            {
                Swap(LODObjects[LODObjects.Count - 3]);
            }
        }
    }

    void Swap(GameObject obj)
    {
        foreach (GameObject gObj in LODObjects)
        {
            gObj.SetActive(false);
        }

        obj.SetActive(true);
    }
}