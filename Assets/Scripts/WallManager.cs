using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private int count = 11;
    [SerializeField] private GameObject player;


    private GameObject[] _leftWalls;
    private GameObject[] _rightWalls;
    private float[] _positions;

    private Vector3 _scale;

    void Start()
    {
        _scale = wallPrefab.transform.localScale;

        _leftWalls = new GameObject[count];
        _rightWalls = new GameObject[count];
        _positions = new float[count];
        InitWalls();
    }

    private void InitWalls()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject wall = Instantiate(wallPrefab);
            wall.transform.position = new Vector3(2, (count / 2.0f) - (i * _scale.y) - .5f, 0);
            _positions[i] = (count / 2.0f * _scale.y) - (i * _scale.y) - .5f;
            _rightWalls[i] = wall;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject wall = Instantiate(wallPrefab);
            wall.transform.position = new Vector3(-2, count / 2.0f - (i * _scale.y) - .5f, 0);
            _leftWalls[i] = wall;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var playerPosition = player.transform.position;
        int playerBlock = Mathf.FloorToInt(playerPosition.y);

        for (var i = 0; i < count; i++)
        {
            _leftWalls[i].transform.position =
                new Vector3(_leftWalls[i].transform.position.x, _positions[i] + playerBlock);
            _rightWalls[i].transform.position =
                new Vector3(_rightWalls[i].transform.position.x, _positions[i] + playerBlock);
        }
    }
}