using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPosition : MonoBehaviour
{
    [SerializeField] GameObject LeftHandAnchor;
    [SerializeField] GameObject LeftArmIK;


    [SerializeField] Vector3 LeftHandPos;
    [SerializeField] Vector3 LeftArmPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LeftHandPos = LeftHandAnchor.transform.position;
        LeftArmPos = LeftArmIK.transform.position;
    }
}
