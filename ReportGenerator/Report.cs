using System;

using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Globalization;

namespace ReportGenerator
{
    class Report : IDisposable
    {
        readonly String cleanThursdayPath = Environment.CurrentDirectory + @"\Template\отчет #Чистый четверг#.dot";
        readonly WorkWithWord word = null;

        public Report()
        {
            word = new WorkWithWord();
            if (File.Exists(cleanThursdayPath))
                word.CreateNewDoc(cleanThursdayPath);
            else
                throw new Exception("Шаблон для чистого четверга не найден");
        }

        public void FillDate()
        {
            DateTime date = DateTime.Now;
            String curDate = date.ToString("dd.MM.yyyy",CultureInfo.CurrentCulture);
            Word.Bookmark bookmark = word.FindByBookMark(BookmarksName.DateBookMark());
            bookmark.Range.Text = curDate;
        }
        public void FillPhotoCount(int photoCount)
        {
            Word.Bookmark bookmark = word.FindByBookMark(BookmarksName.PhotoCountBookMark());
            bookmark.Range.Text = photoCount + " " + Utils.GetDeclensionFiles(photoCount);
        }
        public void FillWork(String works)
        {
            Word.Range bookmarkRange = word.FindByBookMark(BookmarksName.WorkBookMark()).Range;
            bookmarkRange.Text = works;

            object listBehavior = Word.WdDefaultListBehavior.wdWord10ListBehavior;
            bookmarkRange.ListFormat.ApplyBulletDefault(ref listBehavior);

            Word.ListTemplate markedListTemplate = word.CreateListTemplate();
            bookmarkRange.ListFormat.ApplyListTemplate(markedListTemplate, false,
                                            Word.WdListApplyTo.wdListApplyToSelection, listBehavior);

            bookmarkRange.ParagraphFormat = word.CreateParagraphTemplate(bookmarkRange);
        }
        public void FillSuperVisor(String supervisor)
        {
            Word.Bookmark bookmark = word.FindByBookMark(BookmarksName.PositionBookMark());
            bookmark.Range.Text = supervisor;
        }
        public void SaveToFile()
        {
            String folderName = "Чистый четверг - " + DateTime.Now.ToString("yyyy_MM_dd",CultureInfo.CurrentCulture);

            // путь для сохранения
            String newFolderPath = Environment.CurrentDirectory;    // текущй каталог exe файла
            newFolderPath = Path.Combine(newFolderPath, @"..\");    // поднятся на уровень выше
            newFolderPath = Path.Combine(newFolderPath, folderName);


            System.IO.Directory.CreateDirectory(newFolderPath);

            String fileName = "Сопроводиловка на адм.ЦГР";
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
