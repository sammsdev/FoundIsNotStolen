using UnityEngine;



[CreateAssetMenu(menuName = "Scriptables/PlayerStats")]
public class PlayerStats : ScriptableObject {

    public float walkspeed = 4f;
    public float maxVelocity;
    public float runSpeed = 8f;
    public float jumpForce = 50f;
    public float aimSpeed = 2f;
    public float crouchSpeed = 2f;
    public float rotationSpeed = 8f;
}
