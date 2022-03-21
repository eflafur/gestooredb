using System;
using System.Collections.Generic;

namespace Pagination
{
    public class ComposePage
    {
        public ComposePage(int _currentPage, int _totalRow)
        {
            this.currentPage = _currentPage;
            this.totalRow = _totalRow;
        }
        public readonly int ROWBYPAGE = 10;
        public readonly int STEP = 4;
        public int currentPage { get; set; }
        public int totalRow { get; set; }
        public int totalPage
        {
            get
            {
                return totalRow % ROWBYPAGE > 0 ? totalRow / ROWBYPAGE + 1 : totalRow / ROWBYPAGE;
            }
        }
        public int lowRange
        {
            get
            {
                return currentPage - STEP<= 0 ? 1 : totalPage-currentPage<STEP? currentPage -2*STEP +totalPage-currentPage:   currentPage - STEP;
            }
        }
        public int highRange
        {
            get
            {
                return totalPage - currentPage < STEP ? totalPage : currentPage - STEP <= 0 ? 2 * STEP + lowRange:currentPage+STEP;
            }
        }
        public List<Page> Pages = new List<Page>();

        public List<Page> Wrapper()
        {
            this.totalRow = totalRow;
            for (int i = lowRange; i <= highRange; i++)
            {
                var page = new Page();
                page.index = i;
                page.isFirst = i == 1 ? true : false;
                page.isLast = i == totalPage ? true : false;
                page.isSelected = i == currentPage ? true : false;
                Pages.Add(page);
            }
            return Pages;
        }

        //public class Page
        //{
        //    internal bool isFirst { get; set; }
        //    internal bool isLast { get; set; }
        //    internal int index { get; set; }
        //    internal bool isSelected { get; set; }
        //}
    }
    public class Page
    {
        public bool isFirst { get; set; }
        public  bool isLast { get; set; }
        public int index { get; set; }
        public bool isSelected { get; set; }
    }
}