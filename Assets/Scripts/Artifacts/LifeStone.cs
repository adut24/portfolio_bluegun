using UnityEngine;

public class LifeStone : MonoBehaviour
{
    [SerializeField]
    private ArtifactData stoneData;

    private void Start()
    {
        var fields = stoneData.GetType().GetFields();
        Debug.Log(fields.Length);
        foreach (var field in fields)
        {
            Debug.Log(field.Name);
        }
    }

    private void AddToPlayer()
    {

    }

    private void RemoveFromPlayer()
    {
        
    }
}
