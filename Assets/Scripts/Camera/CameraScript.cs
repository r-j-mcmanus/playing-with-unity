using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 baseCameraOfsetPosition;
    public float zoomMin = 0.7f;
    public float zoomMax = 1.3f;
    public float zoomSpeed = 3.0f;
    private float zoomVal = 1.0f;

    public Vector3 position { get { return gameObject.transform.position; } set { gameObject.transform.position = value; } }
    public Vector3 eulerAngles { get { return gameObject.transform.eulerAngles; } set { gameObject.transform.eulerAngles = value; } }
    public Vector3 forward { get { return gameObject.transform.forward; } set { gameObject.transform.forward = value; } }
    public Quaternion rotation { get { return gameObject.transform.rotation; } set { gameObject.transform.rotation = value; } }
    public void Translate(Vector3 vec) { gameObject.transform.Translate(vec); }
    public void TranslateWorld(Vector3 vec) { gameObject.transform.Translate(vec, Space.World); }
    public void LookAt(Transform target) { gameObject.transform.LookAt(target); }

    // Start is called before the first frame update
    void Start()
    {
        baseCameraOfsetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void zoom(float zoomAmount)
    {
        //Debug.Log(transform.localPosition);
        zoomVal = zoomVal + zoomSpeed * zoomAmount * Time.deltaTime;
        zoomVal = Mathf.Clamp(zoomVal, zoomMin, zoomMax);
        transform.localPosition = baseCameraOfsetPosition * zoomVal;
    }

}
