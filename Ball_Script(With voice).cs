using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

public class Ball_Script : MonoBehaviour
{
    public float minX, maxX;
    public float speed;
    public float horizontalSpeed;
    public float SpeechX;

    private Rigidbody myBody;
    private bool thrown = false;


    private Dictionary<string, Action> keywords = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();

        keywords.Add("left", Left);
        keywords.Add("right", Right);

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    // Update is called once per frame
    void Update()
    {
        BallMovement();
    }

    void BallMovement()
    {
        
        if (!thrown) // if not throw the ball yet
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

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        keywords[speech.text].Invoke();

    }

    private void Left()
    {
        transform.Translate(SpeechX, 0, 0);
    }
    private void Right()
    {
        transform.Translate(-SpeechX, 0, 0);
    }

}
