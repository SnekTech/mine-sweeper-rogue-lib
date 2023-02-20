﻿using Cysharp.Threading.Tasks;

namespace SnekPlugin.MineSweeper.Cell.Components;

public interface IFlag
{
    UniTask LiftAsync();
    UniTask PutDownAsync();
}