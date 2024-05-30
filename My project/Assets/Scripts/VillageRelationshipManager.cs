using UnityEngine;
using System.Collections.Generic;

public class VillageRelationshipManager : MonoBehaviour
{
    public GameObject[] villages; 
    public float maxRelationship = 100f; 
    private Dictionary<GameObject, float> relationships = new Dictionary<GameObject, float>();

    void Start()
    {
        InitializeRelationships();
    }

    void InitializeRelationships()
    {
        foreach (GameObject village in villages)
        {
            float relationship = Random.Range(0f, maxRelationship);
            relationships[village] = relationship;
        }
    }

    public float GetRelationship(GameObject village)
    {
        return relationships[village];
    }

    public void ChangeRelationship(GameObject village, float amount)
    {
        relationships[village] += amount;

        relationships[village] = Mathf.Clamp(relationships[village], 0f, maxRelationship);
    }

    public float CalculateAggression(GameObject village)
    {
        float aggression = Mathf.Clamp01(relationships[village] / maxRelationship);
        return aggression;
    }
}
