using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Collider))]
public class PlayerBehaviour : MonoBehaviour {

    //declaring player variables and components
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private GameObject _activeModel;
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Transform _playerTrans;
    [SerializeField] private Collider _playerCol;

    PlayerCameraHandler _camHolder;
    PlayerInputHandler _inpHolder;

    public GameObject weapon;
    public PlayerStats _playerAtrib;

    #region DeclaringStates
    public enum PlayerWorldState
    {
        onGround, onAir
    }

    [SerializeField] private PlayerWorldState _currentState;

    public struct PlayerStates
    {
        public bool onGround;
        public bool isAiming;
        public bool isCrouching;
        public bool isRunning;
        public bool isInteracting;

    }

    public PlayerStates playerState;

    #endregion

    #region DeclaringInputs
    //setting PlayersInputs 
    public struct PlayerInputs
    {
        public float hor;
        public float ver;
        public bool jump;
        public float moveAmount;
        public bool retain;
        public Vector3 moveDirection;
        public Vector3 aimPosition;
        public Vector3 rotateDirection;
        
    }

    public PlayerInputs playerInp;

    #endregion

    

    private void Start()
    {
        Cursor.visible = false;
        _playerRb = GetComponent<Rigidbody>();
        _playerCol = GetComponent<Collider>();
        _playerTrans = GetComponent<Transform>();
       
        _currentState = PlayerWorldState.onGround;
        _playerRb.isKinematic = false;
        _playerRb.drag = 1;
        _playerRb.angularDrag = 999;
        _playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        
        SetupAnimator();
        _camHolder = FindObjectOfType<PlayerCameraHandler>();
        _inpHolder = FindObjectOfType<PlayerInputHandler>();
        _camHolder.StartCameraHandler(this);
        _inpHolder.StartInputHandler(this, _camHolder);

    }


    public void FixedUpdate()
    {
        if (playerInp.retain)
            RetainWeapon();
        
        switch (_currentState)
        {
            
            case PlayerWorldState.onGround:
                Rotate();
                playerState.onGround = OnGround();
                if (!playerState.onGround)
                    _currentState = PlayerWorldState.onAir;
                if (playerState.isAiming)                
                    MoveOnGroundAiming();
                 else
                    MoveOnGroundNormal();
                 if (playerInp.jump)
                        Jump();                
                break;       

            case PlayerWorldState.onAir:
                Rotate();
                playerState.onGround = OnGround();                
                if (playerState.onGround)
                    _currentState = PlayerWorldState.onGround;
                break;
               
        }
    }

   

    private void Rotate()
    {
        //if (playerState.isAiming)        
         //   playerInp.rotateDirection = playerInp.moveDirection;           
        
        Vector3 tDir = playerInp.rotateDirection;
        //tDir.y = 0f;
        if (tDir == Vector3.zero)
            tDir = _playerTrans.forward;

        Quaternion lookDir = Quaternion.LookRotation(tDir);
        Quaternion tRotation = Quaternion.Slerp(_playerTrans.rotation, lookDir, _playerAtrib.rotationSpeed * Time.deltaTime);
        transform.rotation = tRotation;
    }

    private void MoveOnGroundNormal()
    {
        float speed = _playerAtrib.walkspeed;
        float maxVel = _playerAtrib.maxVelocity;
        if (playerState.isRunning) {

            speed = _playerAtrib.runSpeed;
            maxVel = _playerAtrib.maxVelocity * 2 ;
        }
        if (playerState.isCrouching)
            speed = _playerAtrib.crouchSpeed;
        Vector3 dir = Vector3.zero;
        dir = playerInp.moveDirection;
        if (_playerRb.velocity.magnitude > maxVel)
        {
            _playerRb.velocity = _playerRb.velocity.normalized * maxVel;
            return;
        }            
        _playerRb.AddForce(dir * playerInp.moveAmount * speed);
    }

    private void HandleAnimationNormal()
    {

    }

    private void Jump()
    {
        _playerRb.AddForce((Vector3.up + playerInp.moveDirection.normalized) * _playerAtrib.jumpForce, ForceMode.Impulse);
    }

    private void RetainWeapon()
    {
        //handleAnimation first / polish input
        weapon.SetActive(!weapon.activeSelf);
    }

    private void MoveOnGroundAiming()
    {
        float speed = _playerAtrib.aimSpeed;
        float maxVel = _playerAtrib.aimSpeed;
        Vector3 dir = Vector3.zero;
        dir = playerInp.moveDirection * speed;
        if (_playerRb.velocity.magnitude > maxVel)
        {
            _playerRb.velocity = _playerRb.velocity.normalized * maxVel;
            return;
        }
        _playerRb.AddForce(dir * playerInp.moveAmount * speed);
    }

    private void HandleAnimationAiming()
    {

    }

    void SetupAnimator()
    {
        if (_activeModel == null)
        {
            _playerAnim = GetComponentInChildren<Animator>();
            _activeModel = _playerAnim.gameObject;
        }

        if (_playerAnim == null)
            _playerAnim = _activeModel.GetComponent<Animator>();
    }

    bool OnGround()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        Vector3 origin = _playerTrans.position;
        origin.y += 0.6f;
        Vector3 dir = -Vector3.up;
        float dis = 1.8f;
        RaycastHit hit;

        if (Physics.Raycast(origin, dir, out hit, dis, layerMask))
            return true;

        return false;

    }

}
