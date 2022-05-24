using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    public StateMachine movementSM;
    public Idle idle;
    public Move move;
    public Jump jump;
    public Win win;
    public Death playerDeath;
    public float moveVelocity;
    public float jumpPower;
    public float rotateSpeed;
    public Joystick _joy;
    public JoyButton _joyButton;
    internal Rigidbody rb;
    internal CapsuleCollider capsuleCollider;
    internal Animator anim;
    #endregion
    private void Awake()
    {
        movementSM = new StateMachine();
        idle = new Idle(this, movementSM);
        move = new Move(this, movementSM);
        jump = new Jump(this, movementSM);
        win = new Win(this, movementSM);
        playerDeath = new Death(this, movementSM);
        movementSM.Initialize(idle);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    void Start()
    {

    }
    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();

    }

    void Update()
    {
        movementSM.CurrentState.LogicUpdate();

    }
    #region Physics

    #endregion
    #region Methods
    internal bool IsGrounded()
    {
       
            float extraHeight = 0.01f;
            RaycastHit hit;
            Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, out hit, capsuleCollider.bounds.extents.y + extraHeight);
            return hit.collider != null;
        
    }
    internal void JoyMove()
    {
        rb.velocity = new Vector3(_joy.Horizontal * moveVelocity, rb.velocity.y, _joy.Vertical * moveVelocity);
        Vector3 movementDirection = new Vector3(_joy.Horizontal, 0, _joy.Vertical);
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.fixedDeltaTime);
        }
        
    }
    public void WalkingSound() => AudioManager.singleton.WalkingSound();
    IEnumerator Win()
    {
        anim.SetTrigger("finish");
        yield return new WaitForSeconds(2);
        GameManager.singleton.WinOrLoss(true);
    }


    #endregion
    #region Physics
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("deathTrigger"))
        {
            GameManager.singleton.WinOrLoss(false);
        }
        if (other.CompareTag("Carrot"))
        {
            GameManager.singleton.PickUpCarrot();
            AudioManager.singleton.PickUpSound();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Finish"))
        {
            movementSM.ChangeState(win);
             StartCoroutine(Win());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //временный костыль на выключение прыжка
        if (IsGrounded())
        {
            anim.SetBool("jump", false);
        }
    }
    #endregion
}
