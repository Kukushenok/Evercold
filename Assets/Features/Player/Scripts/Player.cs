using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ���������� ��� ����������
    private CharacterController characterController;
    private Vector3 velocity;

    // ������
    public Transform playerCamera;
    public float mouseSensitivity = 2f;
    private float cameraPitch = 0f;

    // �������� ��������
    public float walkSpeed = 5f;
    public float jumpHeight = 1.5f;
    private float gravity = -9.81f;

    // ����� "������" ��� �������
    private float coyoteTime = 0.2f;
    private float lastGroundedTime;

    // ���������� �������
    private GameObject capsuleObject;
    public float capsuleHeight = 2f;
    public float capsuleRadius = 0.5f;
    public Material transparentMaterial;  // ���������� ��������

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // ������ �������

        // �������� ���������� �������
        CreateTransparentCapsule();
    }

    void Update()
    {
        HandleCameraRotation();  // ���������� �������
        HandleMovement();        // ����������� ������
        HandleJump();            // ��������� ������
    }

    // ���������� ������� (��������)
    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // �������� ��������� �� ��� Y
        transform.Rotate(Vector3.up * mouseX);

        // ����������� �������� ������ �� ���������
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
    }

    // ���������� ������������
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * walkSpeed * Time.deltaTime);

        // ���������� ����������
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
            lastGroundedTime = Time.time; // ��������� �����, ����� ����� ��� �� �����
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    // ��������� ������
    void HandleJump()
    {
        bool canJump = (Time.time - lastGroundedTime) <= coyoteTime;

        if (Input.GetButtonDown("Jump") && canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // �������� ���������� �������
    void CreateTransparentCapsule()
    {
        capsuleObject = GameObject.CreatePrimitive(PrimitiveType.Capsule); // ������� �������
        capsuleObject.transform.SetParent(transform);  // ����������� � � ������
        capsuleObject.transform.localPosition = Vector3.zero;
        capsuleObject.transform.localScale = new Vector3(capsuleRadius, capsuleHeight / 2, capsuleRadius);

        // ��������� ���������� ��������
        MeshRenderer renderer = capsuleObject.GetComponent<MeshRenderer>();
        if (transparentMaterial != null)
        {
            renderer.material = transparentMaterial;
        }
        else
        {
            renderer.material.color = new Color(1, 1, 1, 0.3f); // �� ���������, ���� �������� �� �����
        }

        // ��������� ������ �������, ����� ��� �� ������ �������� ������
        Destroy(capsuleObject.GetComponent<Collider>());
    }
}

