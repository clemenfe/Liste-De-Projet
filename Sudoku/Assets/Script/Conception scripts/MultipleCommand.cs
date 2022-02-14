using ProjetDeSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Conception_scripts
{
    public class MultipleCommand : ICommand
    {
        private List<ICommand> multipleCommand = new List<ICommand>();

        public void addCommand(ICommand command)
        {
            multipleCommand.Add(command);
        }

        public int getCommandCount()
        {
            return multipleCommand.Count();
        }

        public void execute()
        {
            
            foreach (ICommand c in multipleCommand)
                c.execute();

            GameController.Instance.orderNewMultipleCommand();
        }

        public void undo()
        {
            foreach (ICommand c in multipleCommand)
                c.undo();
        }
    }
}
