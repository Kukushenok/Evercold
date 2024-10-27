using UnityEngine;
using System.Collections.Generic;

public abstract class PlayerLocomotionFeature
{
    public abstract void LocomotionFixedUpdate(BasePlayerLocomotion loc);
    public abstract void LocomotionUpdate(BasePlayerLocomotion loc);
}

public abstract class BasePlayerLocomotion : MonoBehaviour
{
    public Camera playerCamera; // Камера от первого лица

    public abstract Vector3 DesiredDeltaPos { get; set; }
    public abstract Vector3 CameraRotation { get; set; }
    public abstract void Jump();

    [SerializeReference, SubclassSelector]
    private List<PlayerLocomotionFeature> _playerFeatures = new List<PlayerLocomotionFeature>();

    protected virtual void OnLocomotionFixedUpdate()
    {
        foreach (var feature in _playerFeatures)
        {
            feature.LocomotionFixedUpdate(this);
        }
    }

    protected virtual void OnLocomotionUpdate()
    {
        foreach (var feature in _playerFeatures)
        {
            feature.LocomotionUpdate(this);
        }
    }

    private void FixedUpdate()
    {
        OnLocomotionFixedUpdate();
    }

    private void Update()
    {
        OnLocomotionUpdate();
    }
}

public class CharacterControllerLocomotion : BasePlayerLocomotion
{
    private CharacterController _controller;
    private Vector3 _velocity;
    protected internal bool _isGrounded; 
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    public override Vector3 DesiredDeltaPos { get; set; }
    public override Vector3 CameraRotation { get; set; }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        // Настройка камеры от первого лица, если её нет в объекте игрока
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            playerCamera.transform.SetParent(transform);
            playerCamera.transform.localPosition = new Vector3(0, 1.6f, 0); // Позиция камеры на уровне головы
            playerCamera.transform.localRotation = Quaternion.identity;
        }
    }

    public override void Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    protected override void OnLocomotionUpdate()
    {
        _isGrounded = _controller.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        Vector3 move = DesiredDeltaPos;
        _controller.Move(move * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}

[System.Serializable]
public class JumpLocomotionFeature : PlayerLocomotionFeature
{
    public override void LocomotionFixedUpdate(BasePlayerLocomotion loc) { }

    public override void LocomotionUpdate(BasePlayerLocomotion loc)
    {
        if (CanJump(loc) && Input.GetButtonDown("Jump"))
        {
            loc.Jump();
        }
    }

    protected virtual bool CanJump(BasePlayerLocomotion loc)
    {
        return ((CharacterControllerLocomotion)loc)._isGrounded;
    }
}

[System.Serializable]
public class CoyoteTimeJumpLocomotionFeature : JumpLocomotionFeature
{
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public override void LocomotionUpdate(BasePlayerLocomotion loc)
    {
        CharacterControllerLocomotion locomotion = (CharacterControllerLocomotion)loc;
        if (locomotion._isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0 && Input.GetButtonDown("Jump"))
        {
            loc.Jump();
        }
    }

    protected override bool CanJump(BasePlayerLocomotion loc)
    {
        return true;
    }
}

[System.Serializable]
public class MoveLocomotionFeature : PlayerLocomotionFeature
{
    public float speed = 5f;

    public override void LocomotionFixedUpdate(BasePlayerLocomotion loc) { }

    public override void LocomotionUpdate(BasePlayerLocomotion loc)
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ);
        loc.DesiredDeltaPos = loc.transform.TransformDirection(move) * speed;
    }
}

[System.Serializable]
public class CameraLocomotionFeature : PlayerLocomotionFeature
{
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;

    public override void LocomotionFixedUpdate(BasePlayerLocomotion loc) { }

    public override void LocomotionUpdate(BasePlayerLocomotion loc)
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Вращение игрока по горизонтали
        loc.transform.Rotate(Vector3.up * mouseX);

        // Вращение камеры по вертикали
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        if (loc.playerCamera != null)
        {
            loc.playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
}
