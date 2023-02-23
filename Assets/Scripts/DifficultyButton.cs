using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] int difficulty;

    Button difficultyButton;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        difficultyButton = GetComponent<Button>();
        difficultyButton.onClick.AddListener(SetDifficulty);

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
        Debug.Log(difficultyButton.gameObject.name);
    }
}
