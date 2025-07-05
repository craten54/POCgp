using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    void Start()
    {
        // Atur resolusi ke 960x540 dalam mode windowed.
        // Untuk build, pastikan 'Resizable Window' di Player Settings juga tidak dicentang.
        Screen.SetResolution(960, 540, false);
        Debug.Log("Resolusi diatur ke 960x540.");
    }
}