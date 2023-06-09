﻿using Cysharp.Threading.Tasks;
using SnekTech.MineSweeperRogue.Core.FSM;

namespace SnekTech.MineSweeperRogue.GridSystem.CellSystem
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
}