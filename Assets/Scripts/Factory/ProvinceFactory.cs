using System.Collections.Generic;
using Static;
using UnityEngine;

namespace Factory {
    public static class ProvinceFactory {
        private static readonly int _zOffset = 0;
        private static int _nextProvinceId;

        
        public static GameObject Create(List<Vector2> contour, Color color) {
            List<Vector3> verts = new List<Vector3>();
            int[] tris = Triangulator.Triangulate(contour.ToArray());
            
            foreach (var contourVector2 in contour) {
                verts.Add(new Vector3(contourVector2.x, contourVector2.y, 0 + _zOffset));
            }
            
            ProvinceData pd = new ProvinceData(color, 1, _nextProvinceId +1);
            ProvinceMesh pm = new ProvinceMesh(verts.ToArray(), tris);

            _nextProvinceId++;
            
            Province province = new Province(pd, pm);
            GameObject provinceGameObject = GenerateGameObject(province);

            return provinceGameObject;
        }
        
        private static GameObject GenerateGameObject(Province province) {
            GameObject go = new GameObject($"Province-{province.GetProvinceData().ID}");
            MeshFilter mf = go.AddComponent<MeshFilter>();
            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            MeshCollider mc = go.AddComponent<MeshCollider>();
            ProvinceComponent provinceComponent = go.AddComponent<ProvinceComponent>();
            Material provinceMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));

            Mesh mesh = province.GetProvinceMesh().GetMesh();
        
            provinceMaterial.color = province.GetProvinceData().Color;
            mf.mesh = mesh;
            mc.sharedMesh = mesh;
            mr.material = provinceMaterial;
            go.tag = "PROVINCE";
            
            provinceComponent.province = province;
            
            return go;
        }
    }
}