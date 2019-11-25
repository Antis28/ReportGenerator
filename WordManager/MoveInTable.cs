using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace WordManager
{
    class MoveInTable
    {
        public static Word.Range MoveRight(Word._Application app, int count = 1)
        {
            app.Selection.MoveRight(Word.WdUnits.wdCell, count);
            return app.Selection.Range;
        }
    }
}
