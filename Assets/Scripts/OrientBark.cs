using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientBark : MonoBehaviour
{
    [Range(0,1)]
    public float rotationSmoothing = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // Remember to cast position in game world to the overall screen position
        Vector2 targetDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        // In order to apply some smoothing, we use Lerp instead of assigning the targetRotation immediately to transform
        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, transform.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothing);
        // Note: forward is the always Z axis which is perpendicular to the 2D space
    }
}
