using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_Samples.ViewModels
{
    public class PaginationViewModel
    {
        public const int MAXNROFPAGES = 118;
        public int CurrentPage { get; set; }
        public int NextPage1 { get; set; }
        public int NextPage2 { get; set; }
        public int Next { get; set; }
        public int Previous { get; set; }
        public bool HasNext { get; set; }
        public int Last { get; set; }
        public PaginationViewModel(int currentPage)
        {
            this.CurrentPage = currentPage;
            this.NextPage1 = currentPage + 1;
            this.NextPage2 = currentPage + 2;
            this.Next = currentPage + 3;
            this.Previous = currentPage - 1;
            this.HasNext = true;
            this.Last = MAXNROFPAGES;
            if (currentPage == MAXNROFPAGES)
            {
                this.HasNext = false;
            }
        }
    }
}