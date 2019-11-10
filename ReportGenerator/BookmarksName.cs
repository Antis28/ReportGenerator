using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator
{
    class BookmarksName
    {
        const String date = "Date";
        const String photoCount = "PhotoCount";
        const String position = "Position";
        const String work = "Work";

        String bookmark = "Не определено";

        private BookmarksName(String bookmark)
        {
            this.bookmark = bookmark;
        }

        public static BookmarksName DateBookMark() => new BookmarksName(date);
        public static BookmarksName PhotoCountBookMark() => new BookmarksName(photoCount);
        public static BookmarksName PositionBookMark() => new BookmarksName(position);
        public static BookmarksName WorkBookMark() => new BookmarksName(work);


        public override string ToString()
        {
            return bookmark;
        }
    }
}
