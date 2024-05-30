using UnityEngine;


public class VillageRadius : MonoBehaviour
{
    public float villageRadius = 5f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, villageRadius);
    }
}
