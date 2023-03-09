using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPosition = new Vector3(33.50f, 0, 0);
    private readonly float startDelay = 2;
    private readonly float repeatRate = 2;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver && playerControllerScript.startTheGame)
        {
            var selectedObstacle = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[selectedObstacle], spawnPosition, obstaclePrefab[selectedObstacle].transform.rotation);

            var chanceForObstacleTower = Random.Range(0, 4);
            var fiftyPercentChance = 2;

            if ((obstaclePrefab[selectedObstacle].name.Equals("Obstacle_Crate") || obstaclePrefab[selectedObstacle].name.Equals("Obstacle_Barrel02")) && chanceForObstacleTower > fiftyPercentChance)
            {
                var crateHeight = obstaclePrefab[selectedObstacle].GetComponent<BoxCollider>().size.y;
                var secondCrateSpawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + crateHeight, spawnPosition.z);

                Instantiate(obstaclePrefab[selectedObstacle], secondCrateSpawnPosition, obstaclePrefab[selectedObstacle].transform.rotation);
            }
        }
    }
}
