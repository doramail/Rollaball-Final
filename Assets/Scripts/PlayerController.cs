using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Polybrush;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] Vector3 respawnPoint;
    // public GameObject WinTextObject;
    public float speed = 10f;
    public int playerIndex;

    // private TextMeshProUGUI countText;
    private Transform playerRespawnPoint;
    private MenuController menuController;
    private ScoreHandeler scoreHandeler;
    private AudioSource myMusic;
    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        playerRespawnPoint = GameObject.Find("RespawnPoint").transform;
        menuController = GameObject.Find("Canvas").GetComponent<MenuController>();
        scoreHandeler = GameObject.Find("Canvas/CountPanel").GetComponent<ScoreHandeler>();
        myMusic = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        // WinTextObject.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            Respawn();
        }
    }

    void OnMove(InputValue movementValue)
    {
        // Function body
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        // countText.text = "count : " + count.ToString();
        menuController.AddCountText(playerIndex, count);
        if (scoreHandeler.Score >= 12 ) 
        {
            // WinTextObject.SetActive(true);
            menuController.WinGame();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        EnforceMaxSpeed();
    }

    void EnforceMaxSpeed()
    {
        // If the current player's speed is greater then what we want ... 
        if (rb.velocity.magnitude > maxSpeed)
        {
            // Cap it to maxSpeed
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            scoreHandeler.Score += 1;
            myMusic.Play();

            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Respawn();
            EndGame();
        }
    }

    public void SetNewRespawn(Vector3 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }

    public void KillPlayer()
    {
        // Move player back to respawn point
        transform.position = respawnPoint;
        // Kill all velocity on player rigidbody
        rb.velocity = new Vector3(0, 0, 0);
    }
    void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        transform.position = playerRespawnPoint.position;
    }

    void EndGame()
    {
        menuController.LoseGame();
        gameObject.SetActive(false);
    }
}
