using System.Collections;
using UnityEngine;

public class NPCCut : MonoBehaviour
{
    public float cutRange = 1f;
    private bool cutting = false;

    void Update()
    {
        if (!cutting)
        {
            StartCoroutine(CutWood());
        }
    }

    IEnumerator CutWood()
    {
        cutting = true;
        yield return new WaitForSeconds(2);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, cutRange);
        foreach (var hitCollider in hitColliders)
        {
            TreeObject tree = hitCollider.GetComponent<TreeObject>(); 
            if (tree != null)
            {
                tree.TreeCut(1); 
                GetComponent<ResourceCollection>().CollectWood(10);
            }
        }
        cutting = false;
    }
}
