using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public GameObject player;

    public float viewRadius;
    public float viewAngle;

    //void ssum()
    //{
    //    Vector2 toOtherDir = other.transform.position - this.transform.position;
    //    float theta = Mathf.Acos(Vector2.Dot(shipDir, toOtherDir) / (shipdir.magnitude * toOtherDir.magnitude));
    //    theta = theta * Mathf.Rad2Deg;

    //    if (theta < fov / 2)
    //    {
    //        other.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    //    }
    //    else
    //    {
    //        other.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
    //    }
    //}
}
