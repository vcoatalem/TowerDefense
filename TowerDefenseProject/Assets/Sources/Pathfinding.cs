using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//code from https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/


class Tile
{
	public int X { get; set; }
	public int Y { get; set; }
	public int Cost { get; set; }
	public int Distance { get; set; }
	public int CostDistance => Cost + Distance;
	public Tile Parent { get; set; }
/*
	public Tile(float x, float y)
    {
		X = (int)x;
		Y = (int)y;
    }
*/
	//The distance is essentially the estimated distance, ignoring walls to our target. 
	//So how many tiles left and right, up and down, ignoring walls, to get there. 
	public void SetDistance(int targetX, int targetY)
	{
		this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
	}
}

public class Pathfinding
{

	private static List<Tile> GetWalkableTiles(MapController.CellState[,] grid, Tile currentTile, Tile targetTile)
	{
		var possibleTiles = new List<Tile>()
		{
			new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
			new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
			new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
			new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
		};

		possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

		var maxX = grid.GetLength(1);//map.First().Length - 1;
		var maxY = grid.GetLength(0);//map.Count - 1;
		//Debug.Log(maxX + " " + maxY);
		//Debug.Log(grid[0, 0]);
		//Debug.Log(grid[0, 1]);

		return possibleTiles //mb invert X and Y ?
				.Where(tile => tile.X >= 0 && tile.X < maxX)
				.Where(tile => tile.Y >= 0 && tile.Y < maxY)
				.Where(tile => grid[tile.Y,tile.X] == MapController.CellState.EMPTY || (tile.X == targetTile.X && tile.Y == targetTile.Y))
				.ToList();
	}

	public static List<Vector2> FindPath(MapController.CellState[,] grid, Vector2 from, Vector2 to)
    {

		Debug.Log("from: " + from.ToString());
		Debug.Log("to: " + to.ToString());
		var start = new Tile { X = (int)from.y, Y = (int)from.x };
		var end = new Tile { X = (int)to.y, Y = (int)to.x };

		start.SetDistance(end.X, end.Y);

		var activeTiles = new List<Tile>();
		activeTiles.Add(start);
		var visitedTiles = new List<Tile>();

		while (activeTiles.Any())
		{
			var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

			if (checkTile.X == end.X && checkTile.Y == end.Y)
			{
				List<Vector2> res = new List<Vector2>();
				var tile = checkTile;
				//Debug.Log("Retracing steps backwards...");
				while (true)
				{
					//Debug.Log($"x: {tile.X} / y: {tile.Y}");
					if (tile.Parent != null)
                    {
						res.Add(new Vector2(tile.Y, tile.X));
					}
					tile = tile.Parent;
					if (tile == null)
					{
						res.Reverse();
						return res;
					}
				}
			}

			visitedTiles.Add(checkTile);
			activeTiles.Remove(checkTile);

			var walkableTiles = GetWalkableTiles(grid, checkTile, end);

			foreach (var walkableTile in walkableTiles)
			{
				//We have already visited this tile so we don't need to do so again!
				if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
					continue;

				//It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
				if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
				{
					var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
					if (existingTile.CostDistance > checkTile.CostDistance)
					{
						activeTiles.Remove(existingTile);
						activeTiles.Add(walkableTile);
					}
				}
				else
				{
					//We've never seen this tile before so add it to the list. 
					activeTiles.Add(walkableTile);
				}
			}
		}

		Console.WriteLine("No Path Found!");
		return null;

	}


}
