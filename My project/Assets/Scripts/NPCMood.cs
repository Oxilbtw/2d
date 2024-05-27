using UnityEngine;

public class NPCMood : MonoBehaviour
{
    private NPCMovement npcMovement;
    private NPCCut npcCut;

    private float nextMoodChangeTime = 0f;
    public float moodChangeInterval = 10f;

    void Start()
    {
        npcMovement = GetComponent<NPCMovement>();
        npcCut = GetComponent<NPCCut>();

        // Встановлюємо початковий настрій
        ChangeMood();
    }

    void Update()
    {
        // Змінюємо настрій з ймовірністю 50% кожні moodChangeInterval секунд
        if (Time.time >= nextMoodChangeTime)
        {
            ChangeMood();
            nextMoodChangeTime = Time.time + moodChangeInterval;
        }
    }

    void ChangeMood()
    {
        if (Random.Range(0, 2) == 0) // 50% шанс зміни настрою
        {
            // НПС хоче патрулювати
            npcMovement.enabled = true;
            npcCut.enabled = false;
        }
        else
        {
            // НПС хоче рубати дерева
            npcMovement.enabled = false;
            npcCut.enabled = true;
        }
    }
}
