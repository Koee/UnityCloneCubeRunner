using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject obstacle;
    public float spawTime;
    float m_spawTime;
    int m_score;
    bool m_isGameover;
    // Start is called before the first frame update
    void Start()
    {
        m_spawTime = 0; // khởi tạo ban đầu
    }

    // Update is called once per frame
    void Update()
    {
        // nếu gameover thì reset
        if (m_isGameover)
        {
            m_spawTime = 0;
            return; // return là dừng tắt cả các hoạt động phía dưới
        }

        m_spawTime -= Time.deltaTime;

        //thời gian lùi về 0 thì tạo mới
        if (m_spawTime <= 0)
        {
            // khởi tạo 
            SpawObsacle();

            //reset time
            m_spawTime = spawTime;
        }
    }
    // Phuong thuc tao ra cac chuong ngai vat 
    public void SpawObsacle()
    {
        //random tọa độ posY
        float randYpos = Random.Range(-4.4f, -3.7f);
        Vector2 spawnPos = new Vector2(10.76f, randYpos);

        //kiểm tra  vật thể phải được gán # null
        if (obstacle)
        {
            Instantiate(obstacle, spawnPos, Quaternion.identity);
        }//Quaternion.identity : không xoay 
        //Instantiate : khởi tạo giá trị
    }

    public void SetScore(int value) { m_score = value; }
    public int GetScore() { return m_score; }
    public void ScoreIncrenment() { m_score++; }

    public bool isGameover() { return m_isGameover; }
    public void SetGameover(bool state) { m_isGameover = state; }
}
