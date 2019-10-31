using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BeltSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] cubePrefabs;
    public int cubeDensity;
    public int seed;
    public float innerRadius;
    public float outerRadius;
    public float height;
    public bool rotatingClockwise;

    [Header("Asteroid Settings")]
    public float minOrbitSpeed;
    public float maxOrbitSpeed;
    public float minRotationSpeed;
    public float maxRotationSpeed;

    private Vector3 localPosition;
    private Vector3 worldOffset;
    private Vector3 worldPosition;
    private float randomRadius;
    private float randomRadian;
    private float x;
    private float y;
    private float z;

    //================================================
    // Random Point on a Circle given only the Angle.
    // x = cx + r * cos(a)
    // y = cy + r* sin(a)
    //================================================
    private void Start()
    {
        Random.InitState(seed);

        for (int i = 0; i < cubeDensity; i++)
        {
            do
            {
                randomRadius = Random.Range(innerRadius, outerRadius);
                randomRadian = Random.Range(0, (2 * Mathf.PI));

                y = Random.Range(-(height / 2), (height / 2));
                x = randomRadius * Mathf.Cos(randomRadian);
                z = randomRadius * Mathf.Sin(randomRadian);
            }
            while (float.IsNaN(z) && float.IsNaN(x));

            localPosition = new Vector3(x, y, z);
            worldOffset = transform.rotation * localPosition;
            worldPosition = transform.position + worldOffset;

            GameObject _asteroid = Instantiate(cubePrefabs[Random.Range(0, cubePrefabs.Length)], worldPosition, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            //_asteroid.AddComponent<BeltObject>().SetupBeltObject(Random.Range(minOrbitSpeed, maxOrbitSpeed), Random.Range(minRotationSpeed, maxRotationSpeed), gameObject, rotatingClockwise);
            _asteroid.transform.SetParent(transform);
        }

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        List<MeshFilter> meshFilter = new List<MeshFilter>();

        for (int j = 1; j < meshFilters.Length; j++)
        {
            meshFilter.Add(meshFilters[j]);
            CombineInstance[] combine = new CombineInstance[meshFilter.Count];
            int np = 0;

            while (np < meshFilter.Count)
            {
                combine[np].mesh = meshFilter[np].sharedMesh;
                combine[np].transform = meshFilter[np].transform.localToWorldMatrix;
                meshFilter[np].gameObject.SetActive(false);
                np++;
            }
        }

        //transform.GetComponent<MeshFilter>().mesh = new Mesh();
        //transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        //transform.gameObject.SetActive(true);
    }
}