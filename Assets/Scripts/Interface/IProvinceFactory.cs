using System.Collections.Generic;
using UnityEngine;

namespace Interface {
    // todo: do shit
    public interface IProvinceFactory {
        Province Create(List<Vector2> contour, Color color);
    }
}