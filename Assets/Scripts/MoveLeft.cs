using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    public float speed = 20;
    public bool isDashActive = false;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.startTheGame)
        {
            

            if (Input.GetKeyDown(KeyCode.RightArrow) && !playerControllerScript.gameOver)
            {
                speed = 40;
                playerControllerScript.dash = true;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) && !playerControllerScript.gameOver)
            {
                speed = 20;
                playerControllerScript.dash = false;
            }

            if (!playerControllerScript.gameOver)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

            if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}
