﻿using Charltone.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Charltone.Domain.Mappings
{
    public sealed class InstrumentTypeMap : ClassMap<InstrumentType>
    {
        public InstrumentTypeMap()
        {
            Table("InstrumentType");

            Id(x => x.Id).Column("Id");
            Map(m => m.InstrumentTypeDesc).Column("InstrumentTypeDesc");
            Map(m => m.SortOrder).Column("SortOrder");
        }
    }
}