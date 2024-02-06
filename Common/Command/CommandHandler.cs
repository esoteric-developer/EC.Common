using System;
using System.Collections.Generic;

namespace EC.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandHandler
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly Stack<ICommand> _commandHistory = default;

        /// <summary>
        /// 
        /// </summary>
        protected readonly Stack<ICommand> _undoCommandHistory = default;

        /// <summary>
        /// 
        /// </summary>
        public Stack<ICommand> CommandHistory => _commandHistory;

        /// <summary>
        /// 
        /// </summary>
        public Stack<ICommand> UndoCommandHistory => _undoCommandHistory;

        /// <summary>
        /// 
        /// </summary>
        public event Action<ICommand> OnCommandExecuted;

        /// <summary>
        /// 
        /// </summary>
        public event Action<ICommand> OnCommandUndo;

        /// <summary>
        /// 
        /// </summary>
        public event Action<ICommand> OnCommandRedo;

        /// <summary>
        /// 
        /// </summary>
        public CommandHandler()
        {
            _commandHistory = new Stack<ICommand>();
            _undoCommandHistory = new Stack<ICommand>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public void Execute(ICommand command)
        {
            if (command == null) return;
            _commandHistory.Push(command);
            command.Execute();
            OnCommandExecuted?.Invoke(command);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UndoLastCommand()
        {
            ICommand lastCommand = _commandHistory.Pop();
            if (lastCommand == null) return;
            _undoCommandHistory.Push(lastCommand);
            lastCommand.Undo();
            OnCommandUndo?.Invoke(lastCommand);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RedoLastCommand()
        {
            ICommand lastCommand = _undoCommandHistory.Pop();
            if (lastCommand == null) return;
            _commandHistory.Push(lastCommand);
            lastCommand.Execute();
            OnCommandRedo?.Invoke(lastCommand);
        }
    }
}
