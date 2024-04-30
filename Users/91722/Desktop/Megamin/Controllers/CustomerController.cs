using MINDCRUD1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MINDCRUD1.Models.CustomerModelDAL;


namespace MINDCRUD1.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
         
        CustomerModelDAL cmBusiness = new CustomerModelDAL();

        public ActionResult Index()
        {
            ModelState.Clear();
            tbl_UserInfo model = new tbl_UserInfo();
            model.Customers= cmBusiness.GetCustomers();
            return View(model);
        }

        [HttpPost]
        public ActionResult InsertCustomer(tbl_UserInfo objModel)
        {
            bool IsInserted = false;
            try
            {
                IsInserted = cmBusiness.InsertCustomer(objModel);
                if (IsInserted)
                {
                    ViewBag.Message = "Customer Added Successfully";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Message = "Unsucessfull";
                    ModelState.Clear();
                }

                return RedirectToAction("CustomerIndex");
            }
            catch
            {
                throw;
            }
        }

        public JsonResult EditCustomer(int? id)
        {
            var customer = cmBusiness.GetCustomers().Find(x => x.UserId.Equals(id));
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult UpdateCustomer(CustomerModel objModel)
        //{
        //    try
        //    {
        //        int result = cmBusiness.UpdateCustomer(objModel);
        //        if (result == 1)
        //        {
        //            ViewBag.Message = "Customer Updated Successfully";
        //            ModelState.Clear();
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Unsucessfull";
        //            ModelState.Clear();
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public ActionResult DeleteCustomer(int id)
        //{
        //    try
        //    {
        //        int result = cmBusiness.DeleteCustomer(id);
        //        if (result == 1)
        //        {
        //            ViewBag.Message = "Customer Deleted Successfully";
        //            ModelState.Clear();
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Unsucessfull";
        //            ModelState.Clear();
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}