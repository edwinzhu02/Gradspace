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
    public class ContactController : BaseController
    {
        public IHttpActionResult Get()
        {
            var result = new Result<List<ContactModel>>();
            using (var db = new jupiterEntities())
            {
                List<ContactModel> contactModels = new List<ContactModel>();
                List<Contact> contacts = db.Contacts.ToList();

                Mapper.Map(contacts, contactModels);

                result.Data = contactModels;
                return Json(result);
            }
        }
        //TODO testing error output
        public IHttpActionResult Get(int id)
        {
            var result = new Result<ContactModel>();
            using (var db = new jupiterEntities())
            {
                var a = db.Contacts.Where(x => x.ContactId == id).Select(x => x).FirstOrDefault();
                if (a == null)
                {
                    result.IsFound = false;
                    result.ErrorMessage = "Not Found";
                    return Json(result);
                }
                ContactModel contactModel = new ContactModel();
                Mapper.Map(a, contactModel);
                result.Data = contactModel;
                return Json(result);
            }
        }
        [ResultFilter]
        public IHttpActionResult Post([FromBody]ContactModel contactModel)
        {
            var result = new Result<string>();
 
            using (var db = new jupiterEntities())
            {
                Contact a = new Contact();
                Mapper.Map(contactModel, a);
                try
                {
                    db.Contacts.Add(a);
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
        [ResultFilter]
        public IHttpActionResult Put(int id, [FromBody] ContactModel contactModel)
        {
            var result = new Result<string>();
            Type conType = typeof(Contact);
            using (var db = new jupiterEntities())
            {
                Contact a = db.Contacts.Where(x => x.ContactId == id).Select(x => x).FirstOrDefault();
                if (a == null)
                {
                    result.ErrorMessage = "Not Found";
                    result.IsFound = false;
                    return Json(result);
                }
                UpdateTable(contactModel,conType,a);
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
    }
}
