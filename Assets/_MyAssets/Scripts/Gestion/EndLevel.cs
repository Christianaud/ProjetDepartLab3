using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            int noScene = SceneManager.GetActiveScene().buildIndex;

            //V?rifier si derni?re sc?ne de jeu
            if (noScene < SceneManager.sceneCountInBuildSettings - 2)
            {
                //Passer ? la sc?ne suivante
                SceneManager.LoadScene(noScene + 1);
            }
            else
            {
                GameManager.Instance.EndTime = Time.time - GameManager.Instance.StartTime;
                SceneManager.LoadScene(noScene + 1);
            }
        }
    }
}

