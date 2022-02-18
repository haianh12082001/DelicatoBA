using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DelicatoBA.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Display(Name = "Tên nhân viên", Description = "Tiêu đề dài tối đa 150 ký tự"),
        Required(ErrorMessage = "Hãy nhập tên nhân viên"), StringLength(150, ErrorMessage = "Tối đa 150 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Hình ảnh"), StringLength(500)]
        public string Image { get; set; }
        [Display(Name = "Chức vụ"), UIHint("TextBox")]
        public string Office { get; set; }
        [Display(Name = "Mô tả chi tiết"), UIHint("EditorBox")]
        public string Body { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        public Team()
        {
            Active = true;
        }
    }
}