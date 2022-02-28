using DelicatoBA.DAL;
using DelicatoBA.Models;
using DelicatoBA.ViewModel;
using Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DelicatoBA.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        // GET: OurTeam
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        #region Team
        public ActionResult ListTeam(int? page, string name, string result = "")
        {
            ViewBag.Result = result;
            var pageNumber = page ?? 1;
            const int pageSize = 10;
            var team = _unitOfWork.TeamRepository.Get(orderBy: l => l.OrderByDescending(a => a.Id));
            if (!string.IsNullOrEmpty(name))
            {
                team = team.Where(l => l.Name.ToLower().Contains(name.ToLower()));
            }
            var model = new ListTeamViewModel
            {
                Teams = team.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        public ActionResult Team()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Team(Team model)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                        }
                        else
                        {
                            var imgPath = "/images/team/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            model.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = Image.FromStream(file.InputStream);
                            var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                        }
                    }
                }
                _unitOfWork.TeamRepository.Insert(model);
                _unitOfWork.Save();
                return RedirectToAction("ListTeam", new { result = "success" });
            }
            return View(model);
        }
        public ActionResult UpdateTeam(int teamId = 0)
        {
            var team = _unitOfWork.TeamRepository.GetById(teamId);
            if (team == null)
            {
                return RedirectToAction("ListTeam");
            }
            return View(team);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateTeam(Team model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                        }
                        else
                        {
                            var imgPath = "/images/team/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            if (System.IO.File.Exists(Server.MapPath("/images/team/" + model.Image)))
                            {
                                System.IO.File.Delete(Server.MapPath("/images/team/" + model.Image));
                            }
                            model.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = Image.FromStream(file.InputStream);
                            var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                        }
                    }
                }
                else
                {
                    model.Image = fc["CurrentFile"];
                }
                _unitOfWork.TeamRepository.Update(model);
                _unitOfWork.Save();

                return RedirectToAction("ListTeam", new { result = "update" });
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteTeam(int teamId = 0)
        {
            var team = _unitOfWork.TeamRepository.GetById(teamId);
            if (team == null)
            {
                return false;
            }
            _unitOfWork.TeamRepository.Delete(team);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateTeamCat(int sort = 1, bool active = false,int teamId = 0)
        {
            var teamCat = _unitOfWork.TeamRepository.GetById(teamId);
            if (teamCat == null)
            {
                return false;
            }

            teamCat.Active = active;
         
            _unitOfWork.Save();
            return true;
        }
        #endregion
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}