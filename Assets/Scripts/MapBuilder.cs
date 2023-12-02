using System;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    /// <summary>
    /// Данный метод вызывается автоматически при клике на кнопки с изображениями тайлов.
    /// В качестве параметра передается префаб тайла, изображенный на кнопке.
    /// Вы можете использовать префаб tilePrefab внутри данного метода.
    /// </summary>
    [SerializeField] private Grid _grid;

    private Tile[,] _tiles = new Tile[10, 10];
    private Tile _tilePrefab;
   
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        
    }

    private void Update()
    {
        if (_tilePrefab != null)
        {
            var mousePosition = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var info, 50f))
            {
                var point = info.point;
                var cellPosition = _grid.WorldToCell(point);
                var cellCenterWorld = _grid.GetCellCenterWorld(cellPosition);
                
                Debug.Log(cellPosition.x);
                Debug.Log(cellPosition.z);
                
                var available = !(cellPosition.x<0 || cellPosition.x>=10);

                if ( cellPosition.z<0 || cellPosition.z>=10)
                {
                    available = false;
                }

                if (available && IsPlaceTaken(cellPosition))
                {
                    available = false;
                }
                _tilePrefab.transform.position = new Vector3(cellCenterWorld.x,0,cellCenterWorld.z);
                _tilePrefab.SetTransparent(available);
                
                _tilePrefab.transform.position = new Vector3(cellCenterWorld.x,0,cellCenterWorld.z);
                if (available)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceFlyingBuilding(cellPosition);
                       
                    }
                }
            }
        }
    }
    private bool IsPlaceTaken(Vector3 point)
    {
       
        for (int x = 0; x < _tilePrefab.Size.x; x++)
        {
            for (int y = 0; y < _tilePrefab.Size.y; y++)
            {
                if (_tiles[(int)point.x, (int)point.z] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void PlaceFlyingBuilding(Vector3 point)
    {
        for (int x = 0; x < _tilePrefab.Size.x; x++)
        {
            for (int y = 0; y < _tilePrefab.Size.y; y++)
            {
                var tile = Instantiate(_tilePrefab);
                tile.SetNormal();
                _tiles[(int)point.x, (int)point.z] = tile ;
            }
        }
    }
    public void StartPlacingTile(Tile tilePrefab)
    {
        if (_tilePrefab != null)
        {
            Destroy(_tilePrefab.gameObject);
        }
        _tilePrefab = Instantiate(tilePrefab);
        Debug.Log(tilePrefab);
    }
}