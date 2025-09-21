using UnityEngine;

public class ProvinceMesh
{
    private Vector3[] _vertices;
    private int[] _triangles;

    public ProvinceMesh(Vector3[] verts, int[] tris) {
        _vertices = verts;
        _triangles = tris;
    }

    public Mesh GetMesh()
    {

        Mesh mesh = new Mesh();
        mesh.vertices = _vertices;
        mesh.triangles = _triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        
        return mesh;
    }
}