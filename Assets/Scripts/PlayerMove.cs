using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class PlayerMove : MonoBehaviour
{
    //Varibles del Jugador
    public float SPEED = 0.5f;
    private float GRAVITY = -9.81f;
    private Vector3 direccion;
    private Vector3 Fallingvelocity;
    private Vector3 velocidad;

    //Entradas del jugador
    public InputActionAsset moveAction;
    private InputAction move;
    Vector2 moveInput;

    //Componentes del jugador
    private CharacterController controller;
    private Camera playerCamera;
    private XROrigin xrOrigin;


    void OnEnable()
    {
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }
     void Awake()
    {
        move = moveAction.FindAction("Move");
        controller = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
    }

    void Start()
    {
        playerCamera = xrOrigin.Camera;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = move.ReadValue<Vector2>();

        Move();
        Gravity();
        controller.Move(new Vector3(velocidad.x, Fallingvelocity.y * Time.deltaTime, velocidad.z));
    }

    //Movimiento del jugador
    void Move()
    {
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;
        forward.y = 0f; right.y = 0f;
        forward.Normalize(); right.Normalize();

        Vector3 velocity = (right * moveInput.x + forward * moveInput.y) * SPEED;
        velocidad = velocity;
    }

    //Gravedad
    void Gravity()
    {
        if(controller.isGrounded && Fallingvelocity.y < 0)
        {
            Fallingvelocity.y = -2f;
        }
        else
        {
            Fallingvelocity.y += GRAVITY * Time.deltaTime;
        }
    }
}
