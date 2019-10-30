using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int[] resolutions;
    //public bool autoUpdate = true;
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
    //LODSelector lodSelector;
    LODSwapper lodSwapper;

    [SerializeField, HideInInspector]
    MeshFilter[][] meshFilters;
    TerrainFace[][] terrainFaces;

    int meshFilterArray = 0;
    int terrainFaceArray = 0;
    int destroyChilds = 0;

    private void Start()
    {
        meshFilters = new MeshFilter[resolutions.Length][];
        terrainFaces = new TerrainFace[resolutions.Length][];
        //lodSelector = transform.parent.GetComponent<LODSelector>();
        //lodSelector.LODLevelObjects.Clear();
        lodSwapper = transform.parent.GetComponent<LODSwapper>();
        lodSwapper.LODObjects.Clear();

        GeneratePlanet();
    }

    void Initialize()
    {
        int resolution = 0;
        for (int j = 0; j < resolutions.Length; j++)
        {
            shapeGenerator.UpdateSettings(shapeSettings);
            colourGenerator.UpdateSettings(colourSettings);

            if (meshFilters[meshFilterArray] == null || meshFilters[meshFilterArray].Length == 0)
            {
                meshFilters[meshFilterArray] = new MeshFilter[6];
            }
            terrainFaces[terrainFaceArray] = new TerrainFace[6];

            Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

            for (int i = 0; i < 6; i++)
            {
                if (meshFilters[meshFilterArray][i] == null)
                {
                    GameObject meshObj = new GameObject("mesh");
                    meshObj.transform.parent = transform;

                    meshObj.AddComponent<MeshRenderer>();
                    meshFilters[meshFilterArray][i] = meshObj.AddComponent<MeshFilter>();
                    meshFilters[meshFilterArray][i].sharedMesh = new Mesh();
                }   
                meshFilters[meshFilterArray][i].GetComponent<MeshRenderer>().material = colourSettings.planetMaterial;
                terrainFaces[terrainFaceArray][i] = new TerrainFace(shapeGenerator, meshFilters[meshFilterArray][i].sharedMesh, resolutions[resolution], directions[i]);
                bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
                meshFilters[meshFilterArray][i].gameObject.SetActive(renderFace);
            }

            CreateLODLevel();
            meshFilterArray++;
            resolution++;
        }
        //lodSelector.CreateLODGroup();
        this.gameObject.SetActive(false);
    }

    public void GeneratePlanet()
    {
        Initialize();
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
            if (meshFilters[meshFilterArray][i].gameObject.activeSelf)
            {
                terrainFaces[terrainFaceArray][i].ConstructMesh();
            }
        }

        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colourGenerator.UpdateColours();
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[meshFilterArray][i].gameObject.activeSelf)
            {
                terrainFaces[terrainFaceArray][i].UpdateUVs(colourGenerator);
            }
        }
    }

    void CreateLODLevel()
    {
        GenerateMesh();
        GenerateColours();
        GameObject obj;
        obj = Instantiate(this.gameObject, this.transform.position, this.transform.rotation);
        DestroyImmediate(obj.GetComponent<Planet>());
        //obj.transform.parent = this.transform.parent;
        lodSwapper.LODObjects.Add(obj);

        if (destroyChilds != 0)
        {
            for (int i = 0; i < destroyChilds; i++)
            {
                Destroy(obj.transform.GetChild(i).gameObject);
            }
        }
        destroyChilds += 6;
    }
}