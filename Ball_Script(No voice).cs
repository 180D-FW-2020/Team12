using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball_Script : MonoBehaviour
{
    public float minX, maxX;
    public float speed;
    public float horizontalSpeed;
    private Rigidbody myBody;
    private bool thrown = false;
    

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        BallMovement();
    }

    void BallMovement()
    {
        if(!thrown) // if not throw the ball yet
        {
            float xAxis = Input.GetAxis("Horizontal");
            Vector3 position = transform.localPosition;
            position.x += xAxis * horizontalSpeed;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            transform.localPosition = position;

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
        if(thrown && myBody.IsSleeping())
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
