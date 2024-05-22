using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour
{
    public int wood = 0;

    public void CollectWood(int amount)
    {
        wood +=amount;
        Debug.Log(wood);
    }
}
