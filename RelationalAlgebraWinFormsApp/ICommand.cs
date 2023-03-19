using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelationalAlgebraWinFormsApp
{
    internal interface ICommand
    {
        void Execute(object parameter);
        void Undo();
    }
}
