﻿namespace Charltone.Domain
{
    public class HomePageImage : EntityBase
    {
        public virtual byte[] Data { get; set; }
        public virtual int SortOrder { get; set; }
    }
}