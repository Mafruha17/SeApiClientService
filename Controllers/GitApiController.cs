﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeApiClientService.Models;

namespace SeApiClientService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitApiController : ControllerBase
    {
       
        private readonly ILogger<GitApiController> _logger;
        public GitApiController(ILogger<GitApiController> logger)
        {
            _logger = logger;
        }

        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// default get
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<List<Repository>> Get()
        {
            // following line of code coud be written in a sperate block of code/file

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
           
            var gitQueryResult = (
                "https://api.github.com/orgs/dotnet/repos?result=language:C#&sort=stars&order=desc&page=1&par_page=5");

            //could check the result status if its a bad request or not found
            var streamTask = client.GetStreamAsync(gitQueryResult);
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            // somehow querystring returning more than 5  (TODO:)
           return repositories.Count() > 5 ? repositories.Take(5).ToList() : repositories;
            
        }


        /// <summary>
        ///  pass your choice of language through URI
        /// </summary>
        /// <param name="_language"></param>
        /// <returns></returns>
        [HttpGet("{language}")]
        public async Task<List<Repository>>Get(string _language)
        {         
            // following line of code coud be written in a sperate block of code/file

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var gitQueryResult = (
                "https://api.github.com/orgs/dotnet/repos?result=language:_language&sort=stars&order=desc&page=1&par_page=5"); 

            //could check the result status if its a bad request or not found
            var streamTask = client.GetStreamAsync(gitQueryResult);
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            return repositories.Count() > 5 ? repositories.Take(5).ToList() : repositories;

        }
    }
}
