﻿using System;
using System.Net;
using ReportPortal.Client.Extentions;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ReportPortal.Client
{
    /// <summary>
    /// Class to interact with common Report Portal services. Provides possibility to manage almost of service's entities.
    /// </summary>
    public partial class Service
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;

        /// <summary>
        /// Constructor to initialize a new object of service.
        /// </summary>
        /// <param name="uri">Base URI for REST service.</param>
        /// <param name="project">A project to manage.</param>
        /// <param name="password">A password for user. Can be UID given from user's profile page.</param>
        public Service(Uri uri, string project, string password)
        {
            _httpHandler = new HttpClientHandler();

            _httpClient = new HttpClient(_httpHandler);
            _httpClient.BaseAddress = uri;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + password);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Reporter");
            BaseUri = uri;
            Project = project;
        }

        /// <summary>
        /// Constructor to initialize a new object of service.
        /// </summary>
        /// <param name="uri">Base URI for REST service.</param>
        /// <param name="project">A project to manage.</param>
        /// <param name="password">A password for user. Can be UID given from user's profile page.</param>
        /// <param name="proxy">Proxy for all HTTP requests.</param>
        public Service(Uri uri, string project, string password, IWebProxy proxy)
            : this(uri, project, password)
        {
            _httpHandler.Proxy = proxy;
        }

        /// <summary>
        /// Get or set project name to interact with.
        /// </summary>
        public string Project { get; set; }

        public Uri BaseUri { get; set; }
    }
}
