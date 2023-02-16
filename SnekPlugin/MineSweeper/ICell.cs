using Cysharp.Threading.Tasks;

namespace SnekPlugin.MineSweeper;

public interface ICell
{
    GridIndex Index { get; }
    IGrid Parent { get; }

    ICover Cover { get; }
    IFlag Flag { get; }

    UniTask<bool> Reveal();
    UniTask<bool> SwitchFlag();
    
    bool HasBomb { get; }
    bool IsFlagged { get; }
    bool IsCovered { get; }
    bool IsRevealed { get; }
}