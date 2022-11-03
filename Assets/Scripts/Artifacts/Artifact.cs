using UnityEngine;

/// <summary>
/// Class used as a base to create all the artifacts.
/// Every artifacts inherit from this class.
/// The three methods inside are overridden if necesserary.
/// </summary>
public class Artifact : MonoBehaviour
{
    public virtual void Add()
    {
        
    }

    public virtual void Remove()
    {

    }

    public virtual void Upgrade()
    {

    }
}
