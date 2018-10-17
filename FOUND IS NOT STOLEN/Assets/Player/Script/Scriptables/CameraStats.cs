using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/CameraStats")]
public class CameraStats : ScriptableObject
{

    public float turnSmooth = .1f;
    public float moveSpeed = 9;
    public float aimSpeed = 15;
    public float y_rotate_speed = 8;
    public float x_rotate_speed = 8;

    public float minAngle = -35;
    public float maxAngle = 35;

    public float normalZ;
    public float normalX;
    public float normalY;
    public float aimZ = -.5f;
    public float aimX = 0;
    public float aimY = 0;

    public float fieldOfViewNormal = 60f;
    public float fieldOfViewAiming = 20f;
    
    public float crouchY;
    public float adaptSpeed = 9;


}