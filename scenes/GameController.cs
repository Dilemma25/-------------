using Godot;
using System;

public partial class GameController : Node
{
	[Export]
	public NodePath tileMapLayerPath;

	public TileMapLayer tileMapLayer;
	public override void _Ready()
	{
		tileMapLayer = GetNode<TileMapLayer>(tileMapLayerPath);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			Vector2 mouseScreenPos = GetViewport().GetMousePosition();
			Vector2 mouseWorldPos = GetViewport().GetCanvasTransform().AffineInverse() * mouseScreenPos;

			Vector2I tilePos = tileMapLayer.LocalToMap(mouseWorldPos);

			bool isWalkable = IsTileWalkable(tilePos);
			GD.Print($"Клик по тайлу: {tilePos}, Проходимый: {isWalkable}");
		}
	}

	private bool IsTileWalkable(Vector2I tilePos)
	{
		// Получаем ID тайла в позиции tilePos
		int tileId = tileMapLayer.GetCellSourceId(tilePos);
		if (tileId == -1)
			return false; // Пустая клетка — непроходима

		// Получаем TileData по позиции, слою и ID
		TileData tileData = tileMapLayer.GetCellTileData(tilePos);
		if (tileData == null)
			return false;

		// Получаем кастомные данные слоя "Walkable"
		bool walkableVariant = (bool)tileData.GetCustomData("Walkable");
		return walkableVariant;
	}
}
