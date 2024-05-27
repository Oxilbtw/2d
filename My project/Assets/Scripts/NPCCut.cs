using UnityEngine;
using System.Collections;

public class NPCCut : MonoBehaviour
{
    public float cutRange = 1f;
    public float speed = 2f;

    private bool cutting = false;
    private Transform targetTree;

    void Update()
    {
        if (!cutting && targetTree == null)
        {
            FindNearestTree();
        }
        else if (!cutting && targetTree != null)
        {
            MoveToTree(); 
        }
    }

    void FindNearestTree()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, cutRange);
        float nearestDistance = float.MaxValue;
        foreach (var hitCollider in hitColliders)
        {
            TreeObject tree = hitCollider.GetComponent<TreeObject>();
            if (tree != null)
            {
                float distance = Vector2.Distance(transform.position, hitCollider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    targetTree = hitCollider.transform;
                }
            }
        }
    }

    void MoveToTree()
    {
        Vector2 direction = (targetTree.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * speed);

        float distanceToTree = Vector2.Distance(transform.position, targetTree.position);
        if (distanceToTree < 0.2f)
        {
            StartCoroutine(CutWood());
        }
    }

    IEnumerator CutWood()
    {
        cutting = true;
        yield return new WaitForSeconds(2);

        TreeObject tree = targetTree.GetComponent<TreeObject>(); 
        if (tree != null)
        {
            tree.TreeCut(1); 
            GetComponent<ResourceCollection>().CollectWood(10);
        }

        targetTree = null;
        cutting = false;
    }
}
