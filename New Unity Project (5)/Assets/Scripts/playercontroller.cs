using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{

   public int coins = 0;
   public int capsule = 0;
   private Controles _controles;
   private PlayerInput _playerInput;
   private Camera _mainCamera;
   private Vector2 _moveInput;
   private Rigidbody _rigidbody;
   private bool _isGrounded;
   public float moveMultiplier;
   public float maxVelocity;
   public float rayDistance;
   public LayerMask layerMask;
   public float jumpForce;

   private void OnEnable()
   {
      _rigidbody = GetComponent<Rigidbody>();
      
      _controles = new Controles();
      
      _playerInput = GetComponent<PlayerInput>();
      
      _mainCamera = Camera.main;
      
      _playerInput.onActionTriggered += OnActionTriggered;
         
   }

   private void OnDisable()
   {
      _playerInput.onActionTriggered -= OnActionTriggered;
   }

   private void OnActionTriggered(InputAction.CallbackContext obj)
   {
      if (obj.action.name.CompareTo(_controles.gameplay.move.name) == 0)
      {
         _moveInput = obj.ReadValue<Vector2>();
      }

      if (obj.action.name.CompareTo(_controles.gameplay.Jump.name)== 0)
      {
         if (obj.performed) Jump();
         {
            
         }
      }
     
   }

   private void Move()
   {
      Vector3 camForward = _mainCamera.transform.forward;
      Vector3 camRight = _mainCamera.transform.right;
      camForward.y = 0;
      camRight.y = 0;
      
      
      
      _rigidbody.AddForce((camForward * _moveInput.y +
                          camRight * _moveInput.x ) * 
                           moveMultiplier * Time.fixedDeltaTime);
   }

   private void FixedUpdate()
   {
      Move();
      LimitVelocity();
   }

   private void LimitVelocity()
   {
      Vector3 velocity = _rigidbody.velocity;
      if (Mathf.Abs (velocity.x) > maxVelocity) velocity.x = Mathf.Sign(velocity.x)*maxVelocity;
      if (Mathf.Abs (velocity.z) > maxVelocity) velocity.z = Mathf.Sign(velocity.z)*maxVelocity;

      _rigidbody.velocity = velocity;
   }

   private void CheckGround()
   {
      RaycastHit collision;
      if (Physics.Raycast(transform.position, Vector3.down,out collision, rayDistance, layerMask ))
      {
         _isGrounded = true;
      }
      else
      {
         _isGrounded = false;
      }
   }

   private void Jump()
   {
      if (_isGrounded)
      {
         _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      }
   }

   private void Update()
   {
      CheckGround();
   }

   private void OnDrawGizmos()
   {
      Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.yellow);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Coin"))
      {
         coins++;
         PlayerObserverManager.CoinsChanged(coins);
         Destroy(other.gameObject);
 
      }

      if (other.CompareTag("capsule"))
      {
         capsule++;
         PlayerObserverManager.capsuleChanged(capsule);
         Destroy(other.gameObject);
      }
   }
   
}   

  