using UnityEngine;

public static class UtilitiesSpaceMath {

    //Specific Position

    public static Vector3 absolutePositionInSpace(float radius, float degrees)
    {
        return new Vector3(radius * Mathf.Cos(degrees), radius * Mathf.Sin(degrees), 0f);
    }

    public static Vector3 relativePositionInSpace(Vector3 origin, float radius, float degrees)
    {
        return new Vector3((radius * Mathf.Cos(degrees)) + origin.x, (radius * Mathf.Sin(degrees)) + origin.y, 0f);
    }

    //Random Position

    public static Vector3 absoluteRandomPositionInSpace(float radiusMin, float radiusMax)
    {
        return absolutePositionInSpace(Random.Range(radiusMin, radiusMax), Random.Range(0, 360));
    }

    public static Vector3 relativeRandomPositionInSpace(Vector3 origin, float radiusMin, float radiusMax)
    {
        return relativePositionInSpace(origin, Random.Range(radiusMin, radiusMax), Random.Range(0, 360));
    }

    //Direction

    public static Vector3 directionNormalised(Vector3 origin, Vector3 target)
    {
        return (target - origin) / (target - origin).magnitude;
    }

    public static Quaternion directionRotation(Vector3 origin, Vector3 target)
    {
        return Quaternion.LookRotation((target - origin),Vector3.back);
    }
}
