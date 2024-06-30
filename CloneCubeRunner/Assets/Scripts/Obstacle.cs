using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed;
    GameController m_gc;
    // Start is called before the first frame update
    void Start()
    {
        // tham chiếu đến game controller >> để gọi các phương thức trong game controller
        m_gc = FindAnyObjectByType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        // obstacleLeftTo();
        obstacleRightTo();
    }
    //xử lý tiếp khi vật thể var vào screenLimit thì reset lại 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ScreenLimit"))
        {
            Debug.Log("var vao ScreenLimit");
            m_gc.ScoreIncrenment();


            Destroy(gameObject);
        }

    }
    private void obstacleLeftTo()
    {
        transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;
    }
    private void obstacleRightTo()
    {
        // xứ lý dần về phái tai trái [Vector3.left => viết tắt của (-1,0,0) * 5 = (-5,1,1)] //
        transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;

    }
}
