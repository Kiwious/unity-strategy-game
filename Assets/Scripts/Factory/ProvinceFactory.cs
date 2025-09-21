using System.Collections.Generic;
using Static;
using UnityEngine;

namespace Factory {
    public static class ProvinceFactory {
        private static readonly int _zOffset = 0;
        private static int _nextProvinceId;

        
        public static Province Create(List<Vector2> contour, Color color) {
            List<Vector3> verts = new List<Vector3>();
            int[] tris = Triangulator.Triangulate(contour.ToArray());
            
            foreach (var contourVector2 in contour) {
                verts.Add(new Vector3(contourVector2.x, contourVector2.y, 0 + _zOffset));
            }
            
            ProvinceData pd = new ProvinceData(color, 1, _nextProvinceId);
            Mesh pm = new Mesh();
            pm.vertices = verts.ToArray();
            pm.triangles = tris;
            pm.RecalculateBounds();
            pm.RecalculateNormals();
            _nextProvinceId++;
            
            Province province = new Province(pd, pm);
            // note: removed due to restructure of provincemanager singleton
            // GameObject provinceGameObject = GenerateGameObject(province);

            return province;
        }
        
        public static GameObject GenerateGameObject(Province province) {
            GameObject go = new GameObject($"Province-{province.provinceData.ID}");
            MeshFilter mf = go.AddComponent<MeshFilter>();
            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            MeshCollider mc = go.AddComponent<MeshCollider>();
            ProvinceComponent provinceComponent = go.AddComponent<ProvinceComponent>();
            Material provinceMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            
            LineRenderer lr = go.AddComponent<LineRenderer>();
            Vector3[] verts = province.provinceMesh.vertices;
            

            Material lrMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            lrMaterial.color = Color.black;
            lr.material = lrMaterial;

            lr.positionCount = verts.Length;
            lr.loop = true;
            lr.widthMultiplier = 5f;
            lr.useWorldSpace = false;
            lr.SetPositions(verts);
            lr.enabled = false;

            Mesh mesh = province.provinceMesh;
        
            provinceMaterial.color = province.provinceData.Color;
            mf.mesh = mesh;
            mc.sharedMesh = mesh;
            mr.material = provinceMaterial;
            go.tag = "PROVINCE";
            
            provinceComponent.province = province;
            
            return go;
        }
    }
}