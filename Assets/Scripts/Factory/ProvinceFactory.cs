using System.Collections.Generic;
using Interface;
using Static;
using UnityEngine;

namespace Factory {
    public static class ProvinceFactory {
        private static readonly int _zOffset = 0;
        
        public static Province Create(List<Vector2> contour, Color color) {
            List<Vector3> verts = new List<Vector3>();
            int[] tris = Triangulator.Triangulate(contour.ToArray());
            
            foreach (var contourVector2 in contour) {
                verts.Add(new Vector3(contourVector2.x, contourVector2.y, 0 + _zOffset));
            }
            
            ProvinceData pd = new ProvinceData(color, 1, "province");
            ProvinceMesh pm = new ProvinceMesh(verts.ToArray(), tris);
            
            Province province = new Province(pd, pm);

            return province;
        }
    }
}