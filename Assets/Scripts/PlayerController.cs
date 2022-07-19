using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Input Fields

    private PlayerControllerActions playerActionsAsset;
    private InputAction move;

    // Movement Fields -> SerializeField umo�liwia serializowanie nie tylko public, a dodatkowo private field
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jumpForce = 5f; // skok jeszcze nie uko�czony :)
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;
    private Animator animator;
    private GameManager gm;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerActionsAsset = new PlayerControllerActions();
        animator = this.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerActionsAsset.Player.Attack.started += DoAttack;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero; // Resetuje warto�� do zera, �eby posta� nie przyspiesza�a dalej po puszczeniu przycisku

        Vector3 HorizontalVelocity = rb.velocity;
        HorizontalVelocity.y = 0;
        if (HorizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = HorizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y; // Je�li gracz przekroczy maksymaln� pr�dko��
        lookAt();
    }

    private void lookAt() // Zapobiega problemowi, gdzie posta� rotuje w r�nych kierunkach podczas odbijania si� od kolizji itd.
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f) // Jesli posta� jest w ruchu
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized; 
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("attack");
  
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Attack.started -= DoAttack;
        playerActionsAsset.Player.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Danger"))
        {
            other.gameObject.SetActive(false);
        }
    }
}