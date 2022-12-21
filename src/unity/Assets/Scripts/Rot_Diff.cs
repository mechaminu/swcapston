using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot_Diff : MonoBehaviour
{
    [SerializeField] GameObject RotTarget;

    private GameObject FinalRotObject;

    private float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        if (RotTarget.transform.childCount == 0)
        {
            FinalRotObject = RotTarget;
        }
        else
        {
            FinalRotObject = RotTarget.transform.GetChild(0).GetChild(0).gameObject;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = FinalRotObject.transform.rotation;
        
    }
}
