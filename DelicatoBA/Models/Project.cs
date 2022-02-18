using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DelicatoBA.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Display(Name = "Tên dự án", Description = "Tên dự án dài tối đa 150 ký tự"),
         Required(ErrorMessage = "Hãy nhập tên dự án"), StringLength(150, ErrorMessage = "Tối đa 150 ký tự"),
         UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Thông tin dự án"), UIHint("EditorBox"),Required(ErrorMessage = "Thông tin dự án không được bỏ trống"),
         StringLength(600, ErrorMessage = "Tối đa 600 ký tự"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Mô tả chi tiết"), UIHint("EditorBox")]
        public string Body { get; set; }
        [Display(Name = "Danh sách ảnh"), UIHint("UploadMultiFile")]
        public string ListImage { get; set; }
        [Display(Name = "Ngày đăng")]
        public DateTime CreateDate { get; set; }
        public int View { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        [Display(Name = "Danh mục dự án"), Required(ErrorMessage = "Hãy chọn danh mục dự án")]
        public int ProjectCategoryId { get; set; }
        [Display(Name = "Hiện nổi bật")]
        public bool Hot { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        [Display(Name = "Hiện trang chủ")]
        public bool Home { get; set; }
        [StringLength(300)]
        public string Url { get; set; }
        [Display(Name = "Thẻ tiêu đề"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string TitleMeta { get; set; }
        [Display(Name = "Thẻ mô tả"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string DescriptionMeta { get; set; }
        [Display(Name = "Loại dự án")]
        public virtual ProjectCategory ProjectCategory { get; set; }

        public Project()
        {
            CreateDate = DateTime.Now;
            Active = true;
            View = 1;
        }
    }
}