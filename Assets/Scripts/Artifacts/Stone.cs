using System.Linq;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField]
    private ArtifactData stoneData;

    private void Start()
    {
        var fields = stoneData.GetType().GetFields();
        Debug.Log(fields.Count());
    }
}
