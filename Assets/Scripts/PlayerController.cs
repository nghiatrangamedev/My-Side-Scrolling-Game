using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D _playerRb;

    float _speed = 20.0f;
    float _jumpForece = 5.0f;
    float _score = 0;

    float _horizontalInput;
    float _verticalInput;
    

    bool _isOnGround = false;


    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        Jump();
    }

    void PlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    void PlayerMovement()
    {
        if (_horizontalInput != 0)
        {
            Debug.Log(transform.right);
            _playerRb.AddForce(transform.right * _horizontalInput * _speed);
        }
        
    }

    void Jump()
    {
        if (_verticalInput > 0 && _isOnGround)
        {
            _playerRb.AddForce(Vector2.up * _jumpForece, ForceMode2D.Impulse);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isOnGround = true;
        }

        if (collision.gameObject.tag == "WinPosition")
        {
            Debug.Log("You win");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isOnGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gold")
        {
            Destroy(collision.gameObject);
            _score += 5;
            Debug.Log("Score: " + _score);
        }
    }
}
