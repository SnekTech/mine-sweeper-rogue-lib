using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekPlugin.Core.FSM;
using SnekPlugin.MineSweeper.Cell.Components;

namespace SnekPlugin.MineSweeper.Cell.StateMachine
{
    public abstract class CellState : IAsyncState
    {
        protected readonly CellStateMachine StateMachine;
        protected ICell Cell => StateMachine.Context;

        protected CellState(CellStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract CellStateValue Value { get; }

        public abstract UniTask OnEnter();
        public abstract UniTask OnExit();
        public abstract UniTask OnReveal();
        public abstract UniTask OnSwitchFlag();
    }

    public enum CellStateValue
    {
        Covered,
        Flagged,
        Revealed,
    }

    public static class CellStateExtensions
    {
        public static readonly (string covered, string flagged, string revealed) CellEmojis
            = ("🔳", "⛳", "💢");
        
        public static CellStateValue ToCellState(string cellEmoji)
        {
            if (!stateValueByEmoji.ContainsKey(cellEmoji))
            {
                throw new ArgumentException($"no matching state for {nameof(cellEmoji)}: {cellEmoji}");
            }

            return stateValueByEmoji[cellEmoji];
        }

        private static readonly Dictionary<string, CellStateValue> stateValueByEmoji =
            new Dictionary<string, CellStateValue>
            {
                {CellEmojis.covered, CellStateValue.Covered},
                {CellEmojis.flagged, CellStateValue.Flagged},
                {CellEmojis.revealed, CellStateValue.Revealed},
            };
    }
}