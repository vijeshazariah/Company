﻿using Company.Model;
namespace Company.DAL.DTO
{
    public class CompanyDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
        public string Exchange { get; set; }
        public string Isin { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
