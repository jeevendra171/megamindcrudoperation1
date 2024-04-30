using MINDCRUD1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MINDCRUD1.Models.CustomerModelDal;

namespace MINDCRUD1.Controllers
{
    public class UserController : Controller
    {
        CustomerModelDal cmBusiness = new CustomerModelDal();
        //GET: User
        public ActionResult Index()
        {
            ModelState.Clear();
            CustomerModel model = new CustomerModel();
            model.Customers = cmBusiness.GetCustomers();
            return View(model);
        }


        [HttpPost]
        public ActionResult InsertCustomer(CustomerModel objModel)
        {
            try
            {
                int result = cmBusiness.InsertCustomer(objModel);
                if (result != null)
                {
                    ViewBag.Message = "User Added Successfully";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Message = "Unsucessfull";
                    ModelState.Clear();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Json(new { error = "No id provided" }, JsonRequestBehavior.AllowGet);
            }

            var customer = cmBusiness.GetCustomers().Find(x => x.UserId.Equals(id));
            if (customer == null)
            {
                return Json(new { error = "User not found" }, JsonRequestBehavior.AllowGet);
            }

            return Json(customer, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateCustomer(CustomerModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = cmBusiness.UpdateCustomer(objModel);
                    if (result == 1)
                    {
                        ViewBag.Message = "User Updated Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "User update unsuccessful";
                        return View(objModel); 
                    }
                }
                else
                {
                   
                    return View(objModel);
                }
            }
            catch (Exception ex)
            {
                
                ViewBag.Message = "An error occurred while updating the customer." + ex;
                return View(objModel); 
            }
        }



        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                int result = cmBusiness.DeleteCustomer(id);
                if (result == 1)
                {
                    ViewBag.Message = "Customer Deleted Successfully";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Message = "Unsucessfull";
                    ModelState.Clear();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }

        }
    }
}


