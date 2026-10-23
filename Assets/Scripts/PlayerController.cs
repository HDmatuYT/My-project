using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text scoreText;
    public Text winText;
    public GameObject wall;
    public Rigidbody rb;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetscoreText();
        winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement=new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        //retsart level
        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetscoreText();
            if(score >= 5) 
            {
                wall.gameObject.SetActive(false) ;
            }
        }

        if(other.gameObject.tag == "danger")
        {
            Application.LoadLevel(Application.loadedLevel);
        }




    }




    void SetscoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score >= 10)
        {
            winText.text = "You won! Press R to restart or ESC to quit";
        }
    }
}
   