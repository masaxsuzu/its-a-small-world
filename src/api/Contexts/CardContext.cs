using Microsoft.EntityFrameworkCore;

using Netsoft.SmallWorld.Api.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netsoft.SmallWorld.Api.Contexts
{
    public class CardContext : DbContext
    {
        public DbSet<Datum> Datas { get; set; }
        public DbSet<Text> Texts { get; set; }
        public CardContext(DbContextOptions<CardContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder
                .Entity<Datum>(x =>
                {
                    _ = x.ToTable("datas").HasKey("id");
                    _ = x.Property(x => x.id);
                    _ = x.Property(x => x.alias);
                    _ = x.Property(x => x.atk);
                    _ = x.Property(x => x.attribute);
                    _ = x.Property(x => x.category);
                    _ = x.Property(x => x.def);
                    _ = x.Property(x => x.level);
                    _ = x.Property(x => x.ot);
                    _ = x.Property(x => x.race);
                    _ = x.Property(x => x.setcode);
                    _ = x.Property(x => x.type);
                })
                .Entity<Text>(x =>
                {
                    _ = _ = x.ToTable("texts").HasKey("id");
                    _ = x.Property(x => x.id);
                    _ = x.Property(x => x.name);
                    _ = x.Property(x => x.desc);
                    _ = x.Property(x => x.str1);
                    _ = x.Property(x => x.str2);
                    _ = x.Property(x => x.str3);
                    _ = x.Property(x => x.str4);
                    _ = x.Property(x => x.str5);
                    _ = x.Property(x => x.str6);
                    _ = x.Property(x => x.str7);
                    _ = x.Property(x => x.str8);
                    _ = x.Property(x => x.str9);
                    _ = x.Property(x => x.str10);
                    _ = x.Property(x => x.str11);
                    _ = x.Property(x => x.str12);
                    _ = x.Property(x => x.str13);
                    _ = x.Property(x => x.str14);
                    _ = x.Property(x => x.str15);
                    _ = x.Property(x => x.str16);
                });

        }
    }
}
