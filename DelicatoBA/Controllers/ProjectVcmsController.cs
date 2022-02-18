using DelicatoBA.DAL;
using DelicatoBA.Models;
using DelicatoBA.ViewModel;
using Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DelicatoBA.Controllers
{
    [Authorize]
    public class ProjectVcmsController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private IEnumerable<ProjectCategory> ProjectCategories => _unitOfWork.ProCategoryRepository.Get(a => a.CategoryActive, q => q.OrderBy(a => a.CategorySort));
        private SelectList ParentProjectCategoryList => new SelectList(ProjectCategories.Where(a => a.ParentId == null), "Id", "CategoryName");

        #region ProjectCategory
        [ChildActionOnly]
        public ActionResult ListCategory()
        {
            var allcats = _unitOfWork.ProCategoryRepository.Get(orderBy: q => q.OrderBy(a => a.CategorySort));
            return PartialView(allcats);
        }
        public ActionResult ProjectCategory(string result = "")
        {
            ViewBag.NewsCat = result;
            ViewBag.RootCats =
                new SelectList(ProjectCategories.Where(a => a.ParentId == null), "Id", "CategoryName");
            return View(new ProjectCategory());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProjectCategory(ProjectCategory category)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                        isPost = false;
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/projectCategory/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            category.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = Image.FromStream(file.InputStream);
                            //var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            //HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                            file.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }
                if (isPost)
                {
                    category.Url = HtmlHelpers.ConvertToUnSign(null, category.Url ?? category.CategoryName);
                    _unitOfWork.ProCategoryRepository.Insert(category);
                    _unitOfWork.Save();
                    return RedirectToAction("ProjectCategory", new { result = "success" });
                }
            }
            ViewBag.RootCats = new SelectList(ProjectCategories.Where(a => a.ParentId == null), "Id", "CategoryName");
            return View(category);
        }
        public ActionResult UpdateCategory(int catId = 0)
        {
            var category = _unitOfWork.ProCategoryRepository.GetById(catId);
            if (category == null)
            {
                return RedirectToAction("ProjectCategory");
            }
            ViewBag.RootCats = new SelectList(ProjectCategories.Where(a => a.ParentId == null), "Id", "CategoryName");
            return View(category);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateCategory(ProjectCategory category, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                        isPost = false;
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/projectCategory/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            if (System.IO.File.Exists(Server.MapPath("/images/projectCategory/" + category.Image)))
                            {
                                System.IO.File.Delete(Server.MapPath("/images/projectCategory/" + category.Image));
                            }
                            category.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = Image.FromStream(file.InputStream);
                            //var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            //HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                            file.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }
                else
                {
                    category.Image = fc["CurrentFile"];
                }
                if (isPost)
                {
                    category.Url = HtmlHelpers.ConvertToUnSign(null, category.Url ?? category.CategoryName);
                    _unitOfWork.ProCategoryRepository.Update(category);
                    _unitOfWork.Save();
                    return RedirectToAction("ProjectCategory", new { result = "update" });
                }
            }
            ViewBag.RootCats = new SelectList(ProjectCategories.Where(a => a.ParentId == null), "Id", "CategoryName");
            return View(category);
        }
        [HttpPost]
        public bool DeleteCategory(int catId = 0)
        {
            var category = _unitOfWork.ProCategoryRepository.GetById(catId);
            if (category == null)
            {
                return false;
            }
            _unitOfWork.ProCategoryRepository.Delete(category);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateProjectCat(int sort = 1, bool active = false, bool special = false, bool home = false, bool footer = false, int projectCatId = 0)
        {
            var projectCat = _unitOfWork.ProCategoryRepository.GetById(projectCatId);
            if (projectCat == null)
            {
                return false;
            }
            projectCat.CategorySort = sort;
            projectCat.CategoryActive = active;
            projectCat.Special = special;
            projectCat.ShowHome = home;
            projectCat.ShowHome = home;
            projectCat.ShowFooter = footer;

            _unitOfWork.Save();
            return true;
        }
        #endregion

        #region Project 
        public ActionResult ListProject(int? page, string name, int? parentId, int catId = 0, string sort = "date-desc", string result = "")
        {
            ViewBag.Result = result;
            var pageNumber = page ?? 1;
            const int pageSize = 15;
            var projects = _unitOfWork.ProjectRepository.GetQuery().AsNoTracking();
            if (catId > 0)
            {
                projects = projects.Where(l => l.ProjectCategoryId == catId);
            }
            else if (parentId != null)
            {
                projects = projects.Where(a => a.ProjectCategoryId == parentId || a.ProjectCategory.ParentId == parentId);
            }
            if (!string.IsNullOrEmpty(name))
            {
                projects = projects.Where(l => l.Name.ToLower().Contains(name.ToLower()));
            }

            switch (sort)
            {
                case "sort-asc":
                    projects = projects.OrderBy(a => a.Sort);
                    break;
                case "sort-desc":
                    projects = projects.OrderByDescending(a => a.Sort);
                    break;
                case "date-asc":
                    projects = projects.OrderBy(a => a.CreateDate);
                    break;
                default:
                    projects = projects.OrderByDescending(a => a.CreateDate);
                    break;
            }
            var model = new ListProjectViewModel
            {
                SelectCategories = new SelectList(ProjectCategories.Where(a => a.ParentId == null), "Id", "CategoryName"),
                Projects = projects.ToPagedList(pageNumber, pageSize),
                CatId = catId,
                ParentId = parentId,
                Name = name,
                Sort = sort
            };
            if (parentId.HasValue)
            {
                model.ChildCategoryList = new SelectList(ProjectCategories.Where(a => a.ParentId == parentId), "Id", "Categoryname");
            }
            return View(model);
        }
        public ActionResult Project(int? catId, int parentId = 0)
        {
            var model = new InsertProjectViewModel
            {
                ProjectCategoryList = ParentProjectCategoryList,
                ChildCategoryList = new SelectList(new List<ProjectCategory>(), "Id", "CategoryName"),
                Project = new Project { Sort = 1 },
                ParentId = parentId,
                CategoryId = catId,
            };
            if (parentId > 0)
            {
                model.ChildCategoryList = new SelectList(ProjectCategories.Where(a => a.ParentId == parentId), "Id", "CategoryName");
            }
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Project(InsertProjectViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                model.Project.ListImage = fc["Pictures"];
                model.Project.ProjectCategoryId = model.CategoryId ?? model.ParentId;
                model.Project.Url = HtmlHelpers.ConvertToUnSign(null, model.Project.Url ?? model.Project.Name);
                _unitOfWork.ProjectRepository.Insert(model.Project);
                _unitOfWork.Save();
                return RedirectToAction("ListProject", new { result = "success" });
            }
            model.ProjectCategoryList = ParentProjectCategoryList;
            model.ChildCategoryList = new SelectList(new List<ProjectCategory>(), "Id", "CategoryName");

            if (model.ParentId > 0)
            {
                model.ChildCategoryList = new SelectList(ProjectCategories.Where(a => a.ParentId == model.ParentId), "Id", "CategoryName");
            }
            return View(model);
        }
        public ActionResult UpdateProject(int proId = 0)
        {
            var project = _unitOfWork.ProjectRepository.GetById(proId);
            if (project == null)
            {
                return RedirectToAction("ListProject");
            }
            var model = new InsertProjectViewModel
            {
                Project = project,
                Categories = ProjectCategories,
                ParentId = project.ProjectCategory.ParentId ?? project.ProjectCategoryId,
                ProjectCategoryList = ParentProjectCategoryList,
                CategoryId = project.ProjectCategoryId,
            };
            if (model.ParentId > 0)
            {
                model.ChildCategoryList = new SelectList(ProjectCategories.Where(a => a.ParentId == model.ParentId), "Id", "CategoryName");
            }
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateProject(InsertProjectViewModel model, FormCollection fc)
        {
            var project = _unitOfWork.ProjectRepository.GetById(model.Project.Id);
            if (project == null)
            {
                return RedirectToAction("ListProject");
            }
            if (ModelState.IsValid)
            {
                project.ListImage = fc["Pictures"] == "" ? null : fc["Pictures"];
                project.Url = HtmlHelpers.ConvertToUnSign(null, model.Project.Url ?? model.Project.Name);
                project.ProjectCategoryId = model.CategoryId ?? model.ParentId;
                project.Name = model.Project.Name;
                project.Description = model.Project.Description;
                project.Body = model.Project.Body;
                project.Active = model.Project.Active;
                project.Home = model.Project.Home;
                project.Hot = model.Project.Hot;
                //project.Quantity = model.Project.Quantity;
                project.TitleMeta = model.Project.TitleMeta;
                project.DescriptionMeta = model.Project.DescriptionMeta;
                project.Sort = model.Project.Sort;

                _unitOfWork.Save();
                return RedirectToAction("ListProject", new { result = "update" });
            }
            model.ProjectCategoryList = ParentProjectCategoryList;
            if (model.ParentId > 0)
            {
                model.ChildCategoryList = new SelectList(ProjectCategories.Where(a => a.ParentId == model.ParentId), "Id", "CategoryName");
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteProject(int proId = 0)
        {
            var project = _unitOfWork.ProjectRepository.GetById(proId);
            if (project == null)
            {
                return false;
            }
            _unitOfWork.ProjectRepository.Delete(project);
            _unitOfWork.Save();
            return true;
        }
        [HttpPost]
        public bool CloneProject(int proId = 0)
        {
            var project = _unitOfWork.ProjectRepository.GetById(proId);
            if (project == null)
            {
                return false;
            }
            _unitOfWork.ProjectRepository.Insert(project);
            _unitOfWork.Save();
            return true;
        }
        [HttpPost]
        public bool QuickUpdate(bool? status, bool active, bool home, int sort = 0, int proId = 0)
        {
            var project = _unitOfWork.ProjectRepository.GetById(proId);
            if (project == null)
            {
                return false;
            }
            if (status != null)
            {
                project.Active = Convert.ToBoolean(status);
            }
            if (sort >= 0)
            {
                project.Sort = sort;
            }
            //project.Hot = hot;
            project.Active = active;
            project.Home = home;
            _unitOfWork.Save();
            return true;
        }
        #endregion

        public JsonResult GetProjectCategory(int? parentId)
        {
            var categories = ProjectCategories.Where(a => a.ParentId == parentId);
            return Json(categories.Select(a => new { a.Id, Name = a.CategoryName }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChildCategory(int parentId = 0)
        {
            var childCategories = ProjectCategories.Where(a => a.ParentId == parentId);
            return Json(childCategories.Select(a => new { a.Id, a.CategoryName }), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}