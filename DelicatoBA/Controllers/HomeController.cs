using Helpers;
using DelicatoBA.DAL;
using DelicatoBA.Models;
using DelicatoBA.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using static DelicatoBA.ViewModel.HomeViewModel;

namespace DelicatoBA.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private static string Email => WebConfigurationManager.AppSettings["email"];
        private static string Password => WebConfigurationManager.AppSettings["password"];
        private IEnumerable<Banner> Banners => _unitOfWork.BannerRepository.Get(a => a.Active, q => q.OrderBy(a => a.Sort));
        //private IEnumerable<NewsCategory> NewsCategoryServices =>
        //    _unitOfWork.NewsCategoryRepository.Get(a => a.CategoryActive, q => q.OrderBy(a => a.CategorySort)
        //    ).Where(p => p.TypePost == TypePost.Service);
        private IEnumerable<ArticleCategory> ArticleCategories => 
            _unitOfWork.ArticleCategoryRepository.Get(a => a.CategoryActive, o => o.OrderBy(a => a.CategorySort));
        private IEnumerable<ProjectCategory> ProjectCategories =>
            _unitOfWork.ProCategoryRepository.Get(a => a.CategoryActive, q => q.OrderBy(a => a.CategorySort));

        public ConfigSite ConfigSite => (ConfigSite)HttpContext.Application["ConfigSite"];

       
        public ActionResult Index()
        {
            var homeCategories = _unitOfWork.ProCategoryRepository
                .Get(a => a.CategoryActive && a.ShowHome && a.ParentId == null, q => q.OrderBy(c => c.CategorySort));

            var itemsHome = homeCategories.Select(a => new HomeViewModel.ItemHomeProject
            {
                ProjectCategory = a,
                Projects = _unitOfWork.ProjectRepository.Get(l => l.Active && l.Home && (l.ProjectCategoryId == a.Id || l.ProjectCategory.ParentId == a.Id), q => q.OrderBy(c => c.Sort))
            });

            var model = new HomeViewModel
            {

                Banners = _unitOfWork.BannerRepository.GetQuery(a => a.Active, o => o.OrderBy(p => p.Sort)),
                Articles = _unitOfWork.ArticleRepository.GetQuery(a => a.Active && a.Home, o => o.OrderByDescending(a => a.CreateDate), 3),
                ArticleBox = _unitOfWork.ArticleRepository.GetQuery(a => a.Active && a.Box, o => o.OrderBy(p => p.Sort),1),
                Teams = _unitOfWork.TeamRepository.GetQuery(a => a.Active, o => o.OrderBy(a => a.Sort)),
                Projects = _unitOfWork.ProjectRepository.Get(l => l.Active && l.Home , q => q.OrderBy(c => c.Sort)),
                ProjectsHot = _unitOfWork.ProjectRepository.Get(l => l.Active && l.Hot, q => q.OrderBy(c => c.Sort)),
                ItemHomeProjects = itemsHome,
            };
            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            var model = new HeaderViewModel()
            {
                
                ArticleCategories = ArticleCategories.Where(l => l.ShowMenu && l.TypePost != TypePost.Introduce),

                Intro = ArticleCategories.Where(p => p.TypePost == TypePost.Introduce),
                ProjectCategories = ProjectCategories.Where(l => l.ShowHome),
                //ArticleCategories = ArticleCategories.Where(l => l.ShowMenu),
                //ArticleCategories = ArticleCategories.Where(p => p.TypePost == TypePost.Article),
                //Introduces = _unitOfWork.ArticleRepository.GetQuery(p => p.Active && p.ArticleCategory.TypePost == TypePost.Introduce, c => c.OrderBy(l => l.Sort)),
                //ProductCategories = ProductCategories.Where(a => a.ShowMenu),
                //ProductPromotion = _unitOfWork.ProductRepository.GetQuery(a => a.Active && a.PriceSale > 0),

            };
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            var model = new FooterViewModel()
            {
                //ArticleCategories = ArticleCategories.Where(a => a.ShowMenu && a.ParentId == null),
                ArticleCategories = ArticleCategories.Where(l => l.ShowFooter && l.TypePost == TypePost.Article),
                Service = ArticleCategories.Where(l => l.TypePost == TypePost.Service),
                Intro = ArticleCategories.Where(p => p.TypePost == TypePost.Introduce),
                ProjectCategories = ProjectCategories.Where(l => l.ShowFooter),

            };
            return PartialView(model);
        }
        [Route("du-an", Order = 3)]
        public ActionResult AllProject(int? page)
        {
            var projects = _unitOfWork.ProjectRepository.GetQuery(l => l.Active, c => c.OrderByDescending(a => a.Id));
            var pageNumber = page ?? 1;
            var model = new AllProjectViewModel
            {
                Projects = projects.ToPagedList(pageNumber, 15),
                Categories = ProjectCategories
            };
            return View(model);
        }
        [Route("du-an/{url}", Order = 2)]
        public ActionResult ProjectCategory(int? page, string sort, string url)
        {
            var category = ProjectCategories.FirstOrDefault(a => a.Url == url);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var projects = _unitOfWork.ProjectRepository.GetQuery(l => l.Active && l.ProjectCategoryId == category.Id || l.ProjectCategory.ParentId == category.Id, c => c.OrderByDescending(a => a.Id));
            //switch (sort)
            //{
            //    case "name":
            //        projects = projects.OrderBy(a => a.Name);
            //        break;
            //    case "sort-desc":
            //        projects = projects.OrderByDescending(a => a.Sort);
            //        break;
            //    default:
            //        projects = projects.OrderByDescending(a => a.Id);
            //        break;
            //}
            var pageNumber = page ?? 1;
            var model = new CategoryProjectViewModel
            {
                Projects = projects.ToPagedList(pageNumber, 15),
                Category = category,
                Sort = sort,
                Categories = ProjectCategories
            };
            return View(model);
        }
        [Route("du-an/{url}.html", Order = 1)]
        public ActionResult ProjectDetail(string url)
        {
            var project = _unitOfWork.ProjectRepository.GetQuery(l => l.Active && l.Url == url).FirstOrDefault();
            if (project == null)
            {
                return RedirectToAction("Index");
            }
            var projects = _unitOfWork.ProjectRepository.GetQuery(l => l.Active && l.ProjectCategoryId == project.ProjectCategoryId && l.Id != project.Id, a => a.OrderByDescending(c => c.Id), 3);
            var model = new ProjectDetailViewModel
            {
                Project = project,
                Projects = projects,
                Article = _unitOfWork.ArticleRepository.Get(a => a.Active && a.ArticleCategory.TypePost == TypePost.Article, q => q.OrderByDescending(a => a.Id), 5)
            };
            return View(model);
        }
        [Route("tin-tuc")]
        public ActionResult AllArticle(int? page)
        {
            var article = _unitOfWork.ArticleRepository.GetQuery(a => a.Active, o => o.OrderByDescending(a => a.CreateDate));
            var pageNumber = page ?? 1;
            var model = new AllArticleViewModel
            {
                Articles = article.ToPagedList(pageNumber, 10),
                ArticleCategories = ArticleCategories,
            };
            return View(model);
        }
        [Route("{url:regex(^(?!.*(du-an|vcms|banner|contact|projectvcms|article|uploader)).*$)}", Order = 2)]
        public ActionResult ArticleCategory(int? page, string url)
        {
            var category = ArticleCategories.FirstOrDefault(a => a.Url == url);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var article = _unitOfWork.ArticleRepository.GetQuery(l => l.Active && (l.ArticleCategoryId == category.Id || l.ArticleCategory.ParentId == category.Id), c => c.OrderByDescending(a => a.CreateDate));
            var pageNumber = page ?? 1;
            if (article.Count() == 1)
            {
                var fi = article.First();
                return RedirectToAction("ArticleDetail", new { url = fi.Url });
            }
            var model = new CategoryArticleViewModel
            {
                Articles = article.ToPagedList(pageNumber, 6),
                Category = category,
                Categories = new List<ArticleCategory>()
            };

            if (category.ParentId == null)
            {
                model.Categories = ArticleCategories.Where(a => a.ParentId == category.Id);
            }
            return View(model);
        }

        [Route("{url}.html", Order = 1)]
        public ActionResult ArticleDetail(string url)
        {
            var article = _unitOfWork.ArticleRepository.GetQuery(a => a.Active && a.Url == url).FirstOrDefault();
            if (article == null)
            {
                return RedirectToAction("Index");
            }
            var articles = _unitOfWork.ArticleRepository.GetQuery(l => l.Active && l.ArticleCategoryId == article.ArticleCategoryId && l.Id != article.Id, c => c.OrderByDescending(a => Guid.NewGuid()), 3);
            var model = new ArticleDetailViewModel
            {
                Article = article,
                ArticlesList = articles,
            };
            return View(model);
        }

        public PartialViewResult TeamDeital(int Id)
        {
            var team = _unitOfWork.TeamRepository.GetById(Id);
            return PartialView(team);
        }
        [ChildActionOnly]
        public PartialViewResult MenuArticle(int rootId = 0, int catId = 0)
        {
            var model = new MenuArticleViewModel
            {
                RootId = rootId,
                CatId = catId,
                //ArticleCategories = ArticleCategories.Where(p => p.ParentId != null && p.TypePost == TypePost.Article),
                ArticleCategories = ArticleCategories,
                Articles = _unitOfWork.ArticleRepository.GetQuery(l => l.Active, a => a.OrderByDescending(c => c.CreateDate), 4)
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult ContactForm()
        {
            return PartialView();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ContactForm(Contact model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, msg = "Hãy điền đúng định dạng." });
            }
            if (string.IsNullOrEmpty(model.Fullname))
            {
                model.Fullname = "Unknown";
            }
            if (string.IsNullOrEmpty(model.Body))
            {
                model.Body = "Tôi muốn nhận tư vấn";
            }

            _unitOfWork.ContactRepository.Insert(model);
            _unitOfWork.Save();

            var subject = "Email liên hệ từ website: " + Request.Url?.Host;
            var body = $"<p>Tên người liên hệ: {model.Fullname},</p>" +
                        $"<p>Email liên hệ: {model.Email},</p>" +
                        $"<p>Nội dung:{model.Body}</p>" +
                        $"<p>Đây là hệ thống gửi email tự động, vui lòng không phản hồi lại email này.</p>";

            Task.Run(() => HtmlHelpers.SendEmail("gmail", subject, body, ConfigSite.Email, Email, Email, Password, "Delicato JSC"));

            return Json(new { status = true, msg = "Gửi liên hệ thành công.\nChúng tôi sẽ liên lạc lại với bạn sớm nhất có thể." });
        }

        [Route("tim-kiem")]
        public ActionResult SearchProject(int? page, string keywords)
        {
            if (string.IsNullOrEmpty(keywords))
            {
                return RedirectToAction("Index");
            }
            var pro = _unitOfWork.ProjectRepository.GetQuery(p => p.Active && p.Name.ToLower().Contains(keywords.ToLower())).OrderBy(c => c.Sort);

            var pageNumber = page ?? 1;
            var model = new ProjectSearchViewModel
            {
                Keywords = keywords,
                Projects = pro.ToPagedList(pageNumber, 15)
            };
            return View(model);
        }

        [Route("tim-kiem-bai-viet")]
        public ActionResult SearchArticle(int? page, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            var article = _unitOfWork.ArticleRepository.GetQuery(l => l.Active && l.Subject.Contains(keyword), a => a.OrderByDescending(c => c.CreateDate));
            var pageNumber = page ?? 1;
            var model = new ArticleSearchViewModel
            {
                Keywords = keyword,
                Articles = article.ToPagedList(pageNumber, 10)
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        [Route("lien-he")]
        public ActionResult Contact()
        {
           

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult QuickContact(FormCollection fc)
        {
            var email = fc["email"];
            Subscribe model = new Subscribe();
            var isEmail = new EmailAddressAttribute().IsValid(email);
            if (!isEmail || string.IsNullOrEmpty(email))
            {
                return Json(new { status = false, msg = "Email không hợp lệ, vui lòng thử lại!" });
            }

            model.Email = email;

            _unitOfWork.SubscribeRepository.Insert(model);
            _unitOfWork.Save();
            return Json(new { status = true, msg = "Đăng ký nhân bạn tin thành công!" });
        }
    }
}
