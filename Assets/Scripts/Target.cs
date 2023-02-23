using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float minSpeed = 12f;
    [SerializeField] float maxSpeed = 16f;
    [SerializeField] float maxTorque = 10f;
    [SerializeField] float xSpawnRange = 4f;
    [SerializeField] float ySpawnPos = -6f;
    [SerializeField] int pointValue;

    [SerializeField] ParticleSystem explosionParticle;

    Rigidbody targetRb;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(GenerateRandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(GenerateRandomTorque(), GenerateRandomTorque(), GenerateRandomTorque(), ForceMode.Impulse);
        transform.position = GenerateRandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyTarget() 
    {
        if (gameManager.IsGameActive && !gameManager.IsGamePaused)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            gameManager.UpdateScore(pointValue);

            if (gameObject.CompareTag("Bad"))
            {
                gameManager.ReduceLives();
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.ReduceLives();
        }

        Destroy(gameObject);
    }

    Vector3 GenerateRandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float GenerateRandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 GenerateRandomSpawnPos()
    {
         return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnPos);
    }
}
