using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;

public enum NPC
{
    Kid, Lady, Mayor, Nerd, Man, Woman
}

public class NPCController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    SpriteLibrary library;
    [SerializeField] NPC npc = NPC.Kid;
    int[] sequence = new int[] { 0, 1, 2, 1, 3, 1, 2, 1};

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        library = GetComponent<SpriteLibrary>();
        StartCoroutine(CustomerNPC());
    }

    IEnumerator CustomerNPC()
    {
        int index = 0;
        while (true)
        {
            Debug.Log(index);
            spriteRenderer.sprite = library.GetSprite(npc.ToString(), sequence[index].ToString());
            index++;
            if(index >= sequence.Length)
            {
                index = 0;
            }

            yield return new WaitForSeconds(0.15f);
        }
    }
}
