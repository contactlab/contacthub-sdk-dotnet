﻿using ContactHubSdkLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ContactHubSdkLibrary.Models
{
    #region definition
    public class Customer : PostCustomer
    {
        public string id { get; set; }
        public string registeredAt { get; set; }
        public string updatedAt { get; set; }
        public Links _links { get; set; }
    }
    public class Link
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Link customers { get; set; }
        public Link events { get; set; }
        public Link sources { get; set; }
        public Link sessions { get; set; }
        public Link self { get; set; }
    }
    public class Tags
    {
        public List<String> auto { get; set; }
        public List<String> manual { get; set; }
    }

    public class PostCustomer
    {
        public string externalId { get; set; }
        public string nodeId { get; set; }
        public BaseProperties @base { get; set; }
        public Extended extended { get; set; }  /* da completare*/
        public string extra { get; set; }
        public Tags tags { get; set; }
        public bool? enabled { get; set; }
    }

    public class Extended
    {

    }

    public class PagedCustomer
    {
        public Embedded _embedded { get; set; }

        public PageLink _links { get; set; }
        public Page page { get; set; }
    }
    public class Embedded
    {
        public List<Customer> customers;
    }

    public class PageLink
    {
        public Link first { get; set; }
        public Link last { get; set; }
        public Link next { get; set; }
        public Link previous { get; set; }
        public Link self { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }
    #endregion

}
