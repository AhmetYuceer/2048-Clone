using UnityEngine;

namespace _Main.Scripts.Controller
{
    public class CameraController :MonoBehaviour
    {
        public static CameraController Instance;
        
        private Camera _camera;
        [SerializeField] private Vector3 _cameraPositionOffset;
        [SerializeField] private float _cameraSizeOffset;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        public void CenterCameraOnGrid(int xCount, int yCount)
        {
            Vector3 gridCenter = new Vector3((xCount - 1) / 2f, (yCount - 1) / 2f, -10f);
            _camera.transform.position = gridCenter + _cameraPositionOffset;
            float gridSize = Mathf.Max(xCount, yCount);
            _camera.orthographicSize = (gridSize / 2f + 1f) + _cameraSizeOffset;
        }
    }
}