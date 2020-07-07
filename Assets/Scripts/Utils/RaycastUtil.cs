using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastUtil
{

    public delegate void Callback(RaycastHit hit);

    public static void Raycast(Vector3 startPos, Vector3 lookPos, float maxDistance, Callback callback, bool drawDebug = false)
    {
        if (drawDebug)
            Debug.DrawRay(startPos, lookPos * maxDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(startPos, lookPos, out hit, maxDistance))
        {
            if (callback != null)
                callback.Invoke(hit);
        }
    }

    public static void RaycastAll(Vector3 startPos, Vector3 lookPos, float maxDistance, Callback callback, bool drawDebug = false)
    {
        if (drawDebug)
            Debug.DrawRay(startPos, lookPos * maxDistance, Color.red);

        RaycastHit[] hits = Physics.RaycastAll(startPos, lookPos, maxDistance);

        foreach (RaycastHit hit in hits)
        {
            callback.Invoke(hit);
        }
    }

}
