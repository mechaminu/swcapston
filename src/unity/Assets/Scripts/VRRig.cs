using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

}
public class VRRig : MonoBehaviour
{
    public VRMap rightHand;
    public VRMap leftHand;
    public VRMap head;

    public float turnSmooth;

    public Transform headConstraints;
    private Vector3 headBodyOffset;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraints.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = headConstraints.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraints.up, Vector3.up).normalized, Time.deltaTime * turnSmooth);

        head.Map();
        leftHand.Map();
        //rightHand.Map();
    } 
}
