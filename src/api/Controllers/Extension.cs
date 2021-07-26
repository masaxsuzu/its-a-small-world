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

        public static IEnumerable<CardInfo> ToCardInto(this IEnumerable<Card> cards)
        {
            return cards
                .Select(c => new CardInfo()
                {
                    id = c.id,
                    atk = c.atk,
                    def = c.def,
                    level = c.level,
                    name = c.name,
                    type = "",

                    attribute = c.attribute switch
                    {
                        1 => "EARTH",
                        2 => "WATER",
                        4 => "FIRE",
                        8 => "WIND",
                        16 => "LIGHT",
                        32 => "DARK",
                        64 => "DIVINE",
                        _ => "UNKNOWN",
                    },
                    race = c.race switch
                    {
                        1 => "Warrior",
                        2 => "Spellcaster",
                        4 => "Fairy",
                        8 => "Fiend",
                        16 => "Zombie",
                        32 => "Machine",
                        64 => "Aqua",
                        128 => "Pyro",
                        256 => "Rock",
                        512 => "Winged Beast",
                        1024 => "Plant",
                        2048 => "Insect",
                        4096 => "Thunder",
                        8192 => "Dragon",
                        16384 => "Beast",
                        32768 => "Beast-Warrior",
                        65536 => "Dinosaur",
                        131072 => "Fish",
                        262144 => "Sea Serpent",
                        524288 => "Reptile",
                        1048576 => "Psychic",
                        2097152 => "Divine-Beast",
                        4194304 => "Creator-God",
                        8388608 => "Wyrm",
                        _ => "Unknown",
                    },
                    card_images = new CardImage[] {
                        new CardImage() {
                            id = c.id,
                            image_url = $"https://storage.googleapis.com/ygoprodeck.com/pics/${c.id}.jpg",
                            image_url_small = $"https://storage.googleapis.com/ygoprodeck.com/pics/${c.id}.jpg",
                        }
                    }
                });
        }
    }
}
