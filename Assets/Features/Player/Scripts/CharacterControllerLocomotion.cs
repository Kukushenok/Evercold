using UnityEngine;
using System.Collections.Generic;

public interface IPlayerLocomotionFeature
{
    public void LocomotionFixedUpdate(BasePlayerLocomotion loc);
    public void LocomotionUpdate(BasePlayerLocomotion loc);
}
public abstract class PlayerLocomotionFeature: IPlayerLocomotionFeature
{
    public abstract void LocomotionFixedUpdate(BasePlayerLocomotion loc);
    public abstract void LocomotionUpdate(BasePlayerLocomotion loc);
}

public abstract class BasePlayerLocomotion : MonoBehaviour
{
    public Camera playerCamera; // ������ �� ������� ����

    public abstract Vector3 DesiredDeltaPos { get; set; }
    public abstract Vector3 CameraRotation { get; set; }
    public abstract void Jump();

    [SerializeReference, SubclassSelector]
    private List<IPlayerLocomotionFeature> _playerFeatures = new List<IPlayerLocomotionFeature>();

    protected abstract void OnLocomotionUpdate();
    protected abstract void OnLocomotionFixedUpdate();

    private void FixedUpdate()
    {
      
        foreach (var feature in _playerFeatures)
        {
            feature.LocomotionFixedUpdate(this);
        }
        OnLocomotionFixedUpdate();
    }

    private void Update()
    {
       
        foreach (var feature in _playerFeatures)
        {
            feature.LocomotionUpdate(this);
        }
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

    private Vector3 cameraRotation;
    public override Vector3 DesiredDeltaPos { get; set; }
   

    public override Vector3 CameraRotation
    {
        get => cameraRotation;
        set => cameraRotation = value;
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

       

        
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            playerCamera.transform.SetParent(transform);
            playerCamera.transform.localPosition = new Vector3(0, 1.6f, 0); // ������� ������ �� ������ ������
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


        transform.rotation = Quaternion.Euler(0f, CameraRotation.y, 0f);


       
        
            playerCamera.transform.localRotation = Quaternion.Euler(CameraRotation.x, 0f, 0f);

            
        
    }

    protected override void OnLocomotionFixedUpdate()
    {
       
    }
}



[System.Serializable]
public class CoyoteTimeJumpLocomotionFeature : PlayerLocomotionFeature
{
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    public override void LocomotionFixedUpdate(BasePlayerLocomotion loc)
    {
        // ����������� ����������� ������ ��� ������ ��� ����� coyote time
    }

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
    private float horizontalRotation = 0f;

    public override void LocomotionFixedUpdate(BasePlayerLocomotion loc) { }

    public override void LocomotionUpdate(BasePlayerLocomotion loc)
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // ��������� �������������� � ������������ �������
        horizontalRotation += mouseX;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        // ��������� CameraRotation � ������� ������
        loc.CameraRotation = new Vector3(verticalRotation, horizontalRotation);
    }
}
