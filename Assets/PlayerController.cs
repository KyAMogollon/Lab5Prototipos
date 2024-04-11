using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public float forceJump;
    private Action dasheo;
    public float speed;
    private Func<bool> isSuelo;
    Rigidbody2D _rb;
    public Vector2 movementPlayer;
    public LayerMask mylayermask;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
        Move();
        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.green);
    }
    public void Movement(InputAction.CallbackContext value)
    {
        float movement = value.ReadValue<float>();
        movementPlayer = new Vector2(movement, 0);
    }
    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Debug.Log("Salte");
            ValidateJump(isSuelo);
        }
    }
    public void OnDash(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Dash();
        }
    }
    void Move()
    {
        _rb.velocity = new Vector2(movementPlayer.x * speed, _rb.velocity.y);
    }
    void ValidateJump(Func<bool>isSuelo)
    {
        isSuelo = () => { return Physics2D.Raycast(transform.position, Vector2.down, 1.5f, mylayermask); };
        
        if (isSuelo.Invoke())
        {
            _rb.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        }
        
    }
    
    void Dash()
    {
        if (movementPlayer.x > 0)
        {
            Debug.Log("Entro a la condicion");
            dasheo = () => { transform.position = new Vector2(transform.position.x + 3, transform.position.y); };
            

        }
        else if (movementPlayer.x < 0)
        {
            dasheo = () => { transform.position = new Vector2(transform.position.x - 3, transform.position.y); };
            
        }

        dasheo?.Invoke();
    }
}
