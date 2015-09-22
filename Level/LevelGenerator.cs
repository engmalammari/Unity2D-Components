﻿using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Matcha.Lib;

public class LevelGenerator : CacheBehaviour {

    public Brush brush;
    public Brush erase;

    private TileSystem tileSystem;
    private float columns;
    private float rows;

	void Awake()
    {
        tileSystem = GameObject.Find(TILE_MAP).GetComponent<TileSystem>();
        columns = tileSystem.ColumnCount;
        rows = tileSystem.RowCount;
        PaintBaseTiles();
        CarveRandomRooms();
	}

    void PaintBaseTiles() {

        tileSystem.BeginBulkEdit();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    brush.Paint(tileSystem, r, c);
                    tileSystem.RefreshSurroundingTiles(r, c);
                }
            }

        tileSystem.EndBulkEdit();
    }

    void CarveRandomRooms()
    {
        int numberOfRooms = 5;
        ProceduralRoom roomToDraw = new ProceduralRoom();

        for (int i = 0; i < numberOfRooms; i++)
        {
            GetRoom(roomToDraw);
            // Debug.Log("w = " + roomToDraw.width + ". H = " + roomToDraw.height);
            PaintRoom(roomToDraw);
        }
    }

    void GetRoom(ProceduralRoom room)
    {
        room.width  = (int) MLib.NextGaussian(10f, 5f, 1f, 50f);
        room.height = (int) MLib.NextGaussian(10f, 5f, 1f, 50f);
    }

    void PaintRoom(ProceduralRoom room)
    {
        // int x = (int) MLib.NextGaussian(columns / 2, 30f, 1f, columns);
        // int y = (int) MLib.NextGaussian(rows / 2, 30f, 1f, rows);

        for (int r = 0; r < room.width; r++)
        {
            for (int c = 0; c < room.height; c++)
            {
                tileSystem.EraseTile(r, c);
                // brush.Paint(tileSystem, r, c);
                tileSystem.RefreshSurroundingTiles(r, c);
            }
        }
    }


    // void PaintExampleTiles() {
    //     tileSystem.BeginBulkEdit();
    //         PaintTwoTiles(5, 5);
    //         PaintTwoTiles(6, 5);
    //     tileSystem.EndBulkEdit();
    // }

    // void Update()
    // {
    //     // Find mouse position in world space
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     // Nearest point where ray intersects tile system
    //     TileIndex ti = tileSystem.ClosestTileIndexFromRay(ray);

    //     if (Input.GetMouseButtonDown(0)) {
    //         // Paint with left mouse button
    //         brush.Paint(tileSystem, ti);
    //         tileSystem.RefreshSurroundingTiles(ti);
    //     }
    //     else if (Input.GetMouseButtonDown(1)) {
    //         // Erase with right mouse button
    //         tileSystem.EraseTile(ti);
    //         tileSystem.RefreshSurroundingTiles(ti);
    //     }
    // }

    // void CalculateTileSystemSize()
    // {
    //     tileSystemSize = new Vector3(
    //         tileSystem.ColumnCount * tileSystem.CellSize.x,
    //         tileSystem.RowCount * tileSystem.CellSize.y,
    //         tileSystem.CellSize.z
    //     );
    // }
}
