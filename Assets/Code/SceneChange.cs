using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene1()
    {
        SceneManager.LoadScene("GamePlay1");
    }
    public void loadScene2()
    {
        SceneManager.LoadScene("GamePlay2");
    }
}

