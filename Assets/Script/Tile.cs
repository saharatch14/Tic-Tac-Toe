using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public GameObject objecoption;
    public GameController gameController;

    void Awake()
    {
        gameController = objecoption.GetComponent<GameController>();
    }

    /*public void Setup(GameController gameController)
    {
        this.gameController = gameController;
    }*/
    

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseDown()
    {
        //Debug.Log(TicTacToeCheck);
        //Debug.Log("Pressed primary button.");
        //gameController.TicTacToeCheck(gameObject.GetComponent<SpriteRenderer>());//.TicTacToeCheck(transform);
        //gameController.TicTacToeCheck(this.gameObject);
        //gameController.TicTacToeCheck(this.gameObject);

    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    string locatedtilde()
    {
        return this.gameObject.name;
    }
}
