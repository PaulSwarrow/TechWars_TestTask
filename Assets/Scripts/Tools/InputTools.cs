using UnityEngine;

namespace Tools
{
    public static class InputTools
    {
        public static bool MouseToFloorPoint(Camera camera, float maxDistance, int layerMask, out Vector3 point)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, maxDistance, layerMask))
            {
                point = hit.point;
                return true;
            }

            point = default;
            return false;
        }
        
    }
}