using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody rb;

    [Header("移動・回転設定")]
    public float moveSpeed = 10f;                // WASDでの水平移動速度
    public float rotationSpeed = 1f;   // マウスXでの左右回転速度
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        //上に進む
        if (Input.GetKey(KeyCode.R)) {
            rb.AddForce(transform.up * moveSpeed);
        }
        //下に進む
        if (Input.GetKey(KeyCode.F)) {
            rb.AddForce(-transform.up * moveSpeed);
        }

                //上を向く
        if (Input.GetKey(KeyCode.U)) {
            transform.Rotate(-rotationSpeed, 0, 0);
        }
        //下を向く
        if (Input.GetKey(KeyCode.J)) {
            transform.Rotate(rotationSpeed, 0, 0);
        }
        //右を向く
        if (Input.GetKey(KeyCode.K)) {
            transform.Rotate(0, rotationSpeed, 0);
        }
        //左を向く
        if (Input.GetKey(KeyCode.H)) {
            transform.Rotate(0, -rotationSpeed, 0);
        }
        

/*         // --- マウスY入力で上下移動 ---
        // ※Input.GetAxis("Mouse Y")は通常、上方向に動かすと正の値となるため
        // 上昇/下降の感覚に合わせて符号を反転させる場合は「-Input.GetAxis("Mouse Y")」に変更してください
        float mouseWheelScroll = Input.GetAxis("Mouse ScrollWheel");
        rb.AddForce(transform.up * mouseWheelScroll * verticalMoveSpeed); */
    }

    void Update()
    {
/*         // --- マウスX入力で左右回転 ---
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * horizontalRotationSpeed, 0);

        // 回転や移動をリセットする例（Nキー押下時）
        if (Input.GetKeyDown(KeyCode.N))
        {
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        } */

        // WASDのキーを離した際に慣性をリセットする例
        if (Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.R) ||
            Input.GetKeyUp(KeyCode.F))
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}