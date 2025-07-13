extends Node2D

@onready var tile_map: TileMapLayer = $"../FloorGenerator/Floor"

var astar_grid: AStarGrid2D
var current_id_path: Array[Vector2i] = []
var current_point_path: PackedVector2Array
var target_position: Vector2
var is_moving: bool = false


func _ready():
	astar_grid = AStarGrid2D.new()
	astar_grid.region = tile_map.get_used_rect()
	astar_grid.cell_size = Vector2(16, 16)
	astar_grid.diagonal_mode = AStarGrid2D.DIAGONAL_MODE_NEVER
	astar_grid.update()

	for x in tile_map.get_used_rect().size.x:
		for y in tile_map.get_used_rect().size.y:
			var tile_position = Vector2i(
				x + tile_map.get_used_rect().position.x,
				y + tile_map.get_used_rect().position.y
			)

			var tile_data = tile_map.get_cell_tile_data(tile_position)
			if tile_data == null or tile_data.get_custom_data("walkable") == false:
				astar_grid.set_point_solid(tile_position)


func _input(event):
	if not event.is_action_pressed("MoveMouse"):
		return

	var id_path: Array[Vector2i]
	var mouse_map_pos := tile_map.local_to_map(tile_map.to_local(get_global_mouse_position()))
	var tile_data := tile_map.get_cell_tile_data(mouse_map_pos)
	var walkable: bool = tile_data != null and tile_data.get_custom_data("walkable")

	print("========== Клик по тайлу ==========")
	print("Позиция мыши на карте (индекс тайла): ", mouse_map_pos)
	print("Позиция тайла в локальных координатах TileMap (пиксели): ", tile_map.map_to_local(mouse_map_pos))
	print("Проходимость: ", walkable)
	print("===================================")

	if is_moving:
		id_path = astar_grid.get_id_path(
			tile_map.local_to_map(target_position),
			mouse_map_pos
		)
	else:
		id_path = astar_grid.get_id_path(
			tile_map.local_to_map(global_position),
			mouse_map_pos
		).slice(1)

	if not id_path.is_empty():
		current_id_path = id_path
		current_point_path = astar_grid.get_point_path(
			tile_map.local_to_map(target_position),
			mouse_map_pos
		)



func _physics_process(delta):
	if current_id_path.is_empty():
		return

	if not is_moving:
		target_position = tile_map.map_to_local(current_id_path.front())
		is_moving = true

	global_position = global_position.move_toward(target_position, 1.0)

	if global_position == target_position:
		current_id_path.pop_front()

		if not current_id_path.is_empty():
			target_position = tile_map.map_to_local(current_id_path.front())
		else:
			is_moving = false
