using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;
	[SerializeField] float jogSpeed = 1.5f;

	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;

	Animator m_Animator;
	Rigidbody m_Rigidbody;
	//float m_OrigGroundCheckDistance;
	const float k_Half = 0.5f;
	float m_TurnAmount;
	float m_ForwardAmount;
	Vector3 m_GroundNormal;
	float m_CapsuleHeight;
	Vector3 m_CapsuleCenter;
	CapsuleCollider m_Capsule;
	bool m_Crouching;
	public bool stayThere;
	public bool keyPressed;
	public bool isMoving;
	public PhysicMaterial zeroFriction;
	public PhysicMaterial maxFriction;

	void OnEnable()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Start()
	{
		// get the transform of the main camera
		if (Camera.main != null)
		{
			m_Cam = Camera.main.transform;
		}
		else
		{
			Debug.LogWarning(
				"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
			// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
		}


		
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
		m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;

		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}


	public void Move(Vector3 move, bool crouch)
	{

		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired direction.
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        if (move.x == 0 && move.z == 0) m_TurnAmount = 0f;
        if (m_TurnAmount == Mathf.PI) m_TurnAmount = 0f;
        m_ForwardAmount = move.z;

        ApplyExtraTurnRotation();
        
        // send input and other state parameters to the animator
        UpdateAnimator(move);
        
    }


	void UpdateAnimator(Vector3 move)
	{
		// update the animator parameters
		m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		//m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
		//m_Animator.SetBool("Crouch", m_Crouching);

		// calculate which leg is behind, so as to leave that leg trailing in the jump animation
		// (This code is reliant on the specific run cycle offset in our animations,
		// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		float runCycle =
			Mathf.Repeat(
				m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;

		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (move.magnitude > 0)
		{
			m_Animator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			// don't use that while airborne
			m_Animator.speed = 1;
		}
	}


	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{
        if (this.gameObject.tag == "Player")
        {

            // read inputs
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }

            // pass all parameters to the character control script
            Move(m_Move, crouch);

			//serve a simulare un aumento di velocità (Jog)
			if (Input.GetKey (KeyCode.LeftShift) || (Input.GetAxis("LeftTriggerJoystick") <= -0.001)) {
				Debug.Log ("CORRI!");
				//m_Animator.SetBool ("Run", true);
				m_AnimSpeedMultiplier = jogSpeed;
				//m_MoveSpeedMultiplier = JogSpeed;
				//GameManager.Self.GetComponent<AudioContainer>().d
			} else {
				//m_Animator.SetBool ("Run", false);
				m_AnimSpeedMultiplier = 1f;
				//m_MoveSpeedMultiplier = 1f;

			}

        }

		//Serve a disattivare il parametro collision quando iniziamo un nuovo movimento. 
		//Subito dopo controlliamo se in verità non siamo ancora contro una collision e in caso facciamo partire di nuovo la coroutine di blocco.
		if (this.gameObject.tag == "Player") {
			if (Input.GetAxis("Horizontal") != 0) {
				m_Animator.SetBool ("Collision", false);
				if (stayThere) {
					StartCoroutine (StopMove (1f));
				}
			}
		}

	}


    IEnumerator StartMove (sbyte dir)
    {
        if (!isMoving)
        {
            transform.forward = GameObject.FindGameObjectWithTag("CameraRig").transform.forward * dir;
            yield return null;
        }
    }


    IEnumerator MoveLateral (sbyte dir)
    {
        if (!isMoving)
        {
            transform.forward = GameObject.FindGameObjectWithTag("CameraRig").transform.right * dir;
            yield return null;
        }
    }


	void OnCollisionEnter(Collision collision) {
		if (this.gameObject.tag == "Player") {
			
			//Blocca il personaggio quando collide con qualsiasi cosa che non sia il pavimento così da evitare che continui a camminare contro una parete ad es.
			if (collision.transform.tag != "Floor") {
				//Debug.Log ("COLLISION");
				StartCoroutine (StopMove (0.2f));
				stayThere = true;
			}

			//Cambia i materiali dei collider quando collide con un NPC per evitare che i personaggi si spostino a vicenda o il player ci scivoli sopra andando verso l'alto
			if (collision.transform.tag == "ControllableNPC") {
				//Debug.Log ("CHANGE MATERIAL ON COLLISION ENTER");
				m_Rigidbody.isKinematic = true;
				m_Rigidbody.Sleep ();

				this.transform.GetComponent<CapsuleCollider> ().material = zeroFriction;
				collision.transform.GetComponent<CapsuleCollider> ().material = zeroFriction;

				m_Rigidbody.isKinematic = false;
				m_Rigidbody.WakeUp ();
			}

		}

	}


	void OnCollisionExit(Collision collision) {
		if (this.gameObject.tag == "Player") {
			if (collision.transform.name != "Floor") {
				m_Animator.SetBool ("Collision", false);
				stayThere = false;
			}

			//Ripristina i materiali dei collider quando si finisce di collidere con un NPC per evitare che i personaggi scivolino dopo essersi mossi
			if (collision.transform.tag == "ControllableNPC") {
				//Debug.Log ("CHANGE MATERIAL ON COLLISION EXIT");
				m_Rigidbody.isKinematic = true;
				m_Rigidbody.Sleep ();

				this.transform.GetComponent<CapsuleCollider> ().material = maxFriction;
				collision.transform.GetComponent<CapsuleCollider> ().material = maxFriction;

				m_Rigidbody.isKinematic = false;
				m_Rigidbody.WakeUp ();
			}
		}
	}


	public IEnumerator StopMove (float delay) {
		
		//Debug.Log ("STOP MOVE");
		yield return new WaitForSeconds (delay);
		if (stayThere) {
			m_Animator.SetBool ("Collision", true);
		}
	}


	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}

    
	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if (Time.deltaTime > 0)
		{
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}

}
