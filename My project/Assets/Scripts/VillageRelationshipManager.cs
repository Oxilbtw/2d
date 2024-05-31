using UnityEngine;
using System.Collections.Generic;

public class VillageRelationshipManager : MonoBehaviour
{
    public GameObject[] villages;
    public float minRelationship = -1f;
    public float maxRelationship = 1f;
    public float updateInterval = 20f;

    private Dictionary<(GameObject, GameObject), float> relationships = new Dictionary<(GameObject, GameObject), float>();
    private float lastUpdateTime = 0f;

    void Start()
    {
        foreach (GameObject village1 in villages)
        {
            foreach (GameObject village2 in villages)
            {
                if (village1 != village2)
                {
                    relationships[(village1, village2)] = Random.Range(minRelationship, maxRelationship);
                }
            }
        }

        PrintRelationships();
    }

    void Update()
    {
        if (Time.time - lastUpdateTime >= updateInterval)
        {
            UpdateRelationships();
            lastUpdateTime = Time.time;
        }
    }

    void UpdateRelationships()
    {
        foreach (var key in relationships.Keys)
        {
            float change = Random.Range(-0.1f, 0.1f); // Рандомне зміщення відношень
            relationships[key] += change;
            relationships[key] = Mathf.Clamp(relationships[key], minRelationship, maxRelationship);
        }

        PrintRelationships();
    }

    void PrintRelationships()
    {
        foreach (var kvp in relationships)
        {
            Debug.Log(kvp.Key.Item1.name + " та " + kvp.Key.Item2.name + ": " + kvp.Value);
        }
    }
}
