using System;
using System.Collections.Generic;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;


namespace ReportGenerator
{
    class WorkWithWord: IDisposable
    {
        public Word._Application _application;
        Word._Document _document;

        Object _missingObj = System.Reflection.Missing.Value;
        Object _trueObj = true;
        Object _falseObj = false;


        public void CreateNewDoc(String path)
        {
            //создаем обьект приложения word
            _application = new Word.Application();
            // создаем путь к файлу
            Object templatePathObj = path;

            // если вылетим на этом этапе, приложение останется открытым
            try
            {
                _document = _application.Documents.Add(ref templatePathObj, ref _missingObj, ref _missingObj, ref _missingObj);
            }
            catch (Exception error)
            {
                _document.Close(ref _falseObj, ref _missingObj, ref _missingObj);
                _application.Quit(ref _missingObj, ref _missingObj, ref _missingObj);
                _document = null;
                _application = null;
                throw error;
            }
            _application.Visible = true;

        }        

        public void FindAndReplace(String strToFind, String replaceStr)
        {
            // обьектные строки для Word
            object strToFindObj = strToFind;
            object replaceStrObj = replaceStr;
            // диапазон документа Word
            Word.Range wordRange;
            //тип поиска и замены
            object replaceTypeObj;
            replaceTypeObj = Word.WdReplace.wdReplaceAll;
            // обходим все разделы документа
            for (int i = 1; i <= _document.Sections.Count; i++)
            {
                // берем всю секцию диапазоном
                wordRange = _document.Sections[i].Range;
                /*
                Обходим редкий глюк в Find, ПРИЗНАННЫЙ MICROSOFT, метод Execute 
                на некоторых машинах вылетает с ошибкой 
                "Заглушке переданы неправильные данные / Stub received bad data" 
                Подробности: http://support.microsoft.com/default.aspx?scid=kb;en-us;313104
                // выполняем метод поиска и  замены обьекта диапазона ворд
                wordRange.Find.Execute(ref strToFindObj, ref wordMissing, 
                ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, 
                ref wordMissing, ref wordMissing, ref wordMissing, ref replaceStrObj, 
                ref replaceTypeObj, ref wordMissing, ref wordMissing, ref wordMissing, 
                ref wordMissing);
                */

                Word.Find wordFindObj = wordRange.Find;
                object[] wordFindParameters = new object[15] { strToFindObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, _missingObj, replaceStrObj, replaceTypeObj, _missingObj, _missingObj, _missingObj, _missingObj };

                wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj, wordFindParameters);
            }

        }

        public Word.Bookmark FindByBookMark(BookmarksName bookmarkName)
        {
            // обьектные строки для Word            
            object bookmarkNameObj = bookmarkName.ToString();
            
            Word.Bookmark bookmark;

            if (_document.Bookmarks.Exists(bookmarkNameObj.ToString()))
            {
                bookmark = _document.Bookmarks.get_Item(ref bookmarkNameObj);
                return bookmark;
            }
            throw new ArgumentException("Закладка не найдена");
        }

        public void Dispose()
        {
            _document.Close(ref _falseObj, ref _missingObj, ref _missingObj);
            _application.Quit(ref _missingObj, ref _missingObj, ref _missingObj);
            _document = null;
            _application = null;
        }

        public Word.ListTemplate CreateListTemplate()
        {
            // выбор маркера для списка
            Word._Application app = _application;
            Word.ListGallery gallery = app.ListGalleries[Word.WdListGalleryType.wdBulletGallery];
            Word.ListTemplate myPreferredListTemplate = gallery.ListTemplates[1];// номер семейства маркеров?(тире или что-то другое)
            Word.ListLevel listLevel = myPreferredListTemplate.ListLevels[1]; //Номер семейства(чего?) только 1 работает.
            listLevel.NumberFormat = "-";// символ для маркера
            listLevel.TrailingCharacter = Word.WdTrailingCharacter.wdTrailingSpace;  // После маркера пробел.(табуляция или слитно)
            listLevel.NumberStyle = Word.WdListNumberStyle.wdListNumberStyleBullet;  // wdListNumberStyleBullet для маркированного списка        
            listLevel.Alignment = Word.WdListLevelAlignment.wdListLevelAlignRight;   // выравнивание?.
            listLevel.NumberPosition = 0; 
            listLevel.TextPosition = 0;
            listLevel.StartAt = 1;            

            return myPreferredListTemplate;
        }

        public Word.ParagraphFormat CreateParagraphTemplate(Word.Range range)
        {
            Word.ParagraphFormat paragraphFormat = range.ParagraphFormat;

            paragraphFormat.LeftIndent = _application.CentimetersToPoints(0);
            // отсуп первой строки
            paragraphFormat.FirstLineIndent =_application.CentimetersToPoints(1.27f);
            return paragraphFormat;
        }



    }
}
