using System;

using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Globalization;
using Utilities;
using WordManager;
using GraphicGenerator;
using System.Collections.Generic;

namespace GraphicGuardsWPF
{
    class GraphicGenerator
    {
        readonly String template31Path = Environment.CurrentDirectory + @"\Template\01_График работы_31.dot";
        readonly String template30Path = Environment.CurrentDirectory + @"\Template\01_График работы_30.dot";
        readonly String template29Path = Environment.CurrentDirectory + @"\Template\01_График работы_29.dot";
        readonly String template28Path = Environment.CurrentDirectory + @"\Template\01_График работы_28.dot";
        readonly WorkWithWord word = null;

        const String dateBookMark = "MONTH";            // Месяц, на который делается график
        const String positionBookMark = "supervisor";   // Начальник управления
        const String weekDay = "WD";            // Дни недели
        const String guard1 = "D1";            // начало дежурств сторожа
        const String guard2 = "D2";            // начало дежурств сторожа
        const String guard3 = "D3";            // начало дежурств сторожа
        readonly DateTime curentDate;

        public GraphicGenerator(AppVisibility visibility, DateTime date)
        {
            curentDate = date;

            word = new WorkWithWord();
            if (File.Exists(template31Path))
                word.CreateNewDoc(template31Path, visibility);
            else
                throw new FileLoadException("Шаблон не найден по адресу: " + template31Path);
        }
        public void Generate(String supervisor)
        {
            

            FillSuperVisor(supervisor);
            FillMonth();
            FillWeekDay();
            FillGuards();
            SaveToFile();
        }

        private void FillGuards( int guardNumber = 1)
        {
            GuardManager manager = new GuardManager(curentDate);
            manager.SetGurdContinueDuty(guardNumber);
            manager.PlanGraphic();
            
            FillGuard1(manager);
            FillGuard2(manager);
            FillGuard3(manager);

        }

        private void FillGuard1(GuardManager manager)
        {
            Word.Bookmark bookmark = word.FindBookMark(guard1);
            Guard guard = manager.GetGuard1();
            bookmark.Select();            
            FillGuard(guard, bookmark.Range);
        }
        private void FillGuard2(GuardManager manager)
        {
            Word.Bookmark bookmark = word.FindBookMark(guard2);
            Guard guard = manager.GetGuard2();
            bookmark.Select();
            FillGuard(guard, bookmark.Range);
        }
        private void FillGuard3(GuardManager manager)
        {
            Word.Bookmark bookmark = word.FindBookMark(guard3);
            Guard guard = manager.GetGuard3();
            bookmark.Select();
            FillGuard(guard, bookmark.Range);
        }

        private void FillGuard(Guard guard, Word.Range wordRange)
        {
            int hourCount = 0;
            foreach (var item in guard.GetDuty())
            {
                hourCount += item.Value;
                if (item.Value == 0)
                {
                    wordRange.Text = "";
                }
                else wordRange.Text = item.Value.ToString(); 
                wordRange = word.MoveCellRight();
            }
            wordRange.Text = hourCount.ToString();
            wordRange = word.MoveCellRight();
            wordRange.Text = guard.CountNightHours.ToString();
        }

        private void FillWeekDay()
        {
            DateTime firstDay = new DateTime(curentDate.Year, curentDate.Month, 1);
            DateTime last = firstDay.AddMonths(1).AddDays(-1);
            DateTime currentDay = firstDay;

            Word.Bookmark bookmark = word.FindBookMark(weekDay);
            bookmark.Select();

            Word.Range wordRange = bookmark.Range; // диапазон документа Word
            wordRange.Text = currentDay.ToString("ddd");

            for (currentDay = currentDay.AddDays(1); currentDay <= last; currentDay = currentDay.AddDays(1))
            {
                wordRange = word.MoveCellRight();
                wordRange.Text = currentDay.ToString("ddd");
            }
        }

        private void FillMonth()
        {
            String curDate = curentDate.ToString("MMMM", CultureInfo.CurrentCulture).ToUpper();
            Word.Bookmark bookmark = word.FindBookMark(dateBookMark);
            bookmark.Range.Text = curDate;
        }
        private void FillSuperVisor(String supervisor)
        {
            Word.Bookmark bookmark = word.FindBookMark(positionBookMark);
            bookmark.Range.Text = supervisor;
        }

        private void SaveToFile()
        {
            String folderName = "График дежурств сторожей";

            // путь для сохранения
            String newFolderPath = Environment.CurrentDirectory;    // текущй каталог exe файла
            newFolderPath = Path.Combine(newFolderPath, @"..\");    // поднятся на уровень выше
            newFolderPath = Path.Combine(newFolderPath, folderName);


            System.IO.Directory.CreateDirectory(newFolderPath);

            String fileName = curentDate.ToString("yyyy_MMMM", CultureInfo.CurrentCulture);
            String fileExtension = ".doc";
            String fullPath = Path.Combine(newFolderPath, fileName + fileExtension);

            word._application.ActiveDocument.SaveAs2(
                FileName: fullPath,
                FileFormat: Word.WdSaveFormat.wdFormatDocument
                );
        }
        public void Dispose()
        {
            word.Dispose();
        }
    }
}
