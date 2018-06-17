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

    public bool IsInverted { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            IsInverted = false;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleInvert()
    {
        InvertState(!IsInverted);
    }

    public void InvertState(bool argIsInverted)
    {
        if (IsInverted == argIsInverted)
        {
            return;
        }

        IsInverted = argIsInverted;
        if (OnInvert != null)
        {
            OnInvert.Invoke(IsInverted);
        }
    }
}
