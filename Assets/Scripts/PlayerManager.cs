using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody rb;

    [Header("移動・回転設定")]
    public float moveSpeed = 10f;      // WASDでの水平移動速度
    public float rotationSpeed = 1f;   // 回転速度
    private string currentScene;


    void Start()
    {
        // 開発用：このScene内に既にPlayerが存在していれば破棄（複製防止）
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        rb = GetComponent<Rigidbody>();

        string sceneName = SceneManager.GetActiveScene().name;
        
        // 現在のシーン名を取得
        currentScene = SceneManager.GetActiveScene().name;

    }

    void FixedUpdate()
    {
        // --- WASDで水平移動 ---
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * moveSpeed);
        }

        //頭上に進む
        if (Input.GetKey(KeyCode.R))
        {
            rb.AddForce(transform.up * moveSpeed);
        }
        //足下に進む
        if (Input.GetKey(KeyCode.F))
        {
            rb.AddForce(-transform.up * moveSpeed);
        }

        //上を向く
        if (Input.GetKey(KeyCode.U))
        {
            transform.Rotate(-rotationSpeed, 0, 0);
        }
        //下を向く
        if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(rotationSpeed, 0, 0);
        }
        //右を向く
        if (Input.GetKey(KeyCode.K))
        {
            transform.Rotate(0, rotationSpeed, 0);
        }
        //左を向く
        if (Input.GetKey(KeyCode.H))
        {
            transform.Rotate(0, -rotationSpeed, 0);
        }

        // 右に傾く
        if (currentScene != "WithMap")
        {
            if (Input.GetKey(KeyCode.I))
            {
                transform.Rotate(0, 0, rotationSpeed);
            }
        }

        // 左に傾く
        if (currentScene != "WithMap")
        {
            if (Input.GetKey(KeyCode.Y))
            {
                transform.Rotate(0, 0, -rotationSpeed);
            }
        }

    }

    void Update()
    {

        // WASDのキーを離した際に慣性をリセットする例
        if (Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.R) ||
            Input.GetKeyUp(KeyCode.F) ||
            Input.GetKeyUp(KeyCode.Y) ||
            Input.GetKeyUp(KeyCode.U) ||
            Input.GetKeyUp(KeyCode.I) ||
            Input.GetKeyUp(KeyCode.H) ||
            Input.GetKeyUp(KeyCode.J) ||
            Input.GetKeyUp(KeyCode.K))
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}