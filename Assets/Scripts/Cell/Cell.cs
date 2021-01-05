using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive = false;
    public int neighbours = 0;

    public void SetCellAlive(bool isAlive)
    {
        this.isAlive = isAlive;

        if(this.isAlive)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
