using Cysharp.Threading.Tasks;

namespace SnekPlugin.MineSweeper.Cell.Components;

public interface IFlag
{
    UniTask<bool> LiftAsync();
    UniTask<bool> PutDownAsync();
}