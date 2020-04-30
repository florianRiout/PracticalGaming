using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Revive()
    {
        GameManager.Player.Respawn();
    }

    public void Resume()
    {
        GameManager.Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Settings()
    {

    }

    public void Save()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
