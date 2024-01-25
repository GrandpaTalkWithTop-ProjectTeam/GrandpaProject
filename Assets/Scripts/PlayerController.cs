using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float backSpeed;
    public float walkSpeed;
    
    public float sprintSpeed;
    public float slideSpeed;
    public float climbSpeed;
    public float swingSpeed;
    public float wallrunSpeed;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;


    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;


    [Header("Keybinds")]
    public KeyCode jumpKey=KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    //ġƮŰ �Ҵ�
    public KeyCode cheatKey1 = KeyCode.F1;
    public KeyCode cheatKey2 = KeyCode.F2;
    public KeyCode cheatKey3 = KeyCode.F3;
    public KeyCode cheatKey4 = KeyCode.F4;
    public KeyCode cheatKey5 = KeyCode.F5;
    public KeyCode cheatKey6 = KeyCode.F6;
    public KeyCode cheatKeyF = KeyCode.F;





    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public bool onMovingPlatform;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;



    [Header("References")]
    public Climbing climbingScript;
    public Transform orientation;
    public Transform playerStart;
    public Transform playerStart2;
    public Transform playerStart3;
    public Transform playerStart4;
    public Transform playerStart5;
    public PlayerCamera pc;
    public Transform playerGun;
    private float rungundown = 0;
    private float rungunrot = 0;
    [SerializeField]
    public float movingnow = 0;

    float horizontalInput;
    float verticalInput;

    public float wallJumpCool = 0;

    Vector3 moveDirection;


    public Rigidbody rb;
    public bool flyMode = false;
    public string deadZoneLayer;



    public MovementState state;
    public enum MovementState
    {
        freeze,
        swinging,
        wallrunning,
        walking,
        sprinting,
        climbing,
        crouching,
        sliding,
        air
    }

    public bool freeze;

    public bool activeGrapple;
    public bool swinging;

    public bool sliding;
    public bool crouching;
    public bool climbing;

    public bool wallrunning;

    private bool flycheaton = false;
    



    private void Start()
    {
        rb=GetComponent<Rigidbody>();
        rb.freezeRotation=true; 

        readyToJump=true;

        startYScale = transform.localScale.y;
        
    }

    private void Update()
    {
        


        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        playerGun.transform.rotation = Quaternion.Euler(pc.xRotation + rungunrot, pc.yRotation, 0);
        rungunrot += (rungundown - rungunrot) * 0.2f;

        if ((state == MovementState.wallrunning) || (state == MovementState.sprinting))
        {
            pc.changeNum = 20f;
            rungundown = 30;
        }
        else
        {
            pc.changeNum = 0;
            rungundown = 0;
        }

        



        if (GameManager.GameIsPaused == false)
            MyInput();
        SpeedControl();
        StateHandler();

        if ((grounded && !activeGrapple) || (flyMode))
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (freeze)
        {
            rb.velocity = Vector3.zero;

        }
    }


    private void FixedUpdate()
    {
        
        Vector3 currentRotation = transform.rotation.eulerAngles;
        Quaternion newRotation = Quaternion.Euler(0, currentRotation.y, 0);
        transform.rotation = newRotation;

        
        if (transform.parent == null)
            transform.localScale = Vector3.one;

        MovePlayer();
        if (wallJumpCool>0)
            wallJumpCool-= Time.smoothDeltaTime;

        if (Input.GetKey(KeyCode.E) && flyMode == true)
        {

            transform.Translate(Vector3.up * Time.deltaTime * 20f);

        }

        if (Input.GetKey(KeyCode.Q) && flyMode == true)
        {

            transform.Translate(Vector3.down * Time.deltaTime * 20f);

        }
    }
    private void MyInput()
    {
        if (!wallrunning)
          {
            horizontalInput = Input.GetAxisRaw("Horizontal");
          }
        verticalInput = Input.GetAxisRaw("Vertical");
        

        if (Input.GetKey(cheatKey1))
        {
            transform.position = playerStart.transform.position;

        }

        if (Input.GetKey(cheatKey2))
        {
            transform.position = playerStart2.transform.position;

        }

        if (Input.GetKey(cheatKey3))
        {
            transform.position = playerStart3.transform.position;   


        }

        if (Input.GetKey(cheatKey4))
        {
            transform.position = playerStart4.transform.position;


        }

        if (Input.GetKey(cheatKey5))
        {
            transform.position = playerStart5.transform.position;


        }

        if (Input.GetKey(cheatKey6))
        {
            flycheaton = true;


        }



        if (Input.GetKeyUp(cheatKeyF))
        {
            if (flycheaton == true)
            {
                flyMode = flyMode ? false : true;
                if (flyMode)
                    rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    moveSpeed = 0;
                }

            }
        }

        //�����Ҷ�
        if (Input.GetKey(jumpKey) && readyToJump && grounded && flyMode==false)
        {
            
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

        }

        


    }

    private void StateHandler()
    {
        // ���� ���
        if (freeze)
        {
            state = MovementState.freeze;
            moveSpeed = 0;  
            rb.velocity= Vector3.zero;

        }

        //���� ���
        else if (swinging)
        {
            state = MovementState.swinging;
            desiredMoveSpeed = swingSpeed;


        }
        

        // ��Ÿ�� ���
        else if (climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }

        //���޸��� ���
        else if (wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunSpeed;

        }

        //�����̵� ���
        else if (sliding)
        {
            state = MovementState.sliding;

            if (Onslope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideSpeed;

            else
                desiredMoveSpeed = sprintSpeed;
        }

        //�ɱ� ���
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;

        }

        // �޸��� ���
        else if (grounded && Input.GetKey(sprintKey) && Input.GetKey(KeyCode.W))
        {
            state = MovementState.sprinting;
            if (moveSpeed < sprintSpeed)
                moveSpeed = sprintSpeed;
            desiredMoveSpeed = sprintSpeed;
        }

        //�ȱ� ���
        else if (grounded)
        {
            state = MovementState.walking;
            if (verticalInput < 0)
            {
                moveSpeed = backSpeed;
                desiredMoveSpeed = backSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
                desiredMoveSpeed = walkSpeed;
            }
            

    
        }

        //���߿���
        else
        {
            state = MovementState.air;

            if (flyMode)
            {
                desiredMoveSpeed = 20;
                if (moveSpeed < 20)
                    moveSpeed = 20;
            }
            else
            {
                if (desiredMoveSpeed < sprintSpeed)
                    desiredMoveSpeed = walkSpeed;
                else
                    desiredMoveSpeed = sprintSpeed;
            }
            


        }

        // ���ӱ�
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 2f && moveSpeed !=0) //4f
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        //�ε巴�� movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while(time< difference)
        {
            moveSpeed=Mathf.Lerp(startValue, desiredMoveSpeed, time/ difference);

                time += Time.deltaTime* speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;

    }

  

    private void MovePlayer()
    {
        if (activeGrapple) return;
        if (swinging) return;

        if (climbingScript.exitingWall) return;

        // ���� ���
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        movingnow = Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput);

        //��翡��
        if (Onslope())
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);

        }

        //������
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        //���߿���
        else if (!grounded || !flyMode)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        //��翡 ������ �߷��� ����
        rb.useGravity = !Onslope();
    }

    private void SpeedControl()
    {
        if (activeGrapple) return;

        
        //��翡�� �ӵ� ����
        if (Onslope() && !exitingSlope)
        {
            if (rb.velocity.magnitude>moveSpeed)
                rb.velocity=rb.velocity.normalized*moveSpeed;

        }
        //���� �Ǵ� ������ �ӵ� ����
        else
        {
            
        
            Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);

            if (flatVel.magnitude>moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Jump);

        //y�ӵ� �ʱ�ȭ
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope=false;
    }

    private bool enableMovementOnNextTouch;

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;

    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;
    }

    public void ResetRestrictions()
    {
        activeGrapple=false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;

            ResetRestrictions();

            

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer(deadZoneLayer))
        {
            // �浹�� ������Ʈ�� ���̾ ������ ���̾�� ���� ��
            if (playerStart != null)
            {
                transform.position = playerStart.transform.position;
                rb.velocity = Vector3.zero;
            }
        }
    }


    public bool Onslope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight*0.5f+0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;

    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;

    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;

    }

}
