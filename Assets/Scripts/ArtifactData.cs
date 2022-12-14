using UnityEngine;

[CreateAssetMenu(fileName = "Artifact", menuName = "Artifacts/New artifact")]

public class ArtifactData : ScriptableObject
{
    public new string name;
    public string description;
    public int damage;
    public int health;
    public int maxHealth;
    public float speed;
    public float dashCooldown;
    public float artifactActivation;
    public float value;
    public int slow;
    public int level;
    public Sprite visual;
    public GameObject artifactPrefab;
}