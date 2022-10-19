using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Artifact", menuName = "Artifacts/New artifact")]

public class ArtifactData : ScriptableObject
{
    public new string name;
    public Sprite visual;
}