using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNewArea : MonoBehaviour
{
    [SerializeField, Tooltip("Set the scene this door will open through index.")] private int nextSceneIndex;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PlayerTag>())
        {
            //OpenNewScene();
            Debug.Log("Load Scene");
        }
    }

    private void OpenNewScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
