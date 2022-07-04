using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRayCastScript : MonoBehaviour
{
    public GameObject clickedObject = null;
    private RaycastHit hit;
    private Ray ray;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckClick()
    {
        clickedObject = null;
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                clickedObject = hit.transform.gameObject;
                //Debug.Log(hit.transform.gameObject.name);
            }
        }
    }

     public Vector3 hitPoint { get { return hit.point; } }
}
