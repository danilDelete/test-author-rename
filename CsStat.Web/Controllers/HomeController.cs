﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BusinessFacade.Repositories;
using CsStat.Domain.Entities;
using CsStat.Web.Models;
using Newtonsoft.Json;

namespace CSStat.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private static IPlayerRepository _playerRepository;

        public HomeController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;

    }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetReposutory()
        {

            var playersStat = _playerRepository.GetStatsForAllPlayers().OrderByDescending(x => x.KdRatio); // =  GetStat(string dateFrom, string dateTo).OrderByDescending(x=>x.Points).ThenByDescending(x=>x.KdRatio);
            var json = JsonConvert.SerializeObject(playersStat);
            var result = new JsonResult
            {
                Data = playersStat,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return result;
        }

        private List<PlayerStatsViewModel> GetStat(string dateFrom="", string dateTo="")
        {
            return Mapper.Map<List<PlayerStatsViewModel>>(_playerRepository.GetStatsForAllPlayers(dateFrom, dateTo));
        }
    }
}