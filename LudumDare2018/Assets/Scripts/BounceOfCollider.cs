using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOfCollider : MonoBehaviour {

    Transform directionIndicator;
    Vector2 normal;

    private void Start()
    {
        directionIndicator = transform.GetChild(0);
        normal = (directionIndicator.position - transform.position).normalized;
    }

    public Vector2 GetDirection(Vector2 incomingDirection)
    {

        return Vector2.Reflect(incomingDirection, normal);
    }


}
