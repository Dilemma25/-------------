using Godot;
using System;

public partial class MovementController : Node2D
{

	public TileMapLayer floor;

	public override void _Ready()
	{
		Node mainScene = GetTree().CurrentScene;

		var floorGenerator = mainScene.GetNodeOrNull<Node2D>("FloorGenerator");
		if (floorGenerator != null)
		{
			floor = floorGenerator.GetNodeOrNull<TileMapLayer>("Floor");
			GD.Print("Получили Floor после загрузки сцены: " + floor.Name);
		}
	}

	// public override void _Input(InputEvent @event)
	// {
	// 	if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
	// 	{
	// 		Vector2 mouseScreenPos = GetViewport().GetMousePosition();
	// 		Vector2 mouseWorldPos = GetViewport().GetCanvasTransform().AffineInverse() * mouseScreenPos;

	// 		Vector2I mousePos = floor.LocalToMap(floor.ToLocal(mouseWorldPos));
	// 		Vector2I atlasCoords = floor.GetCellAtlasCoords(mousePos);

	// 		GD.Print($@"
	// 		========== Клик по тайлу ==========
	// 		Позиция на TileMap: {mousePos}
	// 		Координаты в атласе: {atlasCoords}
	// 		Проходимость: {floor.GetCellTileData(mousePos)?.GetCustomData("walkable")}
	// 		===================================
	// 		");
	// 	}
	// }

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			// Получаем глобальную позицию мыши
			Vector2 mouseGlobalPos = GetGlobalMousePosition();

			// Переводим в локальные координаты TileMap, затем получаем координаты ячейки
			Vector2I mousePos = floor.LocalToMap(floor.ToLocal(mouseGlobalPos));
			Vector2I atlasCoords = floor.GetCellAtlasCoords(mousePos);

			var tileData = floor.GetCellTileData(mousePos);
			var walkable = tileData?.GetCustomData("walkable");

			GD.Print($@"
			========== Клик по тайлу ==========
			Позиция на TileMap: {mousePos}
			Координаты в атласе: {atlasCoords}
			Проходимость: {walkable}
			===================================
			");
		}
	}
}
