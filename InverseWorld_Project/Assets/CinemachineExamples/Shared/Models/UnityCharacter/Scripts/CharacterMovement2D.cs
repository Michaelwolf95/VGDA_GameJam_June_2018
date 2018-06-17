using System.Collections;
using UnityEngine;

namespace Cinemachine.Examples
{
    public class CharacterMovement2D : MonoBehaviour
    {
        public KeyCode sprintJoystick = KeyCode.JoystickButton2;
        public KeyCode jumpJoystick = KeyCode.JoystickButton0;
        public KeyCode sprintKeyboard = KeyCode.LeftShift;
        public KeyCode jumpKeyboard = KeyCode.Space;
        public float jumpVelocity = 7f;
        public float groundTolerance = 0.2f;
        public bool checkGroundForJump = true;

        private float speed = 0f;
        private bool isSprinting = false;
        public Animator anim;
        private Vector2 input;
        private float velocity;
        private bool headingleft = false;
        private Quaternion targetrot;
        private Rigidbody rigbody;

        [Header("Inverted World")]
        public LayerMask GroundMask;
        public float SwapTime = 0.15f;
        public float moveSpeed = 5f;

        private Coroutine castCoroutine = null;
        private string invertedObstacleLayerName = "Inverted";

        private CapsuleCollider capsule;
        private Vector3 capsuleCenter;
        private float capsuleHeight;
        public Vector3 capsuleCenterAir;
        public float capsuleHeightAir;

        // Use this for initialization
        void Start ()
	    {
	        if(!anim) anim = GetComponentInChildren<Animator>();
	        rigbody = GetComponent<Rigidbody>();
	        targetrot = transform.rotation;
	        capsule = GetComponent<CapsuleCollider>();
	        capsuleHeight = capsule.height;
	        capsuleCenter = capsule.center;
	    }

        void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (castCoroutine == null)
                {
                    // check if the player is currently overalling an obstacle
                    Collider[] overllapedColliders = Physics.OverlapCapsule(
                        transform.position + capsule.center + new Vector3(0f, (capsule.height/2f), 0f),
                        transform.position + capsule.center - new Vector3(0f, (capsule.height / 2f), 0f), 
                        capsule.radius);
                    //Collider[] overllapedColliders = Physics.OverlapCapsule(transform.position, transform.position + Vector3.up * 2, .5f);

                    bool overlappingObstacle = false;

                    foreach (var col in overllapedColliders)
                    {
                        if (col.gameObject.layer == LayerMask.NameToLayer(invertedObstacleLayerName))
                            overlappingObstacle = true;
                    }

                    if(!overlappingObstacle)
                        castCoroutine = StartCoroutine(CoCastInvertWorld());
                }
            }
        }
	
	    // Update is called once per frame
	    void FixedUpdate ()
	    {
	        if (Time.timeScale <= 0)
	        {
                return;
	        }

	        bool grounded = IsGrounded();


            input.x = Input.GetAxis("Horizontal");

            // Check if direction changes
	        if ((input.x < 0f && !headingleft) || (input.x > 0f && headingleft))
	        {  
                if (input.x < 0f) targetrot = Quaternion.Euler(0, 270, 0);
	            if (input.x > 0f) targetrot = Quaternion.Euler(0, 90, 0);
	            headingleft = !headingleft;
	        }
            // Rotate player if direction changes
            transform.rotation = Quaternion.Lerp(transform.rotation, targetrot, Time.deltaTime * 20f);

		    // set speed to horizontal inputs
	        speed = Mathf.Abs(input.x);
            speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
            anim.SetFloat("Speed", speed);

            // set sprinting
	        if ((Input.GetKeyDown(sprintJoystick) || Input.GetKeyDown(sprintKeyboard))&& input != Vector2.zero) isSprinting = true;
	        if ((Input.GetKeyUp(sprintJoystick) || Input.GetKeyUp(sprintKeyboard))|| input == Vector2.zero) isSprinting = false;
            anim.SetBool("isSprinting", isSprinting);

	        if (anim.applyRootMotion == false)
	        {
	            rigbody.velocity = new Vector3(input.x * moveSpeed, rigbody.velocity.y, 0f);
	        }

	        if (grounded == false)
	        {
	            if (rigbody.velocity.y > 0f)
	            {
	                capsule.center = capsuleCenterAir;
	                capsule.height = capsuleHeightAir;
	                if ((Input.GetKey(jumpJoystick) || Input.GetKey(jumpKeyboard)) == false)
	                {
	                    rigbody.velocity = new Vector3(rigbody.velocity.x, 0f, 0f);
	                }
	            }
	            else
	            {
	                capsule.center = capsuleCenter;
	                capsule.height = capsuleHeight;
	            }
            }
            else
	        {
	            capsule.center = capsuleCenter;
	            capsule.height = capsuleHeight;
	        }

            // Jump
            if ((Input.GetKeyDown(jumpJoystick) || Input.GetKeyDown(jumpKeyboard)) && grounded)
	        {
	            rigbody.velocity = new Vector3(input.x, jumpVelocity, 0f);
	        }

            anim.SetBool("IsGrounded",grounded);
            anim.SetFloat("Y_Speed", rigbody.velocity.y);
	    }

        public bool IsGrounded()
        {
            if (checkGroundForJump)
                return Physics.Raycast(transform.position, Vector3.down, groundTolerance, GroundMask.value);
            else
                return true;
        }


        private IEnumerator CoCastInvertWorld()
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            anim.Play("DoSwap");
            Time.timeScale = 0f;
            //rigbody.velocity = Vector3.zero;
            yield return new WaitForSecondsRealtime(SwapTime/2f);
            InvertManager.Instance.ToggleInvert();
            yield return new WaitForSecondsRealtime(SwapTime / 2f);
            Time.timeScale = 1f;
            anim.updateMode = AnimatorUpdateMode.Normal;
            castCoroutine = null;
        }

    }

}
