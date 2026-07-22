using UnityEngine;

namespace Battlefield
{
    public static class TargetFinder
    {
        public static bool IsInRange(Transform target, Transform self, float range)
        {
            float sqrDistance = (self.position - target.position).sqrMagnitude;
            float sqrRange = range * range;
            //Debug.Log($"sqrDistance: {sqrDistance}, sqrRange: {sqrRange}");
            return sqrDistance <= sqrRange;
        }
    }
}