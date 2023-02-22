﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SnekPlugin.MineSweeper;

public class List2D<T> : IEnumerable<T>
{
    private readonly List<List<T>> _matrix = new List<List<T>>();

    public List2D()
    {
    }

    public List2D(T[,] array2D)
    {
        ResetWith(array2D);
    }

    public void AddRow(List<T> newRow)
    {
        _matrix.Add(newRow);
    }

    public void Clear() => _matrix.Clear();

    public void ResetWith(T[,] array2D)
    {
        Clear();

        var (rows, columns) = (array2D.GetLength(0), array2D.GetLength(1));

        for (var i = 0; i < rows; i++)
        {
            var elementRow = new List<T>();
            for (var j = 0; j < columns; j++)
            {
                elementRow.Add(array2D[i, j]);
            }
            _matrix.Add(elementRow);
        }
    }

    public List<T> this[int rowIndex] => _matrix[rowIndex];
    
    public IEnumerator<T> GetEnumerator()
    {
        return _matrix.SelectMany(row => row).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}