using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace ReportGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String cleanThursdayPath = Environment.CurrentDirectory + @"\Template\отчет #Чистый четверг#.dot";
        WorkWithWord word = null;

        public MainWindow()
        {
            InitializeComponent();
            this.tb_photoCount.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
        }

        //фильтр ввода только цифр
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char ch = e.Text[0];
            if (!Char.IsDigit(ch) && ch != 8) e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
            ref oMissing, ref oMissing);

            //Insert a paragraph at the beginning of the document.
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "Heading 1";
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();

            //Insert a paragraph at the end of the document.
            Word.Paragraph oPara2;
            object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara2.Range.Text = "Heading 2";
            oPara2.Format.SpaceAfter = 6;
            oPara2.Range.InsertParagraphAfter();

            //Insert another paragraph.
            Word.Paragraph oPara3;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara3.Range.Text = "This is a sentence of normal text. Now here is a table:";
            oPara3.Range.Font.Bold = 0;
            oPara3.Format.SpaceAfter = 24;
            oPara3.Range.InsertParagraphAfter();

            //Insert a 3 x 5 table, fill it with data, and make the first row
            //bold and italic.
            Word.Table oTable;
            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, 3, 5, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            int r, c;
            string strText;
            for (r = 1; r <= 3; r++)
                for (c = 1; c <= 5; c++)
                {
                    strText = "r" + r + "c" + c;
                    oTable.Cell(r, c).Range.Text = strText;
                }
            oTable.Rows[1].Range.Font.Bold = 1;
            oTable.Rows[1].Range.Font.Italic = 1;

            //Add some text after the table.
            Word.Paragraph oPara4;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara4 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara4.Range.InsertParagraphBefore();
            oPara4.Range.Text = "And here's another table:";
            oPara4.Format.SpaceAfter = 24;
            oPara4.Range.InsertParagraphAfter();

            //Insert a 5 x 2 table, fill it with data, and change the column widths.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, 5, 2, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            for (r = 1; r <= 5; r++)
                for (c = 1; c <= 2; c++)
                {
                    strText = "r" + r + "c" + c;
                    oTable.Cell(r, c).Range.Text = strText;
                }
            oTable.Columns[1].Width = oWord.InchesToPoints(2); //Change width of columns 1 & 2
            oTable.Columns[2].Width = oWord.InchesToPoints(3);

            //Keep inserting text. When you get to 7 inches from top of the
            //document, insert a hard page break.
            object oPos;
            double dPos = oWord.InchesToPoints(7);
            oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range.InsertParagraphAfter();
            do
            {
                wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                wrdRng.ParagraphFormat.SpaceAfter = 6;
                wrdRng.InsertAfter("A line of text");
                wrdRng.InsertParagraphAfter();
                oPos = wrdRng.get_Information
                                       (Word.WdInformation.wdVerticalPositionRelativeToPage);
            }
            while (dPos >= Convert.ToDouble(oPos));
            object oCollapseEnd = Word.WdCollapseDirection.wdCollapseEnd;
            object oPageBreak = Word.WdBreakType.wdPageBreak;
            wrdRng.Collapse(ref oCollapseEnd);
            wrdRng.InsertBreak(ref oPageBreak);
            wrdRng.Collapse(ref oCollapseEnd);
            wrdRng.InsertAfter("We're now on page 2. Here's my chart:");
            wrdRng.InsertParagraphAfter();

            //Insert a chart.
            Word.InlineShape oShape;
            object oClassType = "MSGraph.Chart.8";
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing,
            ref oMissing, ref oMissing, ref oMissing,
            ref oMissing, ref oMissing, ref oMissing);

            //Demonstrate use of late bound oChart and oChartApp objects to
            //manipulate the chart object with MSGraph.
            object oChart;
            object oChartApp;
            oChart = oShape.OLEFormat.Object;
            oChartApp = oChart.GetType().InvokeMember("Application",
            BindingFlags.GetProperty, null, oChart, null);

            //Change the chart type to Line.
            object[] Parameters = new Object[1];
            Parameters[0] = 4; //xlLine = 4
            oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty,
            null, oChart, Parameters);

            //Update the chart image and quit MSGraph.
            oChartApp.GetType().InvokeMember("Update",
            BindingFlags.InvokeMethod, null, oChartApp, null);
            oChartApp.GetType().InvokeMember("Quit",
            BindingFlags.InvokeMethod, null, oChartApp, null);
            //... If desired, you can proceed from here using the Microsoft Graph 
            //Object model on the oChart and oChartApp objects to make additional
            //changes to the chart.

            //Set the width of the chart.
            oShape.Width = oWord.InchesToPoints(6.25f);
            oShape.Height = oWord.InchesToPoints(3.57f);

            //Add text after the chart.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wrdRng.InsertParagraphAfter();
            wrdRng.InsertAfter("THE END.");

            //Close this form.
            this.Close();
        }

        private void FormDocument_click(object sender, RoutedEventArgs e)
        {
            word = new WorkWithWord();
            word.CreateNewDoc(cleanThursdayPath);

            FillDate();
            FillPhotoCount();
            FillWork();
            FillSuperVisor(cmb_supervisor.SelectedIndex);

        }

        private void FillPhotoCount()
        {
            throw new NotImplementedException();
        }

        private void FillSuperVisor(int index)
        {            
            Word.Bookmark bookmark = word.FindByBookMark(BookmarksName.PositionBookMark());
            
            bookmark.Range.Text = GetSupervisorByIndex(index);
        }
       

        private void FillDate()
        {
            DateTime date = DateTime.Now;
            String curDate = date.ToString("dd.MM.yyyy");
            // word.FindAndReplace("#Date#", curDate);
            Word.Bookmark bookmark = word.FindByBookMark(BookmarksName.DateBookMark());
            bookmark.Range.Text = curDate;
        }
        private void FillWork()
        {
            String works = CreateListWorks();
            FormateList(works);
        }

        private String GetSupervisorByIndex(int index)
        {
            String position = String.Empty;
            switch (index)
            {
                case 0:
                    position = "Начальник управления" + new String(' ', 70) + "М.В. Городкова";
                    break;
                case 1:
                    position = "И.о. начальника управления" + new String(' ', 70) + "Т.Н. Хомич";
                    break;
                default:
                    break;
            }
            return position;
        }
        private string CreateListWorks()
        {
            String work = String.Empty;
            //Зима
            String cleanSnow = "уборка снега на тротуарах;";
            String sprinklingSidewalk = "посыпка тротуара противогололедными материалами;";

            //Весна-осень:
            String landscaping = "облагораживание территории;";
            string harvestingFoliage = "уборка листвы;";
            String sweepingPavements = "подметание тротуаров;";

            //Лето:
            String cutGrass = "покос травы;";

            //Всегда
            String lightGarbageCollection = "уборка легкого мусора.";


            //Зима            
            if (cb_cleanSnow.IsChecked == true)
            {
                work += cleanSnow + Environment.NewLine;
            }
            if (cb_sprinklingSidewalk.IsChecked == true)
            {
                work += sprinklingSidewalk + Environment.NewLine;
            }
            //Весна-осень:
            if (cb_landscaping.IsChecked == true)
            {
                work += landscaping + Environment.NewLine;
            }
            if (cb_harvestingFoliage.IsChecked == true)
            {
                work += harvestingFoliage + Environment.NewLine;
            }
            if (cb_sweepingPavements.IsChecked == true)
            {
                work += sweepingPavements + Environment.NewLine;
            }
            //Лето:
            if (cb_cutGrass.IsChecked == true)
            {
                work += cutGrass + Environment.NewLine;
            }

            //Всегда в конце, потому что тут после предложения поставил точку.
            if (cb_lightGarbageCollection.IsChecked == true)
            {
                work += lightGarbageCollection;// + Environment.NewLine;
            }

            return work;
        }

        private void FormateList(String works)
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                //Делаем при нажатии на колесико мыши
                cb_lightGarbageCollection.IsEnabled = !cb_lightGarbageCollection.IsEnabled;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
        
    }
}
