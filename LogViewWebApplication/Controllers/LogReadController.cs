using System.Web.Mvc;
using Repository.PostgreSQL;
using System.Configuration;
using AutoMapper;
using System.Collections.Generic;
using LogViewWebApplication.Models;
using SharedModels;
using PagedList;
using System;
using System.Linq;
using Common;

namespace LogViewWebApplication.Controllers
{
    public class LogReadController : Controller
    {
        private string connection = ConfigurationManager.ConnectionStrings["NpgsqlConnection"].ConnectionString;

        public ActionResult Index()
        {
            var vwModel = new LogReadViewModel();
            SetViewData();
            SetDefaults(vwModel);
            return View(vwModel);
        }

        #region helpers
        public ActionResult Post(LogReadViewModel vwModel, int? page)
        {
            QueryDatabase(page, vwModel);
            SetViewData();
            return View("Index", vwModel);
        }
        private void QueryDatabase(int? page, LogReadViewModel vwModel)
        {
            if (vwModel.Description == null && vwModel.TimeFrom == DateTime.MinValue && vwModel.TimeTo == DateTime.MinValue)
                return;

            ViewBag.OnePageOfLogs = new List<LogReadViewModel>().ToPagedList(1, 1);
            var dbModel = new List<EventLogModel>();

            if (Session["dbModel"] == null)
            {
                dbModel = new EventLogRepository(connection).SelectListBetween(vwModel.TimeFrom, vwModel.TimeTo);
                Session["dbModel"] = dbModel;
            }
            else
            {
                dbModel = (List<EventLogModel>)Session["dbModel"];
                vwModel.TimeFrom = new DateTime(vwModel.TimeFrom.Year, vwModel.TimeFrom.Month, vwModel.TimeFrom.Day);
            }


            //Filter TaskCategory
            if (vwModel.TaskCategory > 0)
                dbModel = dbModel.Where(x => x.TaskCategory == vwModel.TaskCategory).ToList();

            //Filter Description
            if (vwModel.Description != null)
                dbModel = dbModel.Where(x => x.Description.Contains(vwModel.Description)).ToList();

            Mapper.Initialize(cfg => cfg.CreateMap<EventLogModel, LogReadViewModel>());
            var vwModel2 = Mapper.Map<List<LogReadViewModel>>(dbModel);

            var pageNumber = page ?? 1;
            var onePageOfLogs = vwModel2.ToPagedList(pageNumber, 25);
            ViewBag.OnePageOfLogs = onePageOfLogs;
        }
        private void SetDefaults(LogReadViewModel vwModel)
        {
            vwModel.TimeFrom = DateTime.Now;
            vwModel.TimeTo = DateTime.Now;
            ViewBag.OnePageOfLogs = new List<LogReadViewModel>().ToPagedList(1, 1);
        }
        private void SetViewData()
        {
            var list = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(CategoryModel.Categorys)))
            {
                var id = (int)item;
                var desc = item.ToString();

                list.Add(new SelectListItem() {
                    Value = id.ToString(),
                    Text = string.Format("{0} - {1}", id, desc) });
            }
            ViewData["SelectList_TaskCategory"] = list;
        }
        #endregion
    }
}
