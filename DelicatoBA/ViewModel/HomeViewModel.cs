using DelicatoBA.Models;
using PagedList;
using System.Collections.Generic;

namespace DelicatoBA.ViewModel
{
    public class HomeViewModel
    {
    
        public IEnumerable<Banner> Banners { get; set; }
        public IEnumerable<ItemHomeProject> ItemHomeProjects { get; set; }
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Article> ArticleBox { get; set; }
        public IEnumerable<Project> Projects { get; set; }
     

        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Project> ProjectsHot { get; internal set; }

        public class ItemHomeProject
        {
            public ProjectCategory ProjectCategory { get; set; }
            public IEnumerable<Project> Projects { get; set; }
        }
    }
    public class HeaderViewModel
    {
        //public IEnumerable<Article> Introduces { get; set; }
        public IEnumerable<ArticleCategory> Intro { get; set; }
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
        public IEnumerable<ProjectCategory> ProjectCategories { get; set; }
        public IEnumerable<Article> Articles { get; set; }

    }
    public class FooterViewModel
    {
        public IEnumerable<ArticleCategory> Intro { get; set; }
        public IEnumerable<ArticleCategory> Service { get; set; }
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
        public IEnumerable<ProjectCategory> ProjectCategories { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }

    public class AllProjectViewModel
    {
        public IPagedList<Project> Projects { get; set; }
        public IEnumerable<ProjectCategory> Categories { get; set; }
    }
    public class CategoryProjectViewModel
    {
        public ProjectCategory Category { get; set; }
        public IPagedList<Project> Projects { get; set; }
        public IEnumerable<ProjectCategory> Categories { get; set; }
        public int CatId { get; set; }
        public string Sort { get; set; }
    }
    public class ProjectDetailViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Article> Article { get; set; }
    }
    public class CategoryArticleViewModel
    {
        public ArticleCategory Category { get; set; }
        public IPagedList<Article> Articles { get; set; }
        public IEnumerable<ArticleCategory> Categories { get; set; }
        public int CatId { get; set; }
    }
    public class AllArticleViewModel
    {
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
        public IPagedList<Article> Articles { get; set; }
    }
    public class MenuArticleViewModel
    {
        public int RootId { get; set; }
        public int CatId { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
    }

    
    public class ArticleDetailViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Article> ArticlesList { get; set; }
    }
    public class ProjectSearchViewModel
    {
        public string Keywords { get; set; }
        public int? CategoryId { get; set; }
        public IPagedList<Project> Projects { get; set; }
        public ProjectCategory ProjectCategory { get; set; }
        public IEnumerable<ProjectCategory> ProjectCategories { get; set; }
    }

    public class TeamDeitalViewModel
    {
        public Team Team { get; set; }
        public IEnumerable<Team> TeamList { get; set; }
    }
    
    public class ArticleSearchViewModel
    {
        public string Keywords { get; set; }
        public IPagedList<Article> Articles { get; set; }
    }
}