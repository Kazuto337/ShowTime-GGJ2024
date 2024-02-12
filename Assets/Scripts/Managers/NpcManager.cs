using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] List<PublicBehavior> publicBehaviors;
    private void Start()
    {
        int initialState = Random.Range(1, 2);

        foreach (PublicBehavior item in publicBehaviors)
        {
            item.SetAnimation(initialState);
        }
    }
}
