using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelationalAlgebraWinFormsApp
{
    // Интерфейс ICommand определяет общую структуру команд, которые могут быть выполнены и отменены.
    internal interface ICommand
    {
        // Метод Execute выполняет команду с указанным параметром
        void Execute(object parameter);

        // Метод Undo отменяет выполнение команды
        void Undo();
    }
}