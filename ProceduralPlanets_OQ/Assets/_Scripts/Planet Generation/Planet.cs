using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int[] resolutions;
    [SerializeField]
    private List<GameObject> LODLevelObjects = new List<GameObject>();
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColourGenerator colourGenerator = new ColourGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    private void Start()
    {
        LODLevelObjects.Clear();
        GeneratePlanet();
    }

    void Initialize()
    {
        int resolution = 0;
        for (int j = 0; j < resolutions.Length - 1; j++)
        {
            GameObject obj;
            shapeGenerator.UpdateSettings(shapeSettings);
            colourGenerator.UpdateSettings(colourSettings);

            if (meshFilters == null || meshFilters.Length == 0)
            {
                meshFilters = new MeshFilter[6];
            }
            terrainFaces = new TerrainFace[6];

            Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

            for (int i = 0; i < 6; i++)
            {
                if (meshFilters[i] == null)
                {
                    GameObject meshObj = new GameObject("mesh");
                    meshObj.transform.parent = transform;

                    meshObj.AddComponent<MeshRenderer>();
                    meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                    meshFilters[i].mesh = new Mesh();
                }
                meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;

                terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].mesh, resolutions[resolution], directions[i]);
                bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
                meshFilters[i].gameObject.SetActive(renderFace);            
            }
            resolution++;
            obj = Instantiate(this.gameObject, transform.position, transform.rotation);
            DestroyImmediate(obj.GetComponent<Planet>());
            LODLevelObjects.Add(obj);
        }
        LODLevelObjects.Add(this.gameObject);
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    //public void OnShapeSettingsUpdated()
    //{
    //    if (autoUpdate)
    //    {
    //        Initialize();
    //        GenerateMesh();
    //    }
    //}

    //public void OnColourSettingsUpdated()
    //{
    //    if (autoUpdate)
    //    {
    //        Initialize();
    //        GenerateColours();
    //    }
    //}

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }

        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colourGenerator.UpdateColours();
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].UpdateUVs(colourGenerator);
            }
        }
    }
}
