using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Jupiter.ActionFilter;
using Jupiter.Models;
using JupiterEntity;

namespace Jupiter.Controllers
{
    public class EventTypeController : BaseController
    {
        // GET: api/EventType
        public IHttpActionResult Get()
        {
            var result = new Result<List<EventTypeModel>>();
            List<EventTypeModel> eventTypeModels = new List<EventTypeModel>();
            using (var db = new jupiterEntities())
            {
                List<EventType> eventTypes = db.EventTypes.ToList();
                Mapper.Map(eventTypes, eventTypeModels);
                result.Data = eventTypeModels;
            }
            return Json(result);
        } 

        // GET: api/EventType/5
        public IHttpActionResult Get(int id)
        {
            var result = new Result<EventTypeModel>();
            using (var db = new jupiterEntities())
            {
                EventType eventType = db.EventTypes.Find(id);
                EventTypeModel eventTypeModel = new EventTypeModel();
                if (eventType == null)
                {
                    return Json(DataNotFound(result));
                }
                Mapper.Map(eventType, eventTypeModel);
                result.Data = eventTypeModel;
            }
            return Json(result);
        }

        // PUT: api/EventType/5
        [CheckModelFilter]
        public IHttpActionResult Put(int id, EventTypeModel eventTypeModel)
        {
            var result = new Result<string>();
            using (var db = new jupiterEntities())
            {
                var a = db.EventTypes.Where(x => x.TypeId == id).Select(x => x).FirstOrDefault();
                if (a == null)
                {
                    return Json(DataNotFound(result));
                }

                Type type = typeof(EventType);
                UpdateTable(eventTypeModel,type,a);
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

        // POST: api/EventType
        [CheckModelFilter]
        public IHttpActionResult Post(EventTypeModel eventTypeModel)
        {
            var result = new Result<string>();
            EventType eventType = new EventType();
            Mapper.Map(eventTypeModel, eventType);
            using (var db = new jupiterEntities())
            {
                try
                {
                    db.EventTypes.Add(eventType);
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

        // DELETE: api/EventType/5
        [CheckModelFilter]
        public IHttpActionResult Delete(int id)
        {
            var result = new Result<string>();
            using (var db = new jupiterEntities())
            {
                EventType eventType = db.EventTypes.Find(id);
                if (eventType == null)
                {
                    return Json(DataNotFound(result));
                }
                try
                {
                    db.EventTypes.Remove(eventType);
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