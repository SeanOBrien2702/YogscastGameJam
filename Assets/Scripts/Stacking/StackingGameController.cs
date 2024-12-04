using UnityEngine;
using UnityEngine.Tilemaps;

public class StackingGameController : MonoBehaviour
{
    [SerializeField] GameObject gameoverScreen;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] BlockController blockPrefab;
    [SerializeField] Tilemap stackVisual;
    [SerializeField] Tile tile;
    BlockController currentBlock;
    BlockController lastBlock;
    int level = 0;
    [SerializeField] float startingSpeed;
    [SerializeField] float speedIncrement;
    float currentSpeed = 0;
    float currentSize = 4;
    bool[,] grid;
    bool hasLost = false;

    private void Start()
    {
        gameoverScreen.SetActive(false);
        victoryScreen.SetActive(false);
        grid = new bool[12, 10];

        currentSpeed = startingSpeed;
        currentBlock = Instantiate(blockPrefab, new Vector3(2, 0, 0), Quaternion.identity);
        currentBlock.SetSize(currentSize);
        currentBlock.MoveSpeed = currentSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetBlockPosition();
            ShowRemainingBlocks();
            level++;          
            CreateBlock();
            if (level >= 9 &&
                !hasLost)
            {
                victoryScreen.SetActive(true);
            }
        }
    }

    private void ShowRemainingBlocks()
    {
        for (int i = 0; i < 10; i++)
        {
            if (grid[level, i] == true)
            {
                stackVisual.SetTile(new Vector3Int(i, level, 0), tile);                
            }
        }      
    }

    private void SetBlockPosition()
    {
        if (level == 0)
        {
            for (int i = 0; i < currentSize; i++)
            {            
                    grid[level, (int)currentBlock.transform.position.x + i] = true;
            }
            return;
        }
        
        for (int i = 0; i < currentSize; i++)
        {
            if (grid[level - 1, (int)currentBlock.transform.position.x + i] == true)
            {
                grid[level, (int)currentBlock.transform.position.x + i] = true;
            }
        }
        int size = 0;
        for (int i = 0;i < 10; i++)
        {
            if (grid[level, i] == true)
            {
                size++;
            }
        }
        currentSize = size;
        if(currentSize == 0)
        {
            gameoverScreen.SetActive(true);
            hasLost = true;
        }
    }

    void CreateBlock()
    {
        lastBlock = currentBlock;
        Destroy(lastBlock.gameObject);
        currentBlock = Instantiate(blockPrefab);
        currentBlock.SetSize(currentSize);
        lastBlock.enabled = false;
        int startPos = UnityEngine.Random.Range(1, 6);
        currentBlock.transform.position = new Vector3(startPos, level, 0);
        currentSpeed -= speedIncrement;
        currentBlock.MoveSpeed = currentSpeed;
    }
}