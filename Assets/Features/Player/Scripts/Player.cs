using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Переменные для управления
    private CharacterController characterController;
    private Vector3 velocity;

    // Камера
    public Transform playerCamera;
    public float mouseSensitivity = 2f;
    private float cameraPitch = 0f;

    // Скорость движения
    public float walkSpeed = 5f;
    public float jumpHeight = 1.5f;
    private float gravity = -9.81f;

    // Время "койота" для прыжков
    private float coyoteTime = 0.2f;
    private float lastGroundedTime;

    // Прозрачная капсула
    private GameObject capsuleObject;
    public float capsuleHeight = 2f;
    public float capsuleRadius = 0.5f;
    public Material transparentMaterial;  // Прозрачный материал

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Захват курсора

        // Создание прозрачной капсулы
        CreateTransparentCapsule();
    }

    void Update()
    {
        HandleCameraRotation();  // Управление камерой
        HandleMovement();        // Перемещение игрока
        HandleJump();            // Обработка прыжка
    }

    // Управление камерой (вращение)
    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Вращение персонажа по оси Y
        transform.Rotate(Vector3.up * mouseX);

        // Ограничение вращения камеры по вертикали
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
    }

    // Управление перемещением
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * walkSpeed * Time.deltaTime);

        // Применение гравитации
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
            lastGroundedTime = Time.time; // Обновляем время, когда игрок был на земле
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    // Обработка прыжка
    void HandleJump()
    {
        bool canJump = (Time.time - lastGroundedTime) <= coyoteTime;

        if (Input.GetButtonDown("Jump") && canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Создание прозрачной капсулы
    void CreateTransparentCapsule()
    {
        capsuleObject = GameObject.CreatePrimitive(PrimitiveType.Capsule); // Создаем капсулу
        capsuleObject.transform.SetParent(transform);  // Привязываем её к игроку
        capsuleObject.transform.localPosition = Vector3.zero;
        capsuleObject.transform.localScale = new Vector3(capsuleRadius, capsuleHeight / 2, capsuleRadius);

        // Добавляем прозрачный материал
        MeshRenderer renderer = capsuleObject.GetComponent<MeshRenderer>();
        if (transparentMaterial != null)
        {
            renderer.material = transparentMaterial;
        }
        else
        {
            renderer.material.color = new Color(1, 1, 1, 0.3f); // По умолчанию, если материал не задан
        }

        // Отключаем физику капсулы, чтобы она не мешала коллизии игрока
        Destroy(capsuleObject.GetComponent<Collider>());
    }
}

