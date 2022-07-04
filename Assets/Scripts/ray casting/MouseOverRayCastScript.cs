using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverRayCastScript : MonoBehaviour
{
    public GameObject mouseOverObject = null;
    private RaycastHit hit;
    private Ray ray;

    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        lastMousePosition = Input.mousePosition;
        mouseOverObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckMouseOver()
    {
        mouseOverObject = null;
        lastMousePosition = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            mouseOverObject = hit.transform.gameObject;
        }
    }


    public Vector3 hitPoint { get { return hit.point; } }
}
