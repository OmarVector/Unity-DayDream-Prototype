using System.Collections.Generic;
using UnityEngine;

public static class HelperClass 
{
   //Adding as much helper functions that may be used
   
   //Getting Random point within diameter of a circle 
     public static Vector3 RandomLocationWithinCircle(Vector3 center, float minRange, float maxRange)
    {
        var location = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * Vector3.forward;
        location = location * Random.Range(minRange, maxRange);
        return center + location;
    }
    
     
}
