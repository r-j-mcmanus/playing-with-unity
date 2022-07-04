using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFaceScreenScript : MonoBehaviour
{
    public Transform camTransform;
    public Vector3 front;

    private Vector3 centeredPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(gameObject.transform.position - camTransform.position);
        front = gameObject.transform.forward;
    }

    public void SetText(string text)
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = text;
    }

    public void SetActive(bool val)
    {
        gameObject.SetActive(val);
    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
        centeredPosition = pos;
    }
}
