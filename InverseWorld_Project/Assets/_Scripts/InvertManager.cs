using UnityEngine;

/// <summary>
/// 
/// 
/// Ruben Sanchez
/// 
/// </summary>

public class InvertManager : MonoBehaviour
{
    public static InvertManager Instance;

    public delegate void Invert(bool isInverted);
    public event Invert OnInvert;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    public void InvertState(bool isInverted)
    {
        if(OnInvert != null)
            OnInvert.Invoke(isInverted);
    }
}
