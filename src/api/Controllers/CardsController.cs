using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Netsoft.SmallWorld.Api.Contexts;
using Netsoft.SmallWorld.Api.DTOs;

namespace Netsoft.SmallWorld.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CardsController : ControllerBase
    {

        private readonly ILogger<CardsController> _logger;
        private readonly CardContext _context;

        public CardsController(ILogger<CardsController> logger, CardContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("cards")]
        public ActionResult<IEnumerable<CardInfo>> GetCards()
        {
            _logger.LogInformation("GetCards");

            var ret = FetchMainMonsters(_context)
                .ToCards(_context)
                .OrderBy(c => c.name)
                .ToCardInto()
                .ToArray();
            return Ok(new CardInfoList() { data = ret });
        }

        [HttpGet]
        [Route("hubs")]
        public ActionResult<CardInfoList> GetHubs([FromQuery] string from, [FromQuery] string to)
        {
            _logger.LogInformation("GetHubs");
            _logger.LogDebug($"{{\"from\":\"{from}\",\"to\":\"{to}\"}}");

            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                string error = "Card names must be provided.";
                _logger.LogError(error);
                return BadRequest(error);
            }

            IQueryable<Card> fetchCards = FetchMainMonsters(_context).ToCards(_context);
            var card1 = fetchCards.Where(c => c.name == from).FirstOrDefault();
            var card2 = fetchCards.Where(c => c.name == to).FirstOrDefault();

            if (card1 == null || card2 == null)
            {
                string error = "Monster cards must be provided.";
                _logger.LogError(error);
                return NotFound(error);
            }

            var ret = FetchMainMonsters(_context)
                .Where(c =>
                    (
                        (c.atk == card1.atk && c.def != card1.def && c.level != card1.level && c.race != card1.race && c.attribute != card1.attribute) ||
                        (c.atk != card1.atk && c.def == card1.def && c.level != card1.level && c.race != card1.race && c.attribute != card1.attribute) ||
                        (c.atk != card1.atk && c.def != card1.def && c.level == card1.level && c.race != card1.race && c.attribute != card1.attribute) ||
                        (c.atk != card1.atk && c.def != card1.def && c.level != card1.level && c.race == card1.race && c.attribute != card1.attribute) ||
                        (c.atk != card1.atk && c.def != card1.def && c.level != card1.level && c.race != card1.race && c.attribute == card1.attribute)
                    )
                    &&
                    (
                        (c.atk == card2.atk && c.def != card2.def && c.level != card2.level && c.race != card2.race && c.attribute != card2.attribute) ||
                        (c.atk != card2.atk && c.def == card2.def && c.level != card2.level && c.race != card2.race && c.attribute != card2.attribute) ||
                        (c.atk != card2.atk && c.def != card2.def && c.level == card2.level && c.race != card2.race && c.attribute != card2.attribute) ||
                        (c.atk != card2.atk && c.def != card2.def && c.level != card2.level && c.race == card2.race && c.attribute != card2.attribute) ||
                        (c.atk != card2.atk && c.def != card2.def && c.level != card2.level && c.race != card2.race && c.attribute == card2.attribute)
                    )
                )
                .ToCards(_context)
                .OrderBy(c => c.id)
                .ToCardInto()
                .ToArray();

            return Ok(new CardInfoList() { data = ret });
        }

        private IQueryable<Datum> FetchMainMonsters(CardContext context)
        {
            return context
                .Datas
                .Where(d =>
                    (d.type & 0x01) == 1L &&
                    (d.type & 0x40) == 0L &&        // Not Fusion monsters
                    (d.type & 0x2000) == 0L &&      // Not Synchro monsters
                    (d.type & 0x4000) == 0L &&      // Not Tokens
                    (d.type & 0x800000) == 0L &&    // Not Xyz monsters
                    (d.type & 0x4000000) == 0L);    // Not Link monsters
        }
    }
}
