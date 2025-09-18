using System;
using System.Collections.Generic;
using UnityEngine;

public class ProvinceMesh
{
    private Vector3[] _vertices;
    private int[] _triangles;
    private Vector2[] _uvs;

    public ProvinceMesh(Vector3[] verts, int[] tris, Vector2[] uvs)
    {
        _vertices = verts;
        _triangles = tris;
        _uvs = uvs;
    }

    public ProvinceMesh(Vector3[] verts, int[] tris) {
        _vertices = verts;
        _triangles = tris;
    }

    public Mesh GetMesh()
    {

        Mesh mesh = new Mesh();
        mesh.vertices = _vertices;
        if (_uvs != null) {
            mesh.uv = _uvs;
        }
        mesh.triangles = _triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        
        return mesh;
    }
}