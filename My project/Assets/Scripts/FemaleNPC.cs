using UnityEngine;
using System.Collections;

public class FemaleNPC : MonoBehaviour
{
    private bool isPregnant = false;
    private float pregnancyDuration = 10f; 
    public GameObject[] childPrefabs; 


    public void Reproduce()
    {
        if (!isPregnant)
        {
            isPregnant = true;
            StartCoroutine(GiveBirth());
        }
    }

    public IEnumerator GiveBirth()
{
    yield return new WaitForSeconds(pregnancyDuration);

    GameObject childPrefab = childPrefabs[Random.Range(0, childPrefabs.Length)];
    GameObject child = Instantiate(childPrefab, transform.position, Quaternion.identity);


    Rigidbody2D childRb = child.GetComponent<Rigidbody2D>();
    if (childRb == null)
    {
        childRb = child.AddComponent<Rigidbody2D>();
    }
    string childTag = childPrefab == childPrefabs[0] ? "Male" : "Female";
    child.tag = childTag;
    child.layer = LayerMask.NameToLayer(childTag);

    Rigidbody2D femaleRb = GetComponent<Rigidbody2D>();
    if (femaleRb != null)
    {
        childRb.mass = femaleRb.mass;
        childRb.drag = femaleRb.drag;
        childRb.angularDrag = femaleRb.angularDrag;
        childRb.gravityScale = femaleRb.gravityScale;
        childRb.isKinematic = femaleRb.isKinematic;
        childRb.constraints = femaleRb.constraints;
        childRb.collisionDetectionMode = femaleRb.collisionDetectionMode;
        childRb.sleepMode = femaleRb.sleepMode;
        childRb.interpolation = femaleRb.interpolation;
        childRb.simulated = femaleRb.simulated;
        childRb.freezeRotation = femaleRb.freezeRotation;
    }

    child.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    child.GetComponent<SpriteRenderer>().sortingLayerName = "MG_Ground";
    child.AddComponent<NPCMovement>();

    NPCMovement femaleMovement = GetComponent<NPCMovement>();
    if (femaleMovement != null)
    {
        NPCMovement childMovement = child.GetComponent<NPCMovement>();
        if (childMovement != null)
        {
            childMovement.patrolPoints = new Transform[femaleMovement.patrolPoints.Length];
            for (int i = 0; i < femaleMovement.patrolPoints.Length; i++)
            {
                childMovement.patrolPoints[i] = femaleMovement.patrolPoints[i];
            }
        }
    }

    isPregnant = false;
}

}
