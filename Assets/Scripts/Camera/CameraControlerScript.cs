using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlerScript : MonoBehaviour
{
    public CameraScript cameraScript = null;

    public float rotationSpeed = 20.0f;
    public float translationSpeed = 10.0f;

    private Vector3 cameraPosition;
    private Vector3 cameraEulerAngles;

    private Vector3 cameraPreviousPosition;
    private Quaternion cameraPreviousEulerAngles;

    private float horizontal;
    private float vertical;
    private float rotate;
    private float scroll;
    private Vector3 position;
    private Vector3 rotationEA;

    float frontOffset= -10.0f;
    float leftOffset = 3f;
    float upOffset = 2f;

    bool _playerControl = true;

    // Start is called before the first frame update

    void Start()
    {
        if (this.cameraScript == null)
        {
            this.cameraScript = this.gameObject.GetComponent<CameraScript>();
        }
        cameraPosition = new Vector3(10, 10, 10);
        cameraEulerAngles = new Vector3(45, -135, 0);
        RecordPositionAndRotation();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("RotateCamera");
        scroll = Input.GetAxis("Mouse ScrollWheel"); 
    }

    void FixedUpdate()
    {
        if(_playerControl)
        {
            PlayerControlCamera();
        }
        
    }

    public void OverShoulderCamera(Transform goTransform)
    {
        Vector3 frontVector = goTransform.forward.normalized;
        Vector3 leftVector = new Vector3(-frontVector.z, 0, frontVector.x).normalized;
        Vector3 upVector = Vector3.Cross(leftVector, frontVector).normalized;

        cameraScript.position = goTransform.position;
        cameraScript.rotation = Quaternion.LookRotation(frontVector);

        Vector3 offsetVector = frontOffset * frontVector + leftOffset * leftVector + upOffset * upVector;

        cameraScript.TranslateWorld(offsetVector);
    }

    public void LookAt(Transform target)
    {
        cameraScript.LookAt(target);
    }

    void PlayerControlCamera()
    {
        position = transform.position;
        rotationEA = transform.eulerAngles;

        cameraScript.zoom(scroll);

        rotationEA.y = rotationEA.y + cameraEulerAngles.y;
        rotationEA.y = rotationEA.y * Mathf.PI / 180;

        position.x = position.x + ( Mathf.Cos( rotationEA.y ) * horizontal + Mathf.Sin( rotationEA.y ) * vertical) * translationSpeed * Time.deltaTime;
        position.z = position.z - ( Mathf.Sin( rotationEA.y ) * horizontal - Mathf.Cos( rotationEA.y ) * vertical) * translationSpeed * Time.deltaTime;

        rotationEA.y = rotationEA.y / Mathf.PI * 180;
        rotationEA.y = rotationEA.y - rotationSpeed * rotate * Time.deltaTime - cameraEulerAngles.y;

        this.transform.position = position;
        this.transform.eulerAngles = rotationEA;
    }

    public void RecordPositionAndRotation()
    {
        cameraPreviousPosition = cameraScript.position;
        cameraPreviousEulerAngles = cameraScript.rotation;
    }

    public void RevertToPositionAndRotation()
    {
        cameraScript.position = cameraPreviousPosition;
        cameraScript.rotation = cameraPreviousEulerAngles;
    }

    public void SetCameraCenterPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public IEnumerator GoToAttackingCamera(Transform attackerTransform, Transform attackedTransform, float dT)
    {
        RecordPositionAndRotation();
        _playerControl = false;

        Vector3 forward = attackedTransform.position - attackerTransform.position;

        attackerTransform.rotation = Quaternion.LookRotation(forward);
        attackedTransform.rotation = Quaternion.LookRotation(-forward);

        OverShoulderCamera(attackerTransform);
        LookAt(attackedTransform);

        yield return new WaitForSeconds(dT);

        RevertToPositionAndRotation();
        _playerControl = true;
    }

}
