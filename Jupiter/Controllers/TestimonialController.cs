using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Jupiter.ActionFilter;
using Jupiter.Models;
using JupiterEntity;

namespace Jupiter.Controllers
{
    public class TestimonialController : BaseController
    {
        // GET: api/Testimonial
        public IHttpActionResult Get()
        {
            var result = new Result<List<TestimonialModel>>();
            List<TestimonialModel> testimonialModels = new List<TestimonialModel>();
            using (var db = new jupiterEntities())
            {
                List<Testimonial> testimonials = db.Testimonials.Select(x => x).ToList();
                Mapper.Map(testimonials, testimonialModels);
                result.Data = testimonialModels;
            }
            return Json(result);
        }

        // GET: api/Testimonial/5
        public IHttpActionResult Get(int id)
        {
            var result = new Result<TestimonialModel>();
            using (var db = new jupiterEntities())
            {
                Testimonial testimonial = db.Testimonials.Find(id);
                TestimonialModel testimonialModel = new TestimonialModel();
                if (testimonial == null)
                {
                    return Json(DataNotFound(result));
                }

                Mapper.Map(testimonial, testimonialModel);
                result.Data = testimonialModel;
            }
            return Json(result);
        }

        // PUT: api/Testimonial/5
        [CheckModelFilter]
        public IHttpActionResult Put(int id, [FromBody]TestimonialModel testimonialModel)
        {
            var result = new Result<String>();
            using (var db = new jupiterEntities())
            {
                var a = db.Testimonials.Where(x => x.TestimonialId == id).Select(x => x).FirstOrDefault();
                if (a == null)
                {
                    return Json(DataNotFound(result));
                }
                Type type = typeof(Testimonial);
                UpdateTable(testimonialModel, type, a);
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
        // POST: api/Testimonial
        [CheckModelFilter]
        public IHttpActionResult Post([FromBody]TestimonialModel testimonialModel)
        {
            var result = new Result<TestimonialModel>();
            Testimonial testimonial = new Testimonial();
            using (var db = new jupiterEntities())
            {
                Mapper.Map(testimonialModel, testimonial);
                result.Data = testimonialModel;
                try
                {
                    db.Testimonials.Add(testimonial);
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

        // DELETE: api/Testimonial/5
        public IHttpActionResult Delete(int id)
        {
            var result = new Result<String>();
            using (var db = new jupiterEntities())
            {
                Testimonial testimonial = db.Testimonials.Find(id);
                if (testimonial == null)
                {
                    return Json(DataNotFound(result));
                }
                try
                {
                    db.Testimonials.Remove(testimonial);
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