using UnityEngine;
using System.Collections.Generic;

public class FPCamera : MonoBehaviour
{
    private Camera cam;

    public float yawSensitivity;
    public float pitchSensitivity;
    //private float pitchSensitivity = 5f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float pitch_clamped;

    Quaternion CameraRotation;
    Vector3 StartLocalPosition;
    private float bobPace = 8f;
    private float bobOffest = 0.03f;

    private float tiltPace = 4.5f; //8f
    private float tiltOffest = 0.65f; //1.75f

    Quaternion StartLocalRotation;
    Quaternion rotateQuat;
    FPController charMoveScript;

    // Start is called before the first frame update
    void Awake()
    {
        charMoveScript = GetComponentInParent<FPController>();
        StartLocalPosition = transform.localPosition;
        StartLocalRotation = transform.localRotation;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        //up the tilting on running
        if (charMoveScript.IsSprinting)
        {
            tiltPace = 5.75f;
            tiltOffest = 0.90f;
        }
        else
        {
            tiltPace = 4.5f;
            tiltOffest = 0.65f;
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotateCamera();

        if (!charMoveScript.isMoving) //this may need to be in LateUpdate
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, StartLocalPosition, 0.01f);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, StartLocalRotation, 0.01f);
        }
    }

    private void RotateCamera()
    {
        if (Time.timeScale == 0) return;
        //saves axis movement of x and y mouse movement
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // yaw and pitch values change determined on 
        // mousex and mousey movement and applied sensitivity to both
        yaw += mx * yawSensitivity;
        pitch -= my * pitchSensitivity;

        //clamp pitch, so camera doesn't rotate too far low or high to seem weird
        pitch_clamped = Mathf.Clamp(pitch, -89f, 50f); //45?

        CameraRotation = transform.rotation = Quaternion.Euler(pitch_clamped, yaw, 0);
        // use the clamped pitch and yaw to rotate camera rig, entered in as euler angles through Quaternion class
        if (Time.timeScale > 0 && !charMoveScript.isMoving) transform.rotation = Quaternion.RotateTowards(transform.rotation, CameraRotation, 0.01f);
        else if (Time.timeScale > 0 && charMoveScript.isMoving)
        {
            CameraLocalSway();
        }
    }
    private void CameraLocalSway()
    {
        float moveSway = Mathf.Sin(Time.time * bobPace) * bobOffest;
        Vector3 swayVec = new Vector3(StartLocalPosition.x, StartLocalPosition.y + moveSway, StartLocalPosition.z);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, swayVec, 0.1f);

        float rotateSway = Mathf.Sin(Time.time * tiltPace) * tiltOffest;
        rotateQuat = Quaternion.Euler(StartLocalRotation.x, StartLocalRotation.y, StartLocalRotation.z + rotateSway);
        transform.rotation = CameraRotation * rotateQuat;
    }
}