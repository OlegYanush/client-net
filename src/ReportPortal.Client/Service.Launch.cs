﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportPortal.Client.Extentions;
using ReportPortal.Client.Filtering;
using ReportPortal.Client.Models;
using ReportPortal.Client.Requests;
using RestSharp;
using ReportPortal.Client.Converters;

namespace ReportPortal.Client
{
    public partial class Service
    {
        /// <summary>
        /// Returns a list of launches for current project.
        /// </summary>
        /// <param name="filterOption">Specified criterias for retrieving launches.</param>
        /// <param name="debug">Returns user debug launches or not.</param>
        /// <returns>A list of launches.</returns>
        public LaunchesContainer GetLaunches(FilterOption filterOption = null, bool debug = false)
        {
            var request = !debug ? new RestRequest(Project + "/launch") : new RestRequest(Project + "/launch/mode");
            
            if (filterOption != null)
            {
                foreach (var p in filterOption.ConvertToDictionary())
                {
                    request.AddParameter(p.Key, p.Value);
                }
            }
            var response = _restClient.ExecuteWithErrorHandling(request);
            return ModelSerializer.Deserialize<LaunchesContainer>(response.Content);
        }

        /// <summary>
        /// Returns specified launch by ID.
        /// </summary>
        /// <param name="id">ID of the launch to retrieve.</param>
        /// <returns>A representation of launch.</returns>
        public Launch GetLaunch(string id)
        {
            var request = new RestRequest(Project + "/launch/" + id);
            var response = _restClient.ExecuteWithErrorHandling(request);
            return ModelSerializer.Deserialize<Launch>(response.Content);
        }

        /// <summary>
        /// Creates a new launch.
        /// </summary>
        /// <param name="model">Information about representation of launch.</param>
        /// <returns>Representation of just created launch.</returns>
        public Launch StartLaunch(StartLaunchRequest model)
        {
            var request = new RestRequest(Project + "/launch/", Method.POST);
            var body = ModelSerializer.Serialize<StartLaunchRequest>(model);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = _restClient.ExecuteWithErrorHandling(request);
            return ModelSerializer.Deserialize<Launch>(response.Content);
        }

        public async Task<Launch> StartLaunchAsync(StartLaunchRequest model)
        {
            return await Task.Run(() => StartLaunch(model));
        }

        /// <summary>
        /// Finishes specified launch.
        /// </summary>
        /// <param name="id">ID of specified launch.</param>
        /// <param name="model">Information about representation of launch to finish.</param>
        /// <param name="force">Force finish launch even if test items are in progress.</param>
        /// <returns>A message from service.</returns>
        public Message FinishLaunch(string id, FinishLaunchRequest model, bool force = false)
        {
            RestRequest request;
            if (force)
            {
                request = new RestRequest(Project + "/launch/" + id + "/stop", Method.PUT);
            }
            else
            {
                request = new RestRequest(Project + "/launch/" + id + "/finish", Method.PUT);
            }
            var body = ModelSerializer.Serialize<FinishLaunchRequest>(model);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = _restClient.ExecuteWithErrorHandling(request);
            return ModelSerializer.Deserialize<Message>(response.Content);
        }

        public async Task<Message> FinishLaunchAsync(string id, FinishLaunchRequest model, bool force = false)
        {
            return await Task.Run(() => FinishLaunch(id, model, force));
        }

        /// <summary>
        /// Deletes specified launch.
        /// </summary>
        /// <param name="id">ID of the launch to delete.</param>
        /// <returns>A message from service.</returns>
        public Message DeleteLaunch(string id)
        {
            var request = new RestRequest(Project + "/launch/" + id, Method.DELETE);
            var response = _restClient.ExecuteWithErrorHandling(request);
            return ModelSerializer.Deserialize<Message>(response.Content);
        }

        /// <summary>
        /// Merge several launches.
        /// </summary>
        /// <param name="model">Request for merging.</param>
        /// <returns>Returns the model of merged launches.</returns>
        public Launch MergeLaunches(MergeLaunchesRequest model)
        {
            var request = new RestRequest(Project + "/launch/merge", Method.POST);
            var body = ModelSerializer.Serialize<MergeLaunchesRequest>(model);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = _restClient.ExecuteWithErrorHandling(request);
            return ModelSerializer.Deserialize<Launch>(response.Content);
        }

        /// <summary>
        /// Update specified launch.
        /// </summary>
        /// <param name="id">ID of launch to update.</param>
        /// <param name="model">Information about launch.</param>
        /// <returns>A message from service.</returns>
        public Message UpdateLaunch(string id, UpdateLaunchRequest model)
        {
            var request = new RestRequest(Project + "/launch/" + id + "/update", Method.PUT);
            var body = ModelSerializer.Serialize<UpdateLaunchRequest>(model);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = _restClient.ExecuteWithErrorHandling(request);
            return ModelSerializer.Deserialize<Message>(response.Content);
        }

        /// <summary>
        /// Merge several launches.
        /// </summary>
        /// <param name="id">Request for merging.</param>
        /// <param name="strategy">Known strategy is 'history'.</param>
        /// <returns>A message from service.</returns>
        public Message AnalyzeLaunch(string id, string strategy)
        {
            var request = new RestRequest(Project + "/launch/" + id +"/analyze/" + strategy, Method.POST);
            var response = _restClient.ExecuteWithErrorHandling(request);
            return ModelSerializer.Deserialize<Message>(response.Content);
        }
    }
}
