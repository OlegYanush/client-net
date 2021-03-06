﻿using System;
using System.Collections.Generic;
using ReportPortal.Client.Converters;
using System.Runtime.Serialization;

namespace ReportPortal.Client.Models
{
    [DataContract]
    public class Launch
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "number")]
        public int Number { get; set; }

        [DataMember(Name = "mode")]
        public string ModeString { get; set; }

        public LaunchMode Mode { get { return EnumConverter.ConvertTo<LaunchMode>(ModeString); } set { ModeString = EnumConverter.ConvertFrom(value); } }

        [DataMember(Name = "start_time")]
        public string StartTimeString { get; set; }

        public DateTime StartTime
        {
            get
            {
                return DateTimeConverter.ConvertTo(StartTimeString);
            }
            set
            {
                StartTimeString = DateTimeConverter.ConvertFrom(value);
            }
        }

        [DataMember(Name = "end_time")]
        public string EndTimeString { get; set; }

        public DateTime EndTime
        {
            get
            {
                return DateTimeConverter.ConvertTo(EndTimeString);
            }
            set
            {
                EndTimeString = DateTimeConverter.ConvertFrom(value);
            }
        }

        [DataMember(Name = "tags")]
        public List<string> Tags { get; set; }

        [DataMember(Name = "statistics")]
        public Statistic Statistics { get; set; }
    }

    [DataContract]
    public class Statistic
    {
        [DataMember(Name = "executions")]
        public Executions Executions { get; set; }

        [DataMember(Name = "defects")]
        public Defects Defects { get; set; }
    }

    [DataContract]
    public class Executions
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "passed")]
        public int Passed { get; set; }

        [DataMember(Name = "failed")]
        public int Failed { get; set; }

        [DataMember(Name = "skipped")]
        public int Skipped { get; set; }
    }

    [DataContract]
    public class Defects
    {
        [DataMember(Name = "product_bugs")]
        public Defect ProductBugs { get; set; }

        [DataMember(Name = "automation_bugs")]
        public Defect AutomationBugs { get; set; }

        [DataMember(Name = "system_issue")]
        public Defect SystemIssues { get; set; }

        [DataMember(Name = "to_investigate")]
        public Defect ToInvestigate { get; set; }
    }

    [DataContract]
    public class Defect
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
