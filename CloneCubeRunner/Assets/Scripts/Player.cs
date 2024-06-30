using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D m_rd; //tham   chiếu đến rigidbody2D trên Unity
    bool m_isGround;
    public float jumpForce; // Lực nhảy
    GameController m_gc;

    // Start is called before the first frame update
    void Start()
    {
        m_rd = GetComponent<Rigidbody2D>(); // GetComponent để lấy tất cả thành phần ở trong 9 đối tượng  muốn truy vấn
        m_gc = FindAnyObjectByType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumpKeyPressed = Input.GetKeyDown(KeyCode.Space);
        if (isJumpKeyPressed && m_isGround)
        {

            m_rd.AddForce(Vector2.up * jumpForce); // Thêm 1 lực từ bàn phím vào --- LÚC NÀY VẤN ĐỀ ở trên không vẫn nhãy
            m_isGround = false; // trên Không được nhảy
        } // click nut space
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
            m_gc.SetGameover(true);
            Debug.Log("Var r");
        }
    }

}
