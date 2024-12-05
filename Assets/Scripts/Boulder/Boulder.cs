using UnityEngine;

public class Boulder : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var heading = transform.position - collision.transform.position;
        if(Mathf.Abs(heading.x) > Mathf.Abs(heading.y))
        {
            if(heading.x > 0)
            {
                heading = Vector3.right;
            }
            else
            {
                heading = Vector3.left;
            }         
        }
        else
        {
            if(heading.y > 0)
            {
                heading = Vector3.up;
            }
            else
            {
                heading = Vector3.down;
            }
        }
        if(!IsBoxBlocked(transform.transform.position, heading))
            transform.Translate(heading);
    }

    public bool IsBoxBlocked(Vector3 position, Vector2 direction)
    {
        float interactDistance = 0.75f;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, interactDistance);
        Debug.DrawRay(transform.position, direction * interactDistance, Color.green,4);
        if (hit.Length > 1 && hit[1].collider != null)
        {
            return true;    
        }
        return false;
    }
}