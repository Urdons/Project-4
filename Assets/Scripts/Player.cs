using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Camera camera;
    public float cameraDistance = 10f;
    private Vector3 cameraRotation = new Vector3(0, 0, 0);

    public float maxSpeed = 50f;
    public float jumpPower = 10f;
    public float gravity = -1f;
    public float friction = 0.5f;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool playerOnGround;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference mouse;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerOnGround = characterController.isGrounded;
        if (playerOnGround && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += gravity;

        Vector2 movementInput = Vector2.ClampMagnitude(moveAction.action.ReadValue<Vector2>(), 1);
        Vector3 movement = Quaternion.AngleAxis(-cameraRotation.y - 90, Vector3.forward) * new Vector3(-movementInput.y, 0, 0);
        movement += Quaternion.AngleAxis(-cameraRotation.y, Vector3.forward) * new Vector3(movementInput.x, 0, 0);
        movement *= maxSpeed;

        Vector2 horizontalVelocity = new Vector2(velocity.x + (movement.x - velocity.x) * friction, velocity.z + (movement.y - velocity.z) * friction);
        velocity.x = horizontalVelocity.x;
        velocity.z = horizontalVelocity.y;

        if (transform.position.y < -20)
        {
            velocity = Vector3.zero;
            transform.position = new Vector3(0, 5, 0);
        }
    }

    void Update()
    {
        if (playerOnGround && jumpAction.action.triggered)
        {
            velocity.y = jumpPower;
        }

        characterController.Move(velocity * Time.deltaTime);

        Vector2 mouseMovement = mouse.action.ReadValue<Vector2>();
        cameraRotation += new Vector3(mouseMovement.y, mouseMovement.x, 0);
        cameraRotation = new Vector3(math.clamp(cameraRotation.x, -80, 80), cameraRotation.y, 0);

        camera.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);

        Vector3 cameraOffset = new Vector3(MathF.Cos(math.radians(cameraRotation.x)), MathF.Sin(math.radians(cameraRotation.x)), MathF.Cos(math.radians(cameraRotation.x)));
        cameraOffset = new Vector3(cameraOffset.x * -MathF.Sin(math.radians(cameraRotation.y)), cameraOffset.y, cameraOffset.z * -MathF.Cos(math.radians(cameraRotation.y)));
        cameraOffset *= cameraDistance;

        camera.transform.position = characterController.transform.position + new Vector3(0, 2, 0) + cameraOffset;
    }
}
