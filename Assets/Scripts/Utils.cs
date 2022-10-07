using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ham bo tro
public class Utils
{
    // so sanh layer mask cua object
    public static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }

    public static void FlipAnimation(GameObject obj, Vector3 moveDelta)
    {
        if (moveDelta.x > 0)
            obj.transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            obj.transform.localScale = new Vector3(-1, 1, 1);
    }
}
