using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODSelector : MonoBehaviour
{
    public List<GameObject> LODLevelObjects = new List<GameObject>();
    GameObject[][] childGameObjects;
    LODGroup group;

    private void Start()
    {
        group = GetComponent<LODGroup>();
    }

    public void CreateLODGroup()
    {
        Debug.Log("CREATING LOD GROUP WITH " + LODLevelObjects.Count + " LOD LEVELS");
        childGameObjects = new GameObject[LODLevelObjects.Count][];
        MeshRenderer[] allDrawables = new MeshRenderer[6];
        LOD[] lods = new LOD[LODLevelObjects.Count];
        int lodLevel = LODLevelObjects.Count - 1, childGameObjectArray = 0;

        for (int i = 0; i < LODLevelObjects.Count; i++)
        {
            childGameObjects[childGameObjectArray] = new GameObject[6];

            for (int j = 0; j < allDrawables.Length; j++)
            {
                childGameObjects[childGameObjectArray][j] = LODLevelObjects[childGameObjectArray].transform.GetChild(0).gameObject;
                GameObject go = childGameObjects[childGameObjectArray][j];
                go.transform.parent = gameObject.transform;
                allDrawables[j] = go.GetComponent<MeshRenderer>();
            }
            lods[lodLevel].renderers = allDrawables;
            lodLevel--; 
            childGameObjectArray++;
        }

        group.SetLODs(lods);
        group.RecalculateBounds();
    }
}