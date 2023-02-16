using Cysharp.Threading.Tasks;

namespace SnekPlugin.MineSweeper;

public interface ICover
{
    UniTask<bool> RevealAsync();
    UniTask<bool> PutCoverAsync();
}