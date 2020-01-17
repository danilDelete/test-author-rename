using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using BusinessFacade.Repositories;
using CsStat.Domain;
using CsStat.Domain.Entities;
using CsStat.Web.Models;
using DataService;
using ErrorLogger;
using Microsoft.Ajax.Utilities;

namespace CsStat.Web.Controllers
{
    public class WikiController : BaseController
    {
        // GET
        private static IUsefulLinkRepository _usefulLinkRepository;
        private static ILogger _logger;

        public WikiController(IUsefulLinkRepository usefulLinkRepository)
        {
            _usefulLinkRepository = usefulLinkRepository;
            var connectionString = new ConnectionStringFactory();
            var mongoRepository = new MongoRepositoryFactory(connectionString);
            _logger = new Logger(mongoRepository);
        }
        public ActionResult Index()
        {
            var isAdminMode = Session["IsAdminMode"] != null && Session["IsAdminMode"].ToString() == "true";
            var usefulInfos = _usefulLinkRepository.GetAll().OrderByDescending(x => x.PublishDate);
            var model = new UsefulLinksViewModel
            {
                Items = usefulInfos != null ? Mapper.Map<List<InfoViewModel>>(usefulInfos) : new List<InfoViewModel>(),
                IsAdminMode = isAdminMode
            };
            return View("~/Views/UsefulInfo/Index.cshtml", model);
        }

        public ActionResult GetInfo()
        {
            var isAdminMode = Session["IsAdminMode"] != null && Session["IsAdminMode"].ToString() == "true";
            var usefulInfos = _usefulLinkRepository.GetAll()?.OrderByDescending(x=>x.PublishDate);
            var model = new UsefulLinksViewModel
            {
                Items = usefulInfos != null ? Mapper.Map<List<InfoViewModel>>(usefulInfos) : new List<InfoViewModel>(),
                IsAdminMode = isAdminMode
            };
            return new JsonResult
            {
                Data = model,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult GetAllArticlesFromCms()
        {
            var json = GetJson(Settings.ArticlesPath);
            return new JsonResult
            {
                Data = json,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        private string GetJson(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    return reader.ReadToEnd();
                }
            }
        }

        public ActionResult Add(string id="")
        {
            var isAdminMode = Session["IsAdminMode"] != null && Session["IsAdminMode"].ToString() == "true";
            
            if (!isAdminMode)
            {
                return RedirectToAction("SignIn", "SignIn");
            }

            if (!id.IsNullOrWhiteSpace())
            {
                var info = _usefulLinkRepository.GetInfo(id);
                return View("~/Views/UsefulInfo/AddForm.cshtml", info);
            }

            return View("~/Views/UsefulInfo/AddForm.cshtml", new UsefulInfo());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Save(InfoViewModel infoModel)
        {
            if (infoModel != null)
            {
                var imageName = Path.GetFileName(infoModel.Image?.FileName);
                if (imageName != null)
                {
                    Directory.CreateDirectory(Server.MapPath("~/Files/Images"));
                    var path = Path.Combine(Server.MapPath("~/Files/Images"), imageName);
                    infoModel.Image.SaveAs(path);
                    infoModel.ImagePath = imageName;
                }
                var info = Mapper.Map<UsefulInfo>(infoModel);
                try
                {
                    if (infoModel.Id.IsNullOrWhiteSpace())
                    {
                        _usefulLinkRepository.Add(info);
                    }
                    else
                    {
                        _usefulLinkRepository.Update(info.Id, info);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex,"Error occurred during saving info");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Remove(string id)
        {
            _usefulLinkRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}