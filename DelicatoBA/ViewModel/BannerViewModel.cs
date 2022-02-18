using DelicatoBA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DelicatoBA.ViewModel
{
    public class BannerViewModel
    {
        public Banner Banner { get; set; }
        public SelectList SelectGroup { get; set; }
        public BannerViewModel()
        {
            var listgroup = new Dictionary<int, string>
            {
                { 1, "Banner Slide" },
                { 2, "Banner giới thiệu bên trái" },
                { 3, "Banner giới thiệu bên phải" },
                { 4, "Banner chỉ số phần trăm" },
                //{ 5, "Banner tại sao chọn muối ngâm chân (phải)" },
                //{ 6, "Banner dưới sản phẩm nổi bật" }
            };
            SelectGroup = new SelectList(listgroup, "Key", "Value");
        }
    }
    public class ListBannerViewModel
    {
        public IEnumerable<Banner> Banners { get; set; }
        public int? GroupId { get; set; }
        public SelectList SelectGroup { get; set; }
        public ListBannerViewModel()
        {
            var listgroup = new Dictionary<int, string>
            {
                { 1, "Banner Slide" },
                { 2, "Banner giới thiệu bên trái" },
                { 3, "Banner giới thiệu bên phải" },
                { 4, "Banner chỉ số phần trăm" },
                //{ 5, "Banner tại sao chọn muối ngâm chân (phải)" },
                //{ 6, "Banner dưới sản phẩm nổi bật" }
            };
            SelectGroup = new SelectList(listgroup, "Key", "Value");
        }
    }
    //public class ListFeedbackViewModel
    //{
    //    public IEnumerable<Feedback> Feedbacks { get; set; }
    //    public string Name { get; set; }
    //}
}