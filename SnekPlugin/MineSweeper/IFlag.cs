using Cysharp.Threading.Tasks;

namespace SnekPlugin.MineSweeper;

public interface IFlag
{
    UniTask<bool> LiftAsync();
    UniTask<bool> PutDownAsync();
}