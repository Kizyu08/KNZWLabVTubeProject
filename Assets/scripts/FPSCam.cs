using UnityEngine;
using System.Collections;

public class FPSCam : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    [SerializeField] int mp;//移動速度の倍率

    private float rotateLR, rotateUD;

    void Start()
    {
        // 自分のRigidbodyを取ってくる
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 十字キーで首を左右に回す
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
            rotateLR += 1.0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f));
            rotateLR -= 1.0f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(-1.0f, 0.0f, 0.0f));
            rotateUD -= 1.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f));
            rotateUD += 1.0f;
        }


        //回転リセット
        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(new Vector3(-rotateUD, -rotateLR, 0.0f));
            rotateLR = 0.0f;
            rotateUD = 0.0f;
        }

        // WASDで移動する
        float x = 0.0f;
        float z = 0.0f;
        float y = 0.0f;

        if (Input.GetKey(KeyCode.D))
        {
            x += 1.0f * mp;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1.0f * mp;
        }
        if (Input.GetKey(KeyCode.W))
        {
            z += 1.0f * mp;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1.0f * mp;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            y += 1.0f * mp;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            y -= 1.0f * mp;
        }


        m_Rigidbody.velocity = z * transform.forward + x * transform.right + y * transform.up;
    }
}