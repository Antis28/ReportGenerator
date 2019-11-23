using System;

using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Globalization;
using Utilities;
using WordManager;

namespace Reports
{
    public class ReportThursday : IDisposable
    {
        const String dateBookMark = "Date";
        const String photoCountBookMark = "PhotoCount";
        const String positionBookMark = "Position";
        const String workBookMark = "Work";


        readonly String cleanThursdayPath = Environment.CurrentDirectory + @"\Template\отчет #Чистый четверг#.dot";
        readonly WorkWithWord word = null;

        public ReportThursday()
        {
            word = new WorkWithWord();
            if (File.Exists(cleanThursdayPath))
                word.CreateNewDoc(cleanThursdayPath);
            else
                throw new FileLoadException("Шаблон не найден по адресу: " + cleanThursdayPath);
        }
        public void GenerateThursday(String works, String supervisor, int photoCount = 1)
        {
            FillDate();
            FillPhotoCount(photoCountBookMark, photoCount);
            FillWork(works);
            FillSuperVisor(supervisor);
            SaveToFile();
        }

        private void FillDate()
        {
            DateTime date = DateTime.Now;
            String curDate = date.ToString("dd.MM.yyyy", CultureInfo.CurrentCulture);
            Word.Bookmark bookmark = word.FindBookMark(dateBookMark);
            bookmark.Range.Text = curDate;
        }
        private void FillPhotoCount(String bookMark, int photoCount)
        {
            Word.Bookmark bookmark = word.FindBookMark(bookMark);
            bookmark.Range.Text = photoCount + " " + WordEnds.GetDeclensionFiles(photoCount);
        }
        private void FillWork(String works)
        {
            Word.Range bookmarkRange = word.FindBookMark(workBookMark).Range;
            bookmarkRange.Text = works;

            object listBehavior = Word.WdDefaultListBehavior.wdWord10ListBehavior;
            bookmarkRange.ListFormat.ApplyBulletDefault(ref listBehavior);

            Word.ListTemplate markedListTemplate = word.CreateListTemplate();
            bookmarkRange.ListFormat.ApplyListTemplate(markedListTemplate, false,
                                            Word.WdListApplyTo.wdListApplyToSelection, listBehavior);

            bookmarkRange.ParagraphFormat = word.CreateParagraphTemplate(bookmarkRange);
        }
        private void FillSuperVisor(String supervisor)
        {
            Word.Bookmark bookmark = word.FindBookMark(positionBookMark);
            bookmark.Range.Text = supervisor;
        }
        private void SaveToFile()
        {
            String folderName = "Чистый четверг - " + DateTime.Now.ToString("yyyy_MM_dd", CultureInfo.CurrentCulture);

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
