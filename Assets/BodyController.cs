using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    GameObject body = null;
    GameObject lArm = null;
    GameObject rArm = null;
    GameObject legs = null;

    GameObject lArmSparks = null;
    GameObject rArmSparks = null;

    // Start is called before the first frame update
    void Start()
    {
        body = transform.GetChild(0).gameObject;
        lArm = transform.GetChild(1).gameObject;
        rArm = transform.GetChild(2).gameObject;
        legs = transform.GetChild(3).gameObject;

        lArmSparks = transform.GetChild(4).gameObject;
        lArmSparks.SetActive(false);
        rArmSparks = transform.GetChild(5).gameObject;
        rArmSparks.SetActive(false);
    }

    public void DestroyLArm()
    {
        lArm.SetActive(false);
        lArmSparks.SetActive(true);
    }

    public void DestroyRArm()
    {
        rArm.SetActive(false);
        rArmSparks.SetActive(true);
    }

    public void DestroyLegs()
    {
        rArm.SetActive(false);
        rArmSparks.SetActive(true);
    }
}
