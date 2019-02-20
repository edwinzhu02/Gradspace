﻿using System;
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
        private jupiterEntities db = new jupiterEntities();

        // GET: api/Testimonial
        public IHttpActionResult Get()
        {
            var result = new Result<List<TestimonialModel>>();
            List<TestimonialModel> testimonialModels = new List<TestimonialModel>();
            List<Testimonial> testimonials = db.Testimonials.Select(x => x).ToList();
            Mapper.Map(testimonials, testimonialModels);
            result.Data = testimonialModels;
            return Json(result);
        }

        // GET: api/Testimonial/5
        public IHttpActionResult Get(int id)
        {
            var result = new Result<TestimonialModel>();
            Testimonial testimonial = db.Testimonials.Find(id);
            TestimonialModel testimonialModel = new TestimonialModel();
            if (testimonial == null)
            {
                return Json(NotFound(result));
            }

            Mapper.Map(testimonial, testimonialModel);
            result.Data = testimonialModel;
            return Json(result);
        }

        // PUT: api/Testimonial/5
        [ResultFilter]
        public IHttpActionResult Put(int id, [FromBody]TestimonialModel testimonialModel)
        {
            var result = new Result<String>();

            //db.Entry(testimonial).State = EntityState.Modified;
            var a = db.Testimonials.Where(x => x.TestimonialId == id).Select(x => x).FirstOrDefault();
            if (a == null)
            {
                return Json(NotFound(result));
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
            return Json(result);
            //return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/Testimonial
        // [ResponseType(typeof(Testimonial))]
        [ResultFilter]
        public IHttpActionResult Post([FromBody]TestimonialModel testimonialModel)
        {
            var result = new Result<TestimonialModel>();
            Testimonial testimonial = new Testimonial();
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

            //return CreatedAtRoute("DefaultApi", new { id = testimonial.TestimonialId }, testimonial);
            return Json(result);
        }

        // DELETE: api/Testimonial/5
        public IHttpActionResult Delete(int id)
        {
            var result = new Result<String>();
            Testimonial testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return Json(NotFound(result));
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
            return Json(result);
        }

    }
}