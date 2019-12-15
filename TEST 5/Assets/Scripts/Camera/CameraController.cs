using UnityEngine;


public class CameraController : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;

       
        

        //meget syret effekt, måske det kunne bruges til et spil element?
      //transform.LookAt(target);
    }
}

