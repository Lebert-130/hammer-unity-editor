using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class meshGenerator : MonoBehaviour {

    public Mesh mesh;

    public Vector3 vertex1;
    public Vector3 vertex2;
    public Vector3 vertex3;
    public Vector3 vertex4;

    Vector3[] vertices;
    int[] triangles;

	void Start () {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
	}

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            vertex1,
            vertex2,
            vertex3,
            vertex4
        };

        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
