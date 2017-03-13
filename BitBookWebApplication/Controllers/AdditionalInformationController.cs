using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BitBookWebApplication.BLL;
using BitBookWebApplication.DAL;
using BitBookWebApplication.Models;

namespace BitBookWebApplication.Controllers
{
    public class AdditionalInformationController : Controller
    {
        //
        // GET: /AdditionalInformation/
        SignUpManager signUpManager=new SignUpManager();
        EditGateway editGateway=new EditGateway();
        HomeGateway homeGateway=new HomeGateway();
        public ActionResult Index(string id)
        {
            ViewBag.SignupID = id;
            ViewBag.Password = (string) Session["Password"];
            return View();
        }
        [HttpPost]
        public ActionResult Index(AdditionalInfo additionalInfo)
        {                          
            HttpPostedFileBase file = additionalInfo.ProfilePhoto;
            if (file!=null)
            {
            string fileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(fileName);
            int fileSize = file.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".bmp" ||
                fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".png")
            {
                Stream stream = file.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                additionalInfo.PhotoInByte = bytes;
            }
            else
            {
                ViewBag.Message = "Only images (.jpg, .png, .gif and .bmp) can be uploaded";
                return View();
            }
            }
            else
            {
                additionalInfo.ProfilePhotoId = 0;
            }

            HttpPostedFileBase files = additionalInfo.CoverPhoto;
            if (files!=null)
            {
            string fileNames = Path.GetFileName(files.FileName);
            string fileExtensions = Path.GetExtension(fileNames);
            int fileSizes = files.ContentLength;

            if (fileExtensions.ToLower() == ".jpg" || fileExtensions.ToLower() == ".bmp" ||
                fileExtensions.ToLower() == ".gif" || fileExtensions.ToLower() == ".png")
            {
                Stream streams = files.InputStream;
                BinaryReader binaryReaders = new BinaryReader(streams);
                byte[] bytess = binaryReaders.ReadBytes((int)streams.Length);
                additionalInfo.CoverPhotoInByte = bytess;
            }
            else
            {
                ViewBag.Message = "Only images (.jpg, .png, .gif and .bmp) can be uploaded";
                return View();
            }
            }
            else
            {
                additionalInfo.CoverPhotoId = 0;
            }
            if (additionalInfo.AboutMe==null)
            {
                additionalInfo.AboutMe = ".";
            }
            if (additionalInfo.Religion == null)
            {
                additionalInfo.Religion = ".";
            }
            if (additionalInfo.MobileNumber == null)
            {
                additionalInfo.MobileNumber = ".";
            }
            additionalInfo.EducationID = signUpManager.GetEducationInformation(additionalInfo);
            additionalInfo.JobID = signUpManager.GetJobInformation(additionalInfo);
            additionalInfo.ID = signUpManager.GetAdditionalInformationID(additionalInfo);
            if(additionalInfo.EdLevel!=null && additionalInfo.EdName!=null && additionalInfo.Edyear!=null)
            {
                if (additionalInfo.EducationID == 0)
                {
                    if (signUpManager.SetEducationInformation(additionalInfo))
                    {
                        additionalInfo.EducationID = signUpManager.GetEducationInformation(additionalInfo);
                    }
                    else
                    {
                        ViewBag.Message = "There is some data while entering Education information";
                        return View();
                    }
                }
            }
            if (additionalInfo.JobName != null && additionalInfo.JobPosition != null && additionalInfo.JobBegin != null && additionalInfo.JobEnd != null)
            {
                if (additionalInfo.JobID == 0)
                {
                    if (signUpManager.SetJobInformation(additionalInfo))
                    {
                        additionalInfo.JobID = signUpManager.GetJobInformation(additionalInfo);
                    }
                    else
                    {
                        ViewBag.Message = "There is some data while entering job information";
                        return View();
                    }
                }
            }
            if (additionalInfo.ID == 0)
            {
                if (signUpManager.SetAdditionInformation(additionalInfo))
                {
                    additionalInfo.ID = signUpManager.GetAdditionalInformationID(additionalInfo);
                }
                else
                {
                    ViewBag.Message = "There is some data while entering additional information";
                    return View();
                }
            }
            if (signUpManager.SetAdditionPersonInformation(additionalInfo))
            {
                Authentication authentication = new Authentication();
                authentication.Id = additionalInfo.SignupID;
                authentication.Password = additionalInfo.Passowrd;
                Session["authentication"] = authentication;
                Session["authenticationId"] = authentication.Id;
                var authenticationId = (int)Session["authenticationId"];
                return RedirectToAction("HomePage", "Home", new { authenticationInfo = authenticationId });
            }
            ViewBag.Message = "There is some error while entering Data";
            return View();
        }

	}
}