using UnityEngine;

public class PlayerSwap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadChar1()
    {
        // Load karakter 1
        GameSessionData.Instance.SetGameMode(GameSessionData.GameMode.Stage);
        Debug.Log("Karakter 1 dipilih");
    }

    public void loadChar2()
    {
        // Load karakter 2
        GameSessionData.Instance.SetGameMode(GameSessionData.GameMode.Endurance);
        Debug.Log("Karakter 2 dipilih");
    }

    public void loadChar3()
    {
        // Load karakter 3
        GameSessionData.Instance.SetGameMode(GameSessionData.GameMode.None);
        Debug.Log("Karakter 3 dipilih");
    }
}
