using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class Ball : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveForce = 15f; 
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float counterForce = 5f; // Para frenar rápido
    [SerializeField] private float airControlMultiplier = 0.3f; // Control en el aire
    [SerializeField] private float jumpImpulse = 6f;

    [Header("Detection")]
    [SerializeField] private float groundCheckRadius = 0.55f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private PlayerInput playerInput;
    private Camera mainCamera;
    private float score = 0.0f;
    
    private Vector2 inputVector;
    private bool isGrounded;
    private bool cambiandoDeEscena = false;

    public void PrepararCambioEscena() => cambiandoDeEscena = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        mainCamera = Camera.main;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        // Aumentamos el arrastre angular para que la rotación sea más natural
        rb.angularDamping = 2f; 
    }

    private void OnEnable()
    {
        playerInput.actions["Move"].performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        playerInput.actions["Move"].canceled += ctx => inputVector = Vector2.zero;
        playerInput.actions["Jump"].started += OnJump;
    }

    private void OnDisable()
    {
        playerInput.actions["Jump"].started -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            // Limpiamos velocidad vertical antes de saltar para que el salto sea constante
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);
    }

    private void FixedUpdate()
    {
        ApplyResponsiveMovement();
        LimitVelocity();
    }

    private void ApplyResponsiveMovement()
    {
        // 1. Calcular dirección relativa a la cámara
        Vector3 camForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(mainCamera.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = (camForward * inputVector.y + camRight * inputVector.x).normalized;

        // 2. Si no hay input, aplicar fuerza de frenado para mayor precisión
        if (inputVector.magnitude < 0.1f && isGrounded)
        {
            Vector3 horizontalVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(-horizontalVel * counterForce, ForceMode.Acceleration);
            return;
        }

        // 3. Aplicar fuerza de movimiento
        float currentForce = isGrounded ? moveForce : moveForce * airControlMultiplier;
        rb.AddForce(moveDirection * currentForce, ForceMode.Acceleration);

        // 4. Torque Visual (opcional, hace que ruede según el movimiento)
        Vector3 torqueAxis = Vector3.Cross(Vector3.up, moveDirection);
        rb.AddTorque(torqueAxis * moveForce, ForceMode.Acceleration);
    }

    private void LimitVelocity()
    {
        Vector3 horizontalVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        
        if (horizontalVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = horizontalVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    // --- Colisiones ---
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Muro thisWall))
            thisWall.UpdateHealth(other.relativeVelocity.magnitude);
        
        if (other.gameObject.TryGetComponent(out SpringJoint thisSpring))
            thisSpring.spring = 500;
        
        if (other.gameObject.TryGetComponent(out Meta thisMeta))
        {
            PrepararCambioEscena();
            GameSceneManager.Instance.CargarEscena("Victoria");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Coin thisCoin))
        {
            this.score += thisCoin.ConsumeCoin();
            if(UIManager.Instance != null)
                UIManager.Instance.ScoreText.SetText("Score: " + this.score);
            Destroy(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (GameSceneManager.Instance != null && !cambiandoDeEscena)
        {
            GameSceneManager.Instance.PerderVida();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }
}