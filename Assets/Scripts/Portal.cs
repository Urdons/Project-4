using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string targetScene;

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(targetScene);
    }
}
