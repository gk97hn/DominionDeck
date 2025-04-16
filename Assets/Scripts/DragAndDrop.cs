using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 startPosition;
    public GameObject enemyFightArea;
    public GameObject playerFightArea;
    private bool isOverEnemyFightArea = false;
    private bool isOverPlayerFightArea = false;

    void Start()
    {
        playerFightArea = GameObject.Find("PlayerDeckFightArea");
        enemyFightArea = GameObject.Find("EnemyDeckFightArea");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == enemyFightArea) 
        {
            isOverEnemyFightArea = true;
            isOverPlayerFightArea = false;
        }
        else if (collision.gameObject == playerFightArea)
        {
            isOverEnemyFightArea = false;
            isOverPlayerFightArea = true;
        }
        
    }
    void Update()
    {
        if (isDragging) 
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        
    }
    public void StartDragging() 
    {
        startPosition = transform.position;
        isDragging = true;
    }
    public void EndDragging() 
    {
        isDragging = false;
        if (isOverEnemyFightArea) 
        {
            transform.SetParent(enemyFightArea.transform, false);
        }
        else if (isOverPlayerFightArea)
        {
            transform.SetParent(playerFightArea.transform, false);

        }
        else 
        {
            transform.position = startPosition;
        }
       
    
    }
}
