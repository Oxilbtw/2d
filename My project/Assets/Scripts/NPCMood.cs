using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCMood : MonoBehaviour
{
    private NPCMovement npcMovement;
    private NPCCut npcCut;

    private float nextMoodChangeTime = 0f;
    public float moodChangeInterval = 10f;
    public float moodChangeChance = 0.25f;

    void Start()
    {
        npcMovement = GetComponent<NPCMovement>();
        npcCut = GetComponent<NPCCut>();

        ChangeMood();
    }

    void Update()
    {
        if (Time.time >= nextMoodChangeTime)
        {
            if (Random.value <= moodChangeChance)
            {
                ChangeMood();
            }
            nextMoodChangeTime = Time.time + moodChangeInterval;
        }
    }

    void ChangeMood()
    {
        if (Random.Range(0, 2) == 0)
        {
            npcMovement.enabled = true;
            npcCut.enabled = false;
        }
        else
        {
            npcMovement.enabled = false;
            npcCut.enabled = true;
        }

        if (!npcMovement.enabled)
        {
            float reproductionChance = Random.value;
            if (reproductionChance <= 0.25f)
            {
                StartReproduction();
            }
        }
    }

    void StartReproduction()
    {
        GameObject[] femaleNPCs = GameObject.FindGameObjectsWithTag("Female");

        if (femaleNPCs.Length > 0)
        {
            GameObject closestFemale = null;
            float closestDistance = float.MaxValue;
            foreach (var female in femaleNPCs)
            {
                float distance = Vector2.Distance(transform.position, female.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestFemale = female;
                }
            }

            if (closestFemale != null)
            {
                FemaleNPC femaleNPC = closestFemale.GetComponent<FemaleNPC>();
                if (femaleNPC != null)
                {
                    femaleNPC.Reproduce();
                }
            }
        }
    }
}
