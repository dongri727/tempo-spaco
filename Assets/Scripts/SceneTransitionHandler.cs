using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionHandler : MonoBehaviour
{
        void Awake()
    {
        // シーンをまたいでもこのPlayerオブジェクトを破棄しない
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        // プレイヤー移動や視点の処理など
    }
    void OnCollisionEnter(Collision collision)
    {
        SceneGate gate = collision.gameObject.GetComponent<SceneGate>();

        if (gate != null)
        {
            Vector3 currentPos = transform.position;
            Vector3 newPos = Vector3.Scale(currentPos, gate.positionMultiplier);

            PlayerPrefs.SetFloat("posX", newPos.x);
            PlayerPrefs.SetFloat("posY", newPos.y);
            PlayerPrefs.SetFloat("posZ", newPos.z);

            SceneManager.LoadScene(gate.destinationSceneName);
        }
    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey("posX"))
        {
            transform.position = new Vector3(
                PlayerPrefs.GetFloat("posX"),
                PlayerPrefs.GetFloat("posY"),
                PlayerPrefs.GetFloat("posZ")
            );
        }
    }
}
