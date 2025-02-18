using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 10f;
    private float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //player移動の処理
        //前に進む
        if (Input.GetKey(KeyCode.E)) {
            rb.AddForce(transform.forward * moveSpeed);
        }
        //後ろに進む
        if (Input.GetKey(KeyCode.T)) {
            rb.AddForce(-transform.forward * moveSpeed);
        }
        //右に進む
        if (Input.GetKey(KeyCode.G)) {
            rb.AddForce(transform.right * moveSpeed);
        }
        //左に進む
        if (Input.GetKey(KeyCode.D)) {
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

        /*//右に傾く
        if (Input.GetKey(KeyCode.I)) {
            transform.Rotate(0, 0, rotationSpeed);
        }
        //左に傾く
        if (Input.GetKey(KeyCode.Y)) {
            transform.Rotate(0, 0, -rotationSpeed);
        }*/




    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            //ResetRotation();
            rb.angularVelocity = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.E) ||
            Input.GetKeyUp(KeyCode.R) ||
            Input.GetKeyUp(KeyCode.T) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.F) ||
            Input.GetKeyUp(KeyCode.G))
        {
            rb.linearVelocity = Vector3.zero;
            
        }
    }

    void ResetRotation()
    {
        // プレイヤーの回転をリセット
        transform.rotation = Quaternion.identity;
    }
}
