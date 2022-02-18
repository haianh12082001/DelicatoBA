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
    public class ContactController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
     
        public ActionResult ListContact(int? page, string name)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 15;
            var contact = _unitOfWork.ContactRepository.Get(orderBy: l => l.OrderByDescending(a => a.Id));
            if (!string.IsNullOrEmpty(name))
            {
                contact = contact.Where(l => l.Email.ToLower().Contains(name.ToLower()));
            }
            var model = new ListContactViewModel
            {
                Contacts = contact.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        [HttpPost]
        public bool DeleteContact(int contactId = 0)
        {
            var contact = _unitOfWork.ContactRepository.GetById(contactId);
            if (contact == null)
            {
                return false;
            }
            _unitOfWork.ContactRepository.Delete(contact);
            _unitOfWork.Save();
            return true;
        }

        public ActionResult ListSubscribe(int? page, string name)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 15;
            var subscribes = _unitOfWork.SubscribeRepository.Get(orderBy: l => l.OrderByDescending(a => a.Id));
            if (!string.IsNullOrEmpty(name))
            {
                subscribes = subscribes.Where(l => l.Email.ToLower().Contains(name.ToLower()));
            }
            var model = new ListSubscribeViewModel
            {
                Subscribes = subscribes.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        [HttpPost]
        public bool DeleteSubscribe(int subId = 0)
        {
            var contact = _unitOfWork.SubscribeRepository.GetById(subId);
            if (contact == null)
            {
                return false;
            }
            _unitOfWork.SubscribeRepository.Delete(contact);
            _unitOfWork.Save();
            return true;
        }
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}