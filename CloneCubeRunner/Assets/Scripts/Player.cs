using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D m_rd; //tham   chiếu đến rigidbody2D trên Unity
    bool m_isGround;
    public float jumpForce; // Lực nhảy
    GameController m_gc;


    // xử lý phần di chuyển Player
    public float moveSpeed;
    private Animator m_animator;
    private float moveLeftRight;

    // Xử lý quay đầu vật thể 
    private bool isFacingRight = true;


    //xử lý audio
    public AudioSource aus;
    public AudioClip jumpSound;
    public AudioClip loseSound;

    // Start is called before the first frame update

    //xử lý move btn 
    private bool moveLeft, moveRight;
    void Start()
    {
        m_rd = GetComponent<Rigidbody2D>(); // GetComponent để lấy tất cả thành phần ở trong 9 đối tượng  muốn truy vấn
        m_gc = FindAnyObjectByType<GameController>();
        m_animator = GetComponent<Animator>();

        //xử lý btn left/right 
        moveLeft = false;
        moveRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        bool isJumpKeyPressed = Input.GetKeyDown(KeyCode.Space);
        if (isJumpKeyPressed && m_isGround)
        {

            m_rd.AddForce(Vector2.up * jumpForce); // Thêm 1 lực từ bàn phím vào --- LÚC NÀY VẤN ĐỀ ở trên không vẫn nhãy
            m_isGround = false; // trên Không được nhảy


            //xử lý sound
            if (aus && jumpSound)
            {
                aus.PlayOneShot(jumpSound);//sound có độ dày ngắn
            }
        } // click nut space


        //move
        moveLeftRight = Input.GetAxis("Horizontal");
        m_rd.velocity = new Vector2(moveLeftRight * moveSpeed, m_rd.velocity.y);
        //Flip
        flipPlay();
        //animation
        m_animator.SetFloat("move", Mathf.Abs(moveLeftRight)); // kiểm tra biến khai bao trong animator Unity (Unity) && giá trị tuyệt đối moveLeftRight truyền vào

        //xử lý riêng cho việc btn trên Unity
        if (moveLeft)
        {
            m_rd.velocity = new Vector2(-moveSpeed, m_rd.velocity.y);
        }
        if (moveRight)
        {
            m_rd.velocity = new Vector2(moveSpeed, m_rd.velocity.y);
        }
        //attack
        bool isAttackPressed = Input.GetKeyDown(KeyCode.F);
        if (isAttackPressed)
        {
            m_animator.SetTrigger("attack");
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    // OnCollisionEnter2D bất sự kiện 2 vật thể va vào nhau nhưng không xuyên qua nhau 
    {
        if (col.gameObject.CompareTag("Ground")) // xử lý điều kiện vật thể với tab [vật thể nền] va vào nhau 
        {
            m_isGround = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D col)//-- Ontrigger thì xuyên qua nhau
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            //ý tưởng game kết thúc = cook
            if (aus && loseSound && !m_gc.isGameover())
            {
                aus.PlayOneShot(loseSound);
            }
            m_gc.SetGameover(true);
            Debug.Log("Var r");
        }
        if (col.gameObject.CompareTag("DeathZone"))
        {
            if (aus && loseSound && !m_gc.isGameover())
            {
                aus.PlayOneShot(loseSound);
            }
            m_gc.SetGameover(true);
            Debug.Log("Drop Death");
        }
    }
    // Xử lý quay đầu vật thể 
    private void flipPlay()
    {

        //ý tưởng là thay đổi giá trị x (1, -1) thì player sẽ quay trái phải
        if (isFacingRight && moveLeftRight < 0 || !isFacingRight && moveLeftRight > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 transf = transform.localScale;
            transf.x = transf.x * -1;

            transform.localScale = transf;

        }
    }
    //xử lý btn trên mobile
    public void JumpMob()
    {
        if (!m_isGround)
            return;

        m_rd.AddForce(Vector2.up * jumpForce); // Thêm 1 lực từ bàn phím vào --- LÚC NÀY VẤN ĐỀ ở trên không vẫn nhãy
        m_isGround = false; // trên Không được nhảy


        //xử lý sound
        if (aus && jumpSound)
        {
            aus.PlayOneShot(jumpSound);//sound có độ dày ngắn
        }
    }
    public void moveLeftMob()
    {
        moveLeft = true;
    }
    public void moveRightMob()
    {
        moveRight = true;
    }
    public void StopMoveMob()
    {
        moveLeft = false;
        moveRight = false;
        m_rd.velocity = Vector2.zero;
    }
}
