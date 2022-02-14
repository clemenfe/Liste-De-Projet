using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDeSession
{
    sealed class CommandInvoker
    {
        private static CommandInvoker instance;

        public static CommandInvoker Instance
        {
            get
            {
                if (instance == null)
                    instance = new CommandInvoker();

                return instance;
            }
        }

        private CommandInvoker() { }

        private Stack<ICommand> executedCommands = new Stack<ICommand>();
        private Stack<ICommand> undidCommands = new Stack<ICommand>();

        public void execute(ICommand command)
        {
            executedCommands.Push(command);
            undidCommands.Clear();
            command.execute();
        }

        public void undo()
        {
            if (executedCommands.Any()) //S'il y a des commands dans le stack, on pop, aussi non, on ne fait aucune action
            {
                ICommand command = executedCommands.Pop();
                undidCommands.Push(command);
                command.undo();
            }

        }

        public void redo()
        {
            if (undidCommands.Any()) //S'il y a des commands dans le stack, on pop, aussi non, on ne fait aucune action
            {
                ICommand command = undidCommands.Pop();
                executedCommands.Push(command);
                command.execute();
            }

        }
    }
}
