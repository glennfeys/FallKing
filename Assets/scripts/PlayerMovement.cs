using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    private const float max_velocity = 5.0f;
    [SerializeField] private float die_velocity = 7.0f;

    private Vector2 player_start_pos = new Vector2(-6.66f, 3.33f);
    private Vector3 cam_start_pos = new Vector3(0.0f, 0.0f, -10.0f);

    private bool isGrounded = false;

    public Camera mainCamera;
    private float jumpPower = 0;
    private float maxJumpPower = 1200;
    private bool spaceDown = false;

    public LayerMask PlatformLayer;

    public CapsuleCollider2D collider;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        //body.rotation = body.rotation + Input.GetAxis("Horizontal") * speed;
        //TODO if velo
        if (Mathf.Abs(body.velocity.x) < max_velocity) {
            float airMultiply = isGrounded ? 1.0f : 0.5f;
            body.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed * airMultiply, 0));
        }
            
        if (Input.GetKeyDown(KeyCode.Space) || spaceDown) {
            if (!spaceDown) {
                spaceDown = true;
                jumpPower = 500;
            }
            
            if (jumpPower < maxJumpPower)
                jumpPower++;
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            if (IsGrounded() && transform.up.y > 0) {
                Debug.Log(new Vector2(transform.up.x, transform.up.y) * jumpSpeed * (jumpPower/maxJumpPower));
                body.AddForce(new Vector2(transform.up.x, transform.up.y) * jumpSpeed * (jumpPower/maxJumpPower));
            }
            spaceDown = false;
            jumpPower = 0;
        }
            
    }

    bool IsGrounded() {
        float extraHeight = 1.2f;
        RaycastHit2D hit = Physics2D.Raycast(collider.bounds.center, -transform.up, collider.bounds.extents.y + extraHeight, PlatformLayer);
        Debug.DrawRay(collider.bounds.center, -transform.up * (collider.bounds.extents.y + extraHeight));
        return hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D col)
     {
        isGrounded = true;
        if (col.relativeVelocity.y > die_velocity) {
            die();
            Debug.Log(col.relativeVelocity);
        }
            
        
     }
 
     void OnCollisionExit2D(Collision2D col)
     {
        isGrounded = false;
     }

     void die() {
        Debug.Log("YOU DIED!!");
        StartCoroutine(sleep(2));
        body.transform.position = player_start_pos;
        mainCamera.transform.position = cam_start_pos;
     }

    IEnumerator sleep(int seconds)
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(seconds);
    }
}
