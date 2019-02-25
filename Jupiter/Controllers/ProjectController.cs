using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Jupiter.ActionFilter;
using Jupiter.Models;
using JupiterEntity;

namespace Jupiter.Controllers
{
    public class ProjectController : BaseController
    {
        // GET: api/Project
        public IHttpActionResult Get()
        {
            var result = new Result<List<ProjectModel>>();
            List<ProjectModel> projectModels = new List<ProjectModel>();
            using (var db = new jupiterEntities())
            {
                List < Project > projects= db.Projects.ToList();
                Mapper.Map(projects, projectModels);
                result.Data = projectModels;
                return Json(result);
            }
        }

        // GET: api/Project/5
        public IHttpActionResult GetProject(int id)
        {
            var result = new Result<ProjectModel>();
            ProjectModel projectModel  = new ProjectModel();
            using (var db = new jupiterEntities())
            {
                Project project = db.Projects.Find(id);
                if (project == null)
                {
                    return Json(DataNotFound(result));
                }

                Mapper.Map(project, projectModel);
                result.Data = projectModel;
                return Json(result);
            }
        }

        // PUT: api/Project/5
        [CheckModelFilter]
        public IHttpActionResult PutProject(int id, ProjectModel projectModel)
        {
            var result = new Result<string>();
            using (var db = new jupiterEntities())
            {
                var a = db.Projects.Where(x => x.ProjectId == id).Select(x => x).FirstOrDefault();
                if (a == null)
                {
                    return Json(DataNotFound(result));
                }

                Type type = typeof(Project);
                UpdateTable(projectModel,type,a);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.ErrorMessage = e.Message;
                    result.IsSuccess = false;
                    return Json(result);
                }
            }
            return Json(result);
        }

        // POST: api/Project
        [CheckModelFilter]
        public IHttpActionResult PostProject(ProjectModel projectModel)
        {
            var result = new Result<string>();
            using (var db = new jupiterEntities())
            {
                Project project = new Project();
                Mapper.Map(projectModel, project);
                db.Projects.Add(project);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.ErrorMessage = e.Message;
                    result.IsSuccess = false;
                    return Json(result);
                }
                return Json(result);
            }
        }

        // DELETE: api/Project/5
        public IHttpActionResult DeleteProject(int id)
        {
            var result = new Result<string>();
            using (var db = new jupiterEntities())
            {
                Project project = db.Projects.Find(id);
                if (project == null)
                {
                    return Json(DataNotFound(result));
                }

                try
                {
                    db.Projects.Remove(project);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    result.ErrorMessage = e.Message;
                    result.IsSuccess = false;
                    return Json(result);
                }
            }
            return Json(result);
        }
    }
}