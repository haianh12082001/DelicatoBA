using DelicatoBA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DelicatoBA.ViewModel
{
    public class ListProjectViewModel
    {
        public PagedList.IPagedList<Project> Projects { get; set; }
        public SelectList SelectCategories { get; set; }
        public SelectList ChildCategoryList { get; set; }
        public int? ParentId { get; set; }
        public int? CatId { get; set; }
        public string Name { get; set; }
        public string Sort { get; set; }

        public ListProjectViewModel()
        {
            ChildCategoryList = new SelectList(new List<ProjectCategory>(), "Id", "CategoryName");
        }
    }
    public class InsertProjectViewModel
    {
        public Project Project { get; set; }
        [Display(Name = "Danh mục dự án con"), Required(ErrorMessage = "Hãy chọn danh mục dự án")]
        public int ParentId { get; set; }
        [Display(Name = "Danh mục dự án cha")]
        public int? CategoryId { get; set; }
        public IEnumerable<ProjectCategory> Categories { get; set; }
        public SelectList SelectCategories { get; set; }
        public SelectList ChildCategoryList { get; set; }
        public SelectList ProjectCategoryList { get; set; }
        public InsertProjectViewModel()
        {
            ChildCategoryList = new SelectList(new List<ProjectCategory>(), "Id", "CategoryName");
        }
    }
}