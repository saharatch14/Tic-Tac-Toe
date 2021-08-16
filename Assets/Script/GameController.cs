using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Stack<IAction> historyStack = new Stack<IAction>();
    private Stack<IAction> redoHistoryStack = new Stack<IAction>();

    public int whoseTurn;
    public int turnCount;
   
    public enum Seed {EMPTY, CROSS,CRICLE};
    public Seed turn;

    public GameObject[] whoturnUI;
    public GameObject cross, cricle;

    // Start is called before the first frame update
    public GameObject bypass;
    GridManager gridManager;

    public int[,] markedSpaces;
    public bool gamefinsh = false;

    void Awake()
    {
        gridManager = bypass.GetComponent<GridManager>();
        markedSpaces = new int[gridManager.rows, gridManager.cols];
    }

    void Start()
    {
        turn = (Seed)Random.Range(1, 3);
        for (int row = 0; row < gridManager.rows; row++)
        {
            for (int col = 0; col < gridManager.cols; col++)
            {
                markedSpaces[row, col] = -100;
            }
        }
        if(turn == Seed.CROSS)
        {
            whoturnUI[0].SetActive(true);
            whoseTurn = 0;
        }
        else
        {
            whoturnUI[1].SetActive(true);
            whoseTurn = 1;
        }
    }

    void GameSetup()
    {
        whoseTurn = 0;
        turnCount = 0;
    }

    public void TicTacToeCheck(GameObject obj)
    {

        char[] chars = obj.name.ToCharArray();
        char numrow = chars[0];
        char numcol = chars[2];

        int covnumrow = (int)char.GetNumericValue(numrow);
        int covnumcol = (int)char.GetNumericValue(numcol);

        markedSpaces[covnumrow, covnumcol] = whoseTurn+1;

        turnCount++;
        if (turnCount > gridManager.rows+1)
        {
            winningcheck();
        }
        if (turn == Seed.CROSS)
        {
            Instantiate(cricle, obj.transform.position, Quaternion.identity);
            whoseTurn = 1;
            whoturnUI[0].SetActive(false);
            whoturnUI[1].SetActive(true);
            turn = Seed.CRICLE;
        }
        else
        {

            Instantiate(cross, obj.transform.position, Quaternion.identity);
            whoseTurn = 0;
            whoturnUI[1].SetActive(false);
            whoturnUI[0].SetActive(true);
            turn = Seed.CROSS;
        }

        Destroy(obj.gameObject);

    }

    public void winningcheck()
    {
        int[] solution = new int[gridManager.rows + gridManager.cols + 2];

        for (int row = 0; row < gridManager.rows; row++)
        {
            for (int col = 0; col < gridManager.cols; col++)
            {
                solution[row] += markedSpaces[row, col];
            }
        }

        int lastlocate = gridManager.rows;
        for (int col = 0; col < gridManager.rows; col++)
        {
            for (int row = 0; row < gridManager.cols; row++)
            {
                solution[lastlocate] += markedSpaces[row, col];
            }
            lastlocate += 1;
        }

        for (int row = 0; row < gridManager.cols; row++)
        {
            solution[gridManager.rows + gridManager.cols] += markedSpaces[row, row];
        }

        int s8revert = gridManager.cols - 1;
        for (int row = 0; row < gridManager.cols; row++)
        {
            solution[gridManager.rows + gridManager.cols + 1] += markedSpaces[row, s8revert];
            s8revert -= 1;
        }

        /*int s1 = markedSpaces[0, 0] + markedSpaces[0, 1] + markedSpaces[0, 2];

        int s2 = markedSpaces[1, 0] + markedSpaces[1, 1] + markedSpaces[1, 2];

        int s3 = markedSpaces[2, 0] + markedSpaces[2, 1] + markedSpaces[2, 2];

        int s4 = markedSpaces[0, 0] + markedSpaces[1, 0] + markedSpaces[2, 0];

        int s5 = markedSpaces[0, 1] + markedSpaces[1, 1] + markedSpaces[2, 1];

        int s6 = markedSpaces[0, 2] + markedSpaces[1, 2] + markedSpaces[2, 2];

        int s7 = markedSpaces[0, 0] + markedSpaces[1, 1] + markedSpaces[2, 2];

        int s8 = markedSpaces[0, 2] + markedSpaces[1, 1] + markedSpaces[2, 0];

        var solution = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };*/

        /*for (int row = 0; row < solution.Length; row++)
        {
            Debug.Log(solution[row]);
        }*/

        for (int i = 0; i<solution.Length; i++)
        {
            if(solution[i] == gridManager.cols * (whoseTurn+1))
            {
                Debug.Log("Player "+ whoseTurn + " won!");
                whoseTurn = 0;
                turnCount = 0;
                gamefinsh = true;

                if (turn == Seed.CROSS)
                {
                    whoturnUI[2].SetActive(true);
                }
                else
                {
                    whoturnUI[3].SetActive(true);

                }
            }
        }
    }

    /*public void ExecuteCommand(IAction action)
    {
        action.ExecuteCommand();
        historyStack.Push(action);
        redoHistoryStack.Clear();
    }

    public void UndoCommand()
    {
        if (historyStack.Count > 0)
        {
            redoHistoryStack.Push(historyStack.Peek());
            historyStack.Pop().UndoCommand();
        }
    }

    public void RedoCommand()
    {
        if (redoHistoryStack.Count > 0)
        {
            historyStack.Push(redoHistoryStack.Peek());
            redoHistoryStack.Pop().ExecuteCommand();
        }
    }*/

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && (gamefinsh == false))
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                /*if (turn == Seed.CROSS)
                {
                   ExecuteCommand(new InstantiateCommand(cross, hit.collider));
                   //TicTacToeCheck(hit.collider.gameObject);
                }
                else
                {
                    ExecuteCommand(new InstantiateCommand(cricle, hit.collider));
                    //TicTacToeCheck(hit.collider.gameObject);
                }*/

                TicTacToeCheck(hit.collider.gameObject);

                Debug.Log(markedSpaces[0, 0] + " " + markedSpaces[0, 1] + " " + markedSpaces[0, 2] + "\n" 
                        + markedSpaces[1, 0] + " " + markedSpaces[1, 1] + " " + markedSpaces[1, 2] + "\n"
                        + markedSpaces[2, 0] + " " + markedSpaces[2, 1] + " " + markedSpaces[2, 2]);

            }

        }
    }
}
