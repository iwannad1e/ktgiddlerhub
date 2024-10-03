using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Controler : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    [Space]
    [SerializeField] private LayerMask groundLayerMask;
    [field: SerializeField] public Color accentColor { get; private set; }
    [SerializeField] private Color defaultColor;
    private Rigidbody2D rb;
    private bool isGrounded;
    private GameManager gameManager; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>(); 
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            Move(horizontalInput * movementSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move(float moveSpeed) =>
        rb.AddForce(new Vector2(moveSpeed, 0));

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetGrounded(collision, true);
        ColorTile(collision.gameObject.GetComponent<SpriteRenderer>());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetGrounded(collision, false);
    }

    private void SetGrounded(Collision2D collision, bool isGrounded)
    {
        if (LayerMaskUtil.LayerMaskContainsLayer(groundLayerMask, collision.gameObject.layer))
        {
            this.isGrounded = isGrounded;
        }
    }

    private void ColorTile(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer.color == defaultColor)
        {
            spriteRenderer.color = accentColor;
        }
        else
        {
            spriteRenderer.color = defaultColor;
        }

        
        gameManager.CheckTilesColor();
    }
}