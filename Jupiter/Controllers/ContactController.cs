using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
                var a = db.Contacts.Select(x =>
                    new ContactModel()
                    {
                        ContactId = x.ContactId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        PhoneNum = x.PhoneNum,
                        Email = x.Email,
                        Message = x.Message
                    }).ToList();
                result.Data = a;
                return Json(result);
            }
        }
        //TODO testing error output
        public IHttpActionResult Get(int id)
        {
            var result = new Result<ContactModel>();
            using (var db = new jupiterEntities())
            {
                var a = db.Contacts.Where(x => x.ContactId == id).Select(x => 
                    new ContactModel
                    {
                        ContactId = x.ContactId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        PhoneNum = x.PhoneNum,
                        Email = x.Email,
                        Message = x.Message
                    }).FirstOrDefault();
                if (a == null)
                {
                    result.IsFound = false;
                    result.IsSuccess = false;
                    result.ErrorMessage = "Not Found";
                    return Json(result);
                }
                result.Data = a;
                return Json(result);
            }
        }
        //TODO testing error output
        public IHttpActionResult Post([FromBody]ContactModel contactModel)
        {
            var result = CheckStateModel(ModelState);
            Contact a = new Contact();
            a.ContactId = contactModel.ContactId;
            a.FirstName = contactModel.FirstName;
            a.LastName = contactModel.LastName;
            a.PhoneNum = contactModel.PhoneNum;
            a.Email = contactModel.Email;
            a.Message = contactModel.Message;
            using (var db = new jupiterEntities())
            {
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
        //TODO: no error message when isSuccess = false
        public IHttpActionResult Put(int id, [FromBody] ContactModel contactModel)
        {
            var result = base.CheckStateModel(ModelState);
            
            if (result.IsSuccess == false) return Json(result);
            using (var db = new jupiterEntities())
            {
                Contact a = db.Contacts.Where(x => x.ContactId == id).Select(x => x).FirstOrDefault();
                if (a == null)
                {
                    result.ErrorMessage = "Not Found";
                    result.IsFound = false;
                    return Json(result);
                }

                a.FirstName = contactModel.FirstName ?? a.FirstName;
                a.LastName = contactModel.LastName ?? a.LastName;
                a.PhoneNum = contactModel.PhoneNum ?? a.PhoneNum;
                a.Email = contactModel.Email ?? a.Email;
                a.Message = contactModel.Message ?? a.Message;
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
