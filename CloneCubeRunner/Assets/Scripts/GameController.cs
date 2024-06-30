using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;  //dùng để quản lý các thư mục Screnes
public class GameController : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject obstacleUp;
    public float spawTime;
    float m_spawTime;
    int m_score;
    bool m_isGameover;
    // Start is called before the first frame update

    //xử lý view điểm 
    UIManager m_ui; // thêm chữ tiền tố [m_] để phân biệt public / private / protected
    void Start()
    {
        m_spawTime = 0; // khởi tạo ban đầu
        m_ui = FindAnyObjectByType<UIManager>();
        m_ui.SetScoreText("Score :" + m_score);
    }

    // Update is called once per frame
    void Update()
    {
        // nếu gameover thì reset
        if (m_isGameover)
        {
            m_spawTime = 0;
            m_ui.ShowGameoverPanel(true);
            return; // return là dừng tắt cả các hoạt động phía dưới
        }

        m_spawTime -= Time.deltaTime;

        //thời gian lùi về 0 thì tạo mới
        if (m_spawTime <= 0)
        {
            // khởi tạo obsacle
            SpawObsacle();
            SpawObsacleUp();
            //reset time
            m_spawTime = spawTime;
        }



    }
    // Phuong thuc tao ra cac chuong ngai vat 
    public void SpawObsacle()
    {
        //random tọa độ pos vật thể
        float randYpos = Random.Range(-3f, -2.15f);
        Vector2 spawnPosRightTo = new Vector2(10.76f, randYpos);

        //kiểm tra  vật thể phải được gán # null
        if (obstacle)
        {
            Instantiate(obstacle, spawnPosRightTo, Quaternion.identity);
        }//Quaternion.identity : không xoay 
    }
    public void SpawObsacleUp()
    {
        float randXpos = Random.Range(-7.2f, 7.2f);
        Vector2 spawnPosUpTo = new Vector2(randXpos, 4.04f);
        if (obstacleUp)
        {
            Instantiate(obstacleUp, spawnPosUpTo, Quaternion.identity);
        }

    }
    public void SetScore(int value) { m_score = value; }
    public int GetScore() { return m_score; }
    public void ScoreIncrenment()
    {
        m_score++;
        m_ui.SetScoreText("Score : " + m_score);
    }

    public bool isGameover() { return m_isGameover; }
    public void SetGameover(bool state) { m_isGameover = state; }
    //xử lý replay 
    public void Relay()
    {
        // truyền tên file trong folder Scenes
        SceneManager.LoadScene("Main");
    }
}
