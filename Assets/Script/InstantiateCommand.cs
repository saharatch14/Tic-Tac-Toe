using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCommand : IAction
{
    GameController gameController;
    private GameObject toSpawnGameObject;
    //private Vector2 positionToSpawn;
    private Collider2D positionToSpawn;

    private GameObject spawnedGameObject;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Option").GetComponent<GameController>();
        //gameController.Setup(player);
    }

    public InstantiateCommand(GameObject toSpawnGameObject, Collider2D positionToSpawn)
    {
        this.toSpawnGameObject = toSpawnGameObject;
        this.positionToSpawn = positionToSpawn;
    }

    public void ExecuteCommand()
    {
        //spawnedGameObject = GameObject.Instantiate(toSpawnGameObject, positionToSpawn, Quaternion.identity);
        gameController.TicTacToeCheck(toSpawnGameObject);
    }

    public void UndoCommand()
    {
        GameObject.Destroy(spawnedGameObject);
    }

}
