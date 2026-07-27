using UnityEngine;

namespace Core
{
    public class ArenaBound : MonoBehaviour
    {
        [SerializeField] private PolygonCollider2D polygon;
        
        public bool IsInside(Vector2 point)
        {
            return polygon.OverlapPoint(point);
        }
    }
}