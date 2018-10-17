using UnityEngine;

public class PlayerInputHandler : MonoBehaviour {

    private float _horizontal = 0;
    private float _vertical = 0;
    private bool aimImput;
    private bool sprintInput;
    private bool jump;
    private bool shootInput;
    private bool crouchInput;
    private bool reloadInput;
    private bool switchInput;
    private bool pivotInput;
    private bool retainInput;

    PlayerCameraHandler _cam;
    private Transform _cameraHolder;
    public PlayerBehaviour playerHolder;
    public Shoot shot;

    public void StartInputHandler(PlayerBehaviour player, PlayerCameraHandler cam)
    {
        this._cam = cam;
        playerHolder = player;
        _cameraHolder = cam.transform;
        shot = FindObjectOfType<Shoot>();
    }


    private void Update()
    {
        SettingUpPlayer();
        GetAxis();
        
    }

    public void GetAxis()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Cancel"))        
            MenuManager.instance.LoadSceneGame("Menu");        
        if (Input.GetAxis("Aim") != 0)
            aimImput = true;
        else
            aimImput = false;

        sprintInput = Input.GetButton("Run");
        jump = Input.GetButton("Jump");
        _cam.camInp.horizontal = Input.GetAxis("Mouse X");
        _cam.camInp.vertical = Input.GetAxis("Mouse Y");
        retainInput = Input.GetButtonDown("Retain");
        shot.shotInp.firstShot = Input.GetButtonDown("Fire1");
        shot.shotInp.secondShot = Input.GetButtonDown("Fire2");

    }
    private void SettingUpPlayer()
    {
        playerHolder.playerState.isRunning = sprintInput;
        playerHolder.playerInp.jump = jump;
        playerHolder.playerState.isAiming = aimImput;
        playerHolder.playerInp.rotateDirection = _cam.transform.forward;           
        playerHolder.playerInp.hor = _horizontal;
        playerHolder.playerInp.ver = _vertical;
        playerHolder.playerInp.retain = retainInput;
        playerHolder.playerInp.moveAmount = Mathf.Clamp01(Mathf.Abs(_horizontal) + Mathf.Abs(_vertical));
        playerHolder.playerInp.moveDirection = CalculateMoveDirection();
    }

    public Vector3 CalculateMoveDirection()
    {
        Vector3 moveDir = _cameraHolder.forward * _vertical;
        moveDir += _cameraHolder.right * _horizontal;
        moveDir.Normalize();
        return moveDir;
    }
}
