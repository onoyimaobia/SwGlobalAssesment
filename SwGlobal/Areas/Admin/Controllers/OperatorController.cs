using SwGlobal.Notification;
using SwGlobalData.Service.DTO.Payload;
using SwGlobalData.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SwGlobal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OperatorController : Controller
    {
        private readonly IOperatorService _operatorService;
            public OperatorController(IOperatorService operatorService)
            {
                _operatorService = operatorService;
            }
        // GET: Admin/Operator
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Operator/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Operator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Operator/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( OperatorPayload payload, HttpPostedFileBase operatorImage)
        {
            try
            {
                //save image
                
                    if (operatorImage.ContentLength == 0)
                    {
                        ModelState.AddModelError("", "operator Image is required");
                        return View(payload);
                    }
                    int MaxContentLength = 1024 * 1024 * 2;
                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png", ".jfif", ".jpeg" };
                    FileInfo fi = new FileInfo(operatorImage.FileName);
                    if (!AllowedFileExtensions.Contains(fi.Extension.ToLower()))
                    {
                        ModelState.AddModelError("", "allowed extension:.jpg, .gif, .png, .jfif, .jpeg ");
                        return View(payload);
                    }
                    if (operatorImage.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("", "Please select payment receipt screenshot less than 2mb");
                        return View(payload);
                    }
                var imagePath = UploadPaymentReceipt(operatorImage);
                if(string.IsNullOrEmpty(imagePath))
                {
                    ModelState.AddModelError("", "unable to upload image");
                    return View(payload);
                }
                payload.OperatorImage = imagePath;
                //valida url of operator
               if(!ValidateUrl(payload.OperatorUrl))
                {
                    ModelState.AddModelError("", "inavlid Url provided");
                    return View(payload);
                }
                if (ModelState.IsValid)
                {
                    
                    var result = await _operatorService.CreateOperator(payload);
                    if(result.Status)
                    {
                        TempData["alert_data"] = new Alert(result.Message, AlertStatus.Success);
                    }
                    TempData["alert_data"] = new Alert(result.Message, AlertStatus.Error);
                }
                // TODO: Add insert logic here
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private bool ValidateUrl(string url)
        {
            try
            {
                Uri myUri = new Uri(url);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        [NonAction]
        private string UploadPaymentReceipt(HttpPostedFileBase image)
        {
            try
            {
                //string path = Server.MapPath("~/Storage/HowThingsWork/");
                if (image.ContentLength > 0)
                {
                    FileInfo fi = new FileInfo(image.FileName);
                    var storageDirectory = "~/Images/operators";
                    var fileName = Guid.NewGuid().ToString() + image.FileName;
                    var path = Path.Combine(
                        Server.MapPath(storageDirectory),
                        fileName
                    );
                    image.SaveAs(path);
                   
                    var url = "/Images/operators" + fileName;
                   
                    return url;
                }
                return "";


            }
            catch (Exception ex)
            {
                return "";
            }
        }
        // GET: Admin/Operator/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Operator/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Operator/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Operator/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
