using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_Samples.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int NextPage1 { get; set; }
        public int NextPage2 { get; set; }
        public int Next { get; set; }
        public int Previous { get; set; }
        public bool HasNext { get; set; }
        public bool HasNext2 { get; set; }
        public int Last { get; set; }
        public PaginationViewModel(int currentPage, int totalNrOfItems)
        {
            this.CurrentPage = currentPage;
      
            if (totalNrOfItems % 5 == 0)
            {
                this.Last = totalNrOfItems / 5;
                if (currentPage == totalNrOfItems / 5)
                {
                    this.HasNext = false;
                }
            }
            else
            {
                this.Last = totalNrOfItems / 5 + 1;
                if (currentPage == totalNrOfItems / 5 + 1)
                {
                    this.HasNext = false;
                }
            }

            this.Previous = currentPage - 1;
            this.HasNext = false;
            this.HasNext2 = false;

            if (this.Last != this.CurrentPage && this.CurrentPage <this.Last)
            {
                this.NextPage1 = currentPage + 1;
                this.HasNext = true;
                if (this.Last != this.NextPage1)
                {
                    this.NextPage2 = currentPage + 2;
                    this.HasNext2 = true;
                    if (this.Last != this.NextPage2)
                    {
                        this.Next = currentPage + 3;
                    }
                }
                
            }
        }
    }
}