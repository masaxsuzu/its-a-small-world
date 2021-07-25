using Netsoft.SmallWorld.Api.Contexts;
using Netsoft.SmallWorld.Api.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netsoft.SmallWorld.Api.Controllers
{
    public static class Extension
    {
        public static IQueryable<Card> ToCards(this IQueryable<Datum> data, CardContext context)
        {
            return data
            .Join(
                    context.Texts,
                    d => d.id,
                    t => t.id,
                    (d, t) => new Card()
                    {
                        id = d.id,
                        type = d.type,
                        atk = d.atk,
                        def = d.def,
                        attribute = d.attribute,
                        level = d.level,
                        race = d.race,
                        name = t.name,
                    }
                );
        }
    }
}
