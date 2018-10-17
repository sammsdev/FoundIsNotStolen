using UnityEngine;
using Cinemachine;
public class PlayerCameraHandler : MonoBehaviour {

    //declaring variables
    public Transform camTrans;

    // CinemachineOrbitalTransposer;

    public Camera camProps;     
    public Transform target;
    public Transform pivot;
    public bool leftPivot;
    float smoothX;
    float smoothY;
    float smoothXVelocity;
    float smoothYVelocity;
    float lookAngle;
    float tiltAngle;

    
    public struct CameraInputs
    {
        public float horizontal;
        public float vertical;
        public float stickX;
        public float stickZ;
        public float finalHor;
        public float finalVer;
    }

    public CameraInputs camInp;

    public CameraStats camValues;
   

    PlayerBehaviour _playerHolder;
    PlayerInputHandler _inpHolder;

   
    public void StartCameraHandler (PlayerBehaviour vPlayerState) {

        camProps = GetComponentInChildren<Camera>();
        this._playerHolder = vPlayerState;
        target = this._playerHolder.transform;

	}
	
	
	void LateUpdate () {

      camInp.finalHor = camInp.horizontal + camInp.stickX;
      camInp.finalVer = camInp.vertical + camInp.stickZ;

        HandlePosition();
        HandleRotation();
         float speed = camValues.moveSpeed;
        if (_playerHolder.playerState.isAiming)           
            speed = camValues.aimSpeed;

        Vector3 targetPosition = Vector3.Lerp(this.transform.position, target.position, Time.deltaTime * speed);
        this.transform.position = target.position;
    }


    void HandlePosition()
    {
        float targetX = camValues.normalX;
        float targetY = camValues.normalY;
        float targetZ = camValues.normalZ;
        camProps.fieldOfView = camValues.fieldOfViewNormal;

        if (_playerHolder.playerState.isCrouching)
        {
            targetY = camValues.crouchY;
        }

        if (_playerHolder.playerState.isAiming)
        {
            targetX = camValues.aimX;
            targetY = camValues.aimY;
            targetZ = camValues.aimZ;
            camProps.fieldOfView = camValues.fieldOfViewAiming;

        }

        if (leftPivot)
        {
            targetX = -targetX;
        }

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCamPosition = camTrans.localPosition;
        
        newCamPosition.z = targetZ;

        float t = Time.deltaTime * camValues.adaptSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        camTrans.transform.localPosition = Vector3.Lerp(camTrans.localPosition, newCamPosition, t);

    }

    void HandleRotation()
    {

        if(camValues.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, camInp.finalHor, ref smoothXVelocity, camValues.turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, camInp.finalVer, ref smoothYVelocity, camValues.turnSmooth);
        } else
        {
            smoothX = camInp.finalHor;
            smoothY = camInp.finalVer;

        }

        lookAngle += smoothX * camValues.y_rotate_speed;

        Quaternion targetRot = Quaternion.Euler(0, lookAngle, 0);
        this.transform.rotation = targetRot;

        tiltAngle -= smoothY * camValues.x_rotate_speed;
        tiltAngle = Mathf.Clamp(tiltAngle, camValues.minAngle, camValues.maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
    } 

}
