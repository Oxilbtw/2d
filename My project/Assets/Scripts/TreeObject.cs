using UnityEngine;

public class TreeObject : MonoBehaviour
{
    public int treeHP = 5;

    public void TreeCut(int amount)
    {
        treeHP -= amount;
        if (treeHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
