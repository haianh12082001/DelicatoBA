using DelicatoBA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelicatoBA.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly DataEntities _context = new DataEntities();
        private GenericRepository<Admin> _adminRepository;
        private GenericRepository<Team> _teamRepository;
        private GenericRepository<ArticleCategory> _artcategoryRepository;
        private GenericRepository<Article> _articleRepository;
        private GenericRepository<Banner> _bannerRepository;
        //private GenericRepository<Feedback> _feedbackRepository; 
        private GenericRepository<Contact> _contactRepository;
        private GenericRepository<ConfigSite> _configRepository;
        private GenericRepository<Project> _projectRepository;
        private GenericRepository<ProjectCategory> _projectCategoryRepository;
        private GenericRepository<Subscribe> _subscribeRepository;

        public GenericRepository<Subscribe> SubscribeRepository =>
           _subscribeRepository ?? (_subscribeRepository = new GenericRepository<Subscribe>(_context));
        public GenericRepository<Project> ProjectRepository =>
            _projectRepository ?? (_projectRepository = new GenericRepository<Project>(_context));

        public GenericRepository<ProjectCategory> ProCategoryRepository =>
            _projectCategoryRepository ?? (_projectCategoryRepository = new GenericRepository<ProjectCategory>(_context));
        public GenericRepository<ConfigSite> ConfigSiteRepository =>
            _configRepository ?? (_configRepository = new GenericRepository<ConfigSite>(_context));
        public GenericRepository<Contact> ContactRepository =>
            _contactRepository ?? (_contactRepository = new GenericRepository<Contact>(_context));
        public GenericRepository<Banner> BannerRepository =>
            _bannerRepository ?? (_bannerRepository = new GenericRepository<Banner>(_context));
        public GenericRepository<Article> ArticleRepository =>
            _articleRepository ?? (_articleRepository = new GenericRepository<Article>(_context));
        public GenericRepository<ArticleCategory> ArticleCategoryRepository =>
            _artcategoryRepository ?? (_artcategoryRepository = new GenericRepository<ArticleCategory>(_context));
        public GenericRepository<Admin> AdminRepository =>
            _adminRepository ?? (_adminRepository = new GenericRepository<Admin>(_context));
        public GenericRepository<Team> TeamRepository =>
            _teamRepository ?? (_teamRepository = new GenericRepository<Team>(_context));
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}