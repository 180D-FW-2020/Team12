using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Text;

public class Ball_Script : MonoBehaviour
{
    public int portNum = 22228;
    static UdpClient udpClient;

    public float minX, maxX;
    public float speed;
    private Rigidbody myBody;
    private bool thrown = false;


    // Start is called before the first frame update
    void Start()
    {
        udpClient = new UdpClient(portNum);
        //udpClient.Client.ReceiveTimeout = 100;

        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Ball Movement
        if (!thrown) // if not throw the ball yet
        {
            IPEndPoint remoteEP = null;
            byte[] data = udpClient.Receive(ref remoteEP);
            string message = Encoding.ASCII.GetString(data);

            print("message");
            print(message);
           
            float posx = float.Parse(message);

            float xAxis = Input.GetAxis("Horizontal");

            transform.position = new Vector3(Mathf.Clamp(posx, minX, maxX), 0.11f, -10f);

        }

        if (!thrown && Input.GetKeyDown(KeyCode.Space))
        {
            thrown = true;
            myBody.isKinematic = false;
            myBody.velocity = new Vector3(0, 0, speed);
        }
    }


    private void FixedUpdate() //need go build setting add scene
    {
        if (thrown && myBody.IsSleeping())
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
