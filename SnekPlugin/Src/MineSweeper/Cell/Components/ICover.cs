using Cysharp.Threading.Tasks;

namespace SnekPlugin.MineSweeper.Cell.Components;

public interface ICover
{
    UniTask RevealAsync();
    UniTask PutCoverAsync();
}