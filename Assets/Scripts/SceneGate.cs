using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGate : MonoBehaviour
{
    public string destinationSceneName;

    public enum GateDirection
    {
        Up, Down,
        East, West, North, South,
        UpEast, DownEast, UpWest, DownWest, UpNorth, DownNorth, UpSouth, DownSouth, 
        Northeast, Northwest, Southeast, Southwest,     
        UpNortheast, UpNorthwest, UpSoutheast, UpSouthwest,
        DownNortheast, DownNorthwest, DownSoutheast, DownSouthwest
    }

    public GateDirection gateDirection;
    public float moveDistance = 13f;  // プレイヤーをどのくらい壁から離すか

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log($"Player entered gate to {destinationSceneName} via {gateDirection}");

        Vector3 offset = GetOffsetByDirection(gateDirection);

        Vector3 currentPosition = other.transform.position;
        Vector3 newPosition = currentPosition + offset;

        PlayerPrefs.SetFloat("posX", newPosition.x);
        PlayerPrefs.SetFloat("posY", newPosition.y);
        PlayerPrefs.SetFloat("posZ", newPosition.z);
        PlayerPrefs.Save();

        SceneManager.LoadScene(destinationSceneName);
    }

    private Vector3 GetOffsetByDirection(GateDirection dir)
    {
        float d = moveDistance;
        switch (dir)
        {
            case GateDirection.Up: return new Vector3(0, d, 0);
            case GateDirection.Down: return new Vector3(0, -d, 0);

            case GateDirection.East: return new Vector3(d, 0, 0);
            case GateDirection.West: return new Vector3(-d, 0, 0);
            case GateDirection.North: return new Vector3(0, 0, d);
            case GateDirection.South: return new Vector3(0, 0, -d);

            case GateDirection.UpEast: return new Vector3(d, d, 0);
            case GateDirection.DownEast: return new Vector3(d, -d, 0);
            case GateDirection.UpWest: return new Vector3(-d, d, 0);
            case GateDirection.DownWest: return new Vector3(-d, -d, 0);
            case GateDirection.UpNorth: return new Vector3(0, d, d);
            case GateDirection.DownNorth: return new Vector3(0, -d, d);
            case GateDirection.UpSouth: return new Vector3(0, d, -d);
            case GateDirection.DownSouth: return new Vector3(0, -d, -d);

            case GateDirection.Northeast: return new Vector3(d, 0, d);
            case GateDirection.Northwest: return new Vector3(-d, 0, d);
            case GateDirection.Southeast: return new Vector3(d, 0, -d);
            case GateDirection.Southwest: return new Vector3(-d, 0, -d);

            case GateDirection.UpNortheast: return new Vector3(d, d, d);
            case GateDirection.UpNorthwest: return new Vector3(-d, d, d);
            case GateDirection.UpSoutheast: return new Vector3(d, d, -d);
            case GateDirection.UpSouthwest: return new Vector3(-d, d, -d);

            case GateDirection.DownNortheast: return new Vector3(d, -d, d);
            case GateDirection.DownNorthwest: return new Vector3(-d, -d, d);
            case GateDirection.DownSoutheast: return new Vector3(d, -d, -d);
            case GateDirection.DownSouthwest: return new Vector3(-d, -d, -d);

            default: return Vector3.zero;
        }
    }
}