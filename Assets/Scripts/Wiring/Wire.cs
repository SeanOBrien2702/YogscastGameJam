using UnityEngine;

public class PipeScript : MonoBehaviour
{
    int[] rotations = { 0, 90, 180, 270 };

    public float[] correctRotation;
    [SerializeField]
    bool isPlaced = false;

    int PossibleRots = 1;

    [SerializeField] WireController wireController;

    private void Start()
    {
        PossibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, (int)rotations[rand]);
        int rotation = (int)Mathf.Abs(transform.eulerAngles.z);
        if (PossibleRots > 1)
        {
            if (rotation == correctRotation[0] || rotation == correctRotation[1])
            {
                isPlaced = true;
                wireController.correctMove();
            }
        }
        else
        {
            if (rotation == correctRotation[0])
            {
                isPlaced = true;
                wireController.correctMove();
            }
        }
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        int rotation = (int)Mathf.Abs(transform.eulerAngles.z);
        if (PossibleRots > 1)
        {
            if (rotation == correctRotation[0] || rotation == correctRotation[1] && isPlaced == false)
            {
                isPlaced = true;
                wireController.correctMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                wireController.wrongMove();
            }
        }
        else
        {
            if (rotation == correctRotation[0] && isPlaced == false)
            {
                isPlaced = true;
                wireController.correctMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                wireController.wrongMove();
            }
        }
    }
}