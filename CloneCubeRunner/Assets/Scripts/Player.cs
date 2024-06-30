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
    void Start()
    {
        m_rd = GetComponent<Rigidbody2D>(); // GetComponent để lấy tất cả thành phần ở trong 9 đối tượng  muốn truy vấn
        m_gc = FindAnyObjectByType<GameController>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
            if (aus && loseSound)
            {
                aus.PlayOneShot(loseSound);
            }
            m_gc.SetGameover(true);
            Debug.Log("Var r");
        }
        if (col.gameObject.CompareTag("DeathZone"))
        {
            if (aus && loseSound)
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
}
