using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireController : MonoBehaviour
{
    [SerializeField] GameObject victoryScreen;
    [SerializeField] GameObject PipesHolder;
    GameObject[] pipes;

    [SerializeField]
    int totalPipes = 0;
    [SerializeField]
    int correctedPipes = 0;

    void Start()
    {
        victoryScreen.SetActive(false);
        totalPipes = PipesHolder.transform.childCount;

        pipes = new GameObject[totalPipes];

        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;

        Debug.Log("correct Move");

        if (correctedPipes == totalPipes)
        {
            Debug.Log("You win!");
            victoryScreen.SetActive(true);
        }
    }

    public void wrongMove()
    {
        correctedPipes -= 1;
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WireController : MonoBehaviour
//{
//    public static WireController Instance;

//    // [SerializeField] private LevelData _level;
//    [SerializeField] int height;
//    [SerializeField] int width;
//    [SerializeField] private Wire cellPrefab;
//    [SerializeField] Camera camera;

//    private bool hasGameFinished;
//    private Wire[,] wires;
//    private List<Wire> startWires;

//    private void Awake()
//    {
//        Instance = this;
//        hasGameFinished = false;
//        SpawnLevel();
//    }

//    private void SpawnLevel()
//    {
//        wires = new Wire[height, width];
//        startWires = new List<Wire>();

//        for (int i = 0; i < height; i++)
//        {
//            for (int j = 0; j < width; j++)
//            {
//                Vector2 spawnPos = new Vector2(j + 0.5f, i + 0.5f);
//                Wire tempPipe = Instantiate(cellPrefab);
//                tempPipe.transform.position = spawnPos;
//                //tempPipe.Init(_level.Data[i * width + j]);
//                wires[i, j] = tempPipe;
//                if (tempPipe.WireType == 1)
//                {
//                    startWires.Add(tempPipe);
//                }
//            }
//        }

//        camera.orthographicSize = Mathf.Max(height, width) + 2f;
//        Vector3 cameraPos = camera.transform.position;
//        cameraPos.x = width * 0.5f;
//        cameraPos.y = height * 0.5f;
//        camera.transform.position = cameraPos;

//        StartCoroutine(ShowHint());
//    }

//    private void Update()
//    {
//        if (hasGameFinished) return;

//        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
//        int row = Mathf.FloorToInt(mousePos.y);
//        int col = Mathf.FloorToInt(mousePos.x);
//        if (row < 0 || col < 0) return;
//        if (row >= height) return;
//        if (col >= width) return;

//        if (Input.GetMouseButtonDown(0))
//        {
//            wires[row, col].UpdateInput();
//            StartCoroutine(ShowHint());
//        }
//    }

//    private IEnumerator ShowHint()
//    {
//        yield return new WaitForSeconds(0.1f);
//        CheckFill();
//        CheckWin();
//    }

//    private void CheckFill()
//    {
//        for (int i = 0; i < height; i++)
//        {
//            for (int j = 0; j < width; j++)
//            {
//                Wire tempPipe = wires[i, j];
//                if (tempPipe.WireType != 0)
//                {
//                    tempPipe.IsFilled = false;
//                }
//            }
//        }

//        Queue<Wire> check = new Queue<Wire>();
//        HashSet<Wire> finished = new HashSet<Wire>();
//        foreach (var pipe in startWires)
//        {
//            check.Enqueue(pipe);
//        }

//        while (check.Count > 0)
//        {
//            Wire pipe = check.Dequeue();
//            finished.Add(pipe);
//            List<Wire> connected = pipe.ConnectedPipes();
//            foreach (var connectedPipe in connected)
//            {
//                if (!finished.Contains(connectedPipe))
//                {
//                    check.Enqueue(connectedPipe);
//                }
//            }
//        }

//        foreach (var filled in finished)
//        {
//            filled.IsFilled = true;
//        }

//        for (int i = 0; i < height; i++)
//        {
//            for (int j = 0; j < width; j++)
//            {
//                Wire tempPipe = wires[i, j];
//                tempPipe.UpdateFilled();
//            }
//        }

//    }

//    private void CheckWin()
//    {
//        for (int i = 0; i < height; i++)
//        {
//            for (int j = 0; j < width; j++)
//            {
//                if (!wires[i, j].IsFilled)
//                    return;
//            }
//        }

//        hasGameFinished = true;
//        StartCoroutine(GameFinished());
//    }

//    private IEnumerator GameFinished()
//    {
//        yield return new WaitForSeconds(2f);
//        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
//    }


//}



//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class WiringController : MonoBehaviour
//{
//    Stack<WiringButton> connection = new Stack<WiringButton>();
//    [SerializeField] WiringButton start;
//    Vector3 worldPoint;
//    WiringButton[,] grid;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        grid = GetComponent<Grid>();
//        WiringButton.OnWireTurn += WiringButton_OnWireTurn;
//    }

//    private void OnDestroy()
//    {
//        WiringButton.OnWireTurn -= WiringButton_OnWireTurn;
//    }

//    private void WiringButton_OnWireTurn(WiringButton selectedWire)
//    {

//        //if(connection.Peek().IsConnected(selectedWire))
//        //{

//        //}

//    }

//    //bool IsConnected()
//    //{

//    //}

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            //worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//            //var tpos = tilemap.WorldToCell(worldPoint);
//            //grid.WorldToCell(worldPoint);
//           // Try to get a tile from cell position
//           //var tile = tilemap.GetTile(tpos);

//           // if (tile)
//           // {

//           // }
//        }
//    }
//}
