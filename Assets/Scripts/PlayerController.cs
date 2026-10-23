using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TMP_Text scoreText;
    public TMP_Text winText;
    public Rigidbody rb;
    public int score;
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        if (winText == null && scoreText != null)
        {
            // Win Text pole Inspectoris määratud: tee Score tekstist koopia ekraani keskele
            winText = Instantiate(scoreText, scoreText.transform.parent);
            winText.name = "Win text";
            RectTransform rt = winText.rectTransform;
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = new Vector2(900, 200);
            winText.alignment = TextAlignmentOptions.Center;
            Debug.LogWarning("PlayerController: Win Text pole Inspectoris määratud, lõin selle automaatselt.");
        }
        if (wall == null)
        {
            Debug.LogWarning("PlayerController: Wall pole Inspectoris määratud.");
        }
        SetscoreText();
        if (winText != null)
        {
            winText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        // restart level
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetscoreText();
            if (score >= 5 && wall != null)
            {
                wall.SetActive(false);
            }
        }
        // tag == ei viska viga, kui "danger" tagi pole projektis defineeritud
        else if (other.gameObject.tag == "danger")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void SetscoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score >= 10 && winText != null)
        {
            winText.text = "You won! Press R to restart or ESC to quit";
        }
    }
}