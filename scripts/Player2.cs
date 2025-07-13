using Godot;


public partial class Player2 : Node2D
{
    public AStarGrid2D astarGrid;
    public Godot.Collections.Array<Vector2I> currentIDPath;
    public Vector2 targetPosition;
    public bool isMoving;
    public TileMapLayer tileMap;
    public Godot.Vector2[] currentPointPath;

    public override void _Ready()
    {
        tileMap = GetNode<Node2D>("../FloorGenerator").GetNodeOrNull<TileMapLayer>("Floor");

        astarGrid = new AStarGrid2D();
        astarGrid.Region = tileMap.GetUsedRect();
        astarGrid.CellSize = new Vector2(16, 16);
        astarGrid.DiagonalMode = AStarGrid2D.DiagonalModeEnum.Never;
        astarGrid.Update();

        Rect2I usedRect = tileMap.GetUsedRect();

        for (int x = 0; x < usedRect.Size.X; x++)
        {
            for (int y = 0; y < usedRect.Size.Y; y++)
            {
                // Сначала абсолютная координата тайла
                Vector2I tilePosition = new Vector2I(
                    x + usedRect.Position.X,
                    y + usedRect.Position.Y
                );

                var tileData = tileMap.GetCellTileData(tilePosition);

                // Учитываем, что tileData может быть null
                if (tileData == null || !(bool)tileData.GetCustomData("walkable"))
                {
                    GD.Print($"Тайл {tilePosition}, есть данные: {tileData != null}, проходимый: {tileData?.GetCustomData("walkable")}");
                    astarGrid.SetPointSolid(tilePosition);
                }
            }
        }

    }


    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("MoveMouse") == false)
        {
            return;
        }

        Godot.Collections.Array<Vector2I> idPath;

        if (isMoving)
        {
            idPath = astarGrid.GetIdPath(
                tileMap.LocalToMap(targetPosition),
                tileMap.LocalToMap(GetGlobalMousePosition())
            );
        }
        else
        {
            idPath = astarGrid.GetIdPath(
                tileMap.LocalToMap(GlobalPosition),
                tileMap.LocalToMap(GetGlobalMousePosition())
            ).Slice(1);
        }

        if (idPath.Count != 0)
        {
            currentIDPath = idPath;

            currentPointPath = astarGrid.GetPointPath(
                tileMap.LocalToMap(targetPosition),
                tileMap.LocalToMap(GetGlobalMousePosition())
        );

        }


    }


    public override void _PhysicsProcess(double delta)
    {
        if (currentIDPath == null || currentIDPath.Count == 0)
        {
            return;
        }

        if (tileMap == null)
        {
            GD.PrintErr("tileMap is null in _PhysicsProcess!");
            return;
        }

        if (!isMoving)
        {
            targetPosition = tileMap.MapToLocal(currentIDPath[0]);
            isMoving = true;
        }

        GlobalPosition = GlobalPosition.MoveToward(targetPosition, 1);

        if (GlobalPosition == targetPosition)
        {
            currentIDPath.RemoveAt(0);

            if (currentIDPath.Count != 0)
            {
                targetPosition = tileMap.MapToLocal(currentIDPath[0]);
            }
            else
            {
                isMoving = false;
            }
        }

    }
}

// extends Node2D
// @onready var tile_map = $"../TileMap"
// var astar grid: AStarGrid2D
// var current_id_path: Array [Vector2i]
// var current point_path: Packed Vector2Array
// var target_position: Vector2
// var is moving: bool

// func input (event):
// 1 >if event.is_action_pressed ("move") == false:
// >1 기 return
// 기
// >1 var id_path
// 기
// >1 if is_moving:
// 1 >기 id_path = astar_grid.get_id_path(
// > 기 기 tile_map.local_to_map (target_position),
// >1 기 기 tile_map.local_to_map (get_global_mouse_position
// >1 기 )
// > else:
// > 기 id_path = astar_grid.get_id_path(
// >1 기 > tile_map.local_to_map (global_position),
// >1 기 >1 tile_map.local_to_map (get_global_mouse_position
// 기 기 ).slice(1)
// >1
// if id_path.is_empty() == false:
// >1 기 current_id_path = id_path
// 기 기
// > 기 current_point_path = astar_grid.get_point_path(
// >1 기 기 tile_map.local_to_map (target_position),
// اد >1 >1 tile_map.local_to_map (get_global_mouse_position
// >1 기 )


// func_ready():
// 1 >astar grid AStarGrid2D.new()
// >1 astar _grid.region = tile_map.get_used_rect()
// 기 astar grid.cell_size = Vector2(16, 16)
// > astar grid.diagonal_mode = AStarGrid2D.DIAGONAL_MODE_NEVER
// 기 astar grid.update()
// اد
// >1 for x in tile_map.get_used_rect().size.x:
// >1 기 for y in tile_map.get_used_rect().size.y:
// v> 기 기 var tile position = Vector2i(
// >1 기 기 » x + tile_map.get_used_rect().position.x,
// 기 기 기 » y + tile_map.get_used_rect().position.y
// >1 기 기 )
// >1 기 기
// >1 기 기 var tile_data = tile_map.get_cell_tile_data(0, tile_position)
// 기 기 >
// 1 >기 기 if tile_data == null or tile_data.get_custom_data("walkable") == false:
// >1 > 기 » astar_grid.set_point_solid (tile_position)


// func_physics_process (delta):
// > if current_id_path.is_empty():
// >1 기 return
// 기
// > if is moving == false:
// 기 기 target_position = tile_map.map_to_local (current_id_path.front())
// 기 기 is moving true
// >
// > global_position = global_position.move_toward (target_position, 1)
// >1
// > if global_position == target_position:
// 기 기 current_id_path.pop_front()
// 기 기
// 1 >기 if current_id_path.is_empty() == false:
// 기 기 1 >target_position = tile_map.map_to_local(current_id_path.front())
// >1 기 else:
// >1 기 기 is moving false