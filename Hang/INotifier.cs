using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hang
{
    interface INotifiable
    {
        void ShowIntroMessage();
        void ShowCurrentWord();
        void ShowGameOverMessage();
        void ShowInsertLetterMessage();
        void ShowExistNotExistLetterInWordMessage();
    }
}