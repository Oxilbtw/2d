using UnityEngine;

public class LayerOrderSetter : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
