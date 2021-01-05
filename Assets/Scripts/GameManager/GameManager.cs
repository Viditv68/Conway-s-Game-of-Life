using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tiles;
    [SerializeField]
    private static int row = 170;
    [SerializeField]
    private static int col = 128;

    Cell[,] grid = new Cell[row, col];

    private void Start()
    {
        InitializeGrid();
    }

   

    private void Update()
    {
        Nextgeneration();
        LiveorDie();
    }


    private void InitializeGrid()
    {
        for (int j = 0; j < col; j++)
        {
            for (int i = 0; i < row; i++)
            {
                Cell cell = Instantiate(tiles, new Vector2(i, j), Quaternion.identity).GetComponent<Cell>();
                grid[i, j] = cell;
                grid[i, j].SetCellAlive(SelectLifeRandomly());
            }
        }
    }

    private void Nextgeneration()
    {
        for (int j = 0; j < col-1; j++)
        {
            for (int i = 0; i < row-1; i++)
            {
                
                grid[i,j].neighbours = CountCellNeighbours(j,i);
            }
        }
    }

    private int CountCellNeighbours(int y, int x)
    {
        int neighbours = 0;


        for (int j = Mathf.Max(0,y-1); j <=Mathf.Min(y+1, col); j++)
        {
            for (int i = Mathf.Max(0,x-1); i <= Mathf.Min(x+1, row); i++)
            {
                if((i==x && j== y))
                {
                    continue;
                }
                if(grid[i,j].isAlive)
                {
                    neighbours++;
                }

            }
            
        }

        return neighbours;

    }

    private void LiveorDie()
    {
        for(int j = 0; j < col; j++)
        {
            for(int i = 0; i <row; i++)
            {
                
                if(grid[i,j].isAlive)
                {
                    //Any live cell with fewer than two live neighbors dies, as if by underpopulation
                    //Any live cell with two or three live neighbors lives on to the next generation.
                    ///Any live cell with more than three live neighbors dies, as if by overpopulation.
                    if (grid[i,j].neighbours < 2 || grid[i,j].neighbours > 3)
                    {
                        grid[i, j].SetCellAlive(false);
                    }

                }

                //Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
                else
                {
                    if(grid[i,j].neighbours == 3)
                    {
                        grid[i, j].SetCellAlive(true);
                    }
                }
            }
        }
    }

    private bool SelectLifeRandomly()
    {
        int random = Random.Range(0, 2);
        {
            if(random == 0)
            {
                return false;
            }
            return true; 
        }
    }
}
