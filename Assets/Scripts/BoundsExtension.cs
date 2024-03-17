using UnityEngine;

public static class BoundsExtension 
{
    public static bool ContainBound(this Bounds bounds, Bounds target)
    {
        return bounds.Contains(target.max) && bounds.Contains(target.min);
    }
}
