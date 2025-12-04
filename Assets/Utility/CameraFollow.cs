using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Class: J_CameraFollow
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/26/24
 * Last Modified: 12/2/2024 
 * 
 * Purpose: Has camera follow player character
 */

public enum FollowMode
{
    Lerp,
    XLerp,
    YLerp
}
public class CameraFollow : MonoBehaviour
{
    [Tooltip("Transform to follow, follows target vector if null")]
    [SerializeField] Transform TargetObject;

    [Tooltip("Point for camera to track to")]
    [SerializeField] Vector3 Target;

    [Tooltip("Offset camera keeps from target.")]
    [SerializeField] Vector3 Offset;

    [Tooltip("Axis camera can follow on.")]
    [SerializeField] FollowMode FollowMode;
    [SerializeField] float Interpolant;
    // Start is called before the first frame update
    void Start()
    {
        //Subscribes to playercontroler land event
        PlatformControler.OnLand += UpdateTarget;
    }

    // Update is called once per frame
    void Update()
    {
        //Uses TargetObject for tracking if not null
        if(TargetObject != null) Target = TargetObject.position;

        //Smoothly follows Target locking on axis as set by FollowMode
        switch (FollowMode)
        {
            case FollowMode.Lerp:
                transform.position = Vector3.Lerp(transform.position, Target + Offset, Time.deltaTime/Interpolant);
                break;
            case FollowMode.XLerp:
                transform.position = new Vector3(Mathf.Lerp(transform.position.x,Target.x+Offset.x,Interpolant*Time.deltaTime),transform.position.y,transform.position.z);
                break;
            case FollowMode.YLerp:
                transform.position = new Vector3( transform.position.x, Mathf.Lerp(transform.position.y, Target.y + Offset.y, Interpolant*Time.deltaTime), transform.position.z);
                break;
            default:
                throw new System.Exception($"Unhandled Followmode: {FollowMode}");
        }
    }

    void UpdateTarget(Vector3 pos)
    {
        Target = pos;
    }
}
