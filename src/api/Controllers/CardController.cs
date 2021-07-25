﻿using System;
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
    public class CardController : ControllerBase
    {

        private readonly ILogger<CardController> _logger;
        private readonly CardContext _context;

        public CardController(ILogger<CardController> logger, CardContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("cards")]
        public ActionResult<IEnumerable<Card>> GetCards()
        {
            _logger.LogInformation("GetCards");

            var ret = FetchMainMonsters(_context)
                .ToCards(_context)
                .OrderBy(c => c.name)
                .ToArray();
            return Ok(ret);
        }

        [HttpGet]
        [Route("hubs")]
        public ActionResult<IEnumerable<Card>> GetHubs([FromQuery] string from, [FromQuery] string to)
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
                string error = "Monster cards must be provided." ;
                _logger.LogError(error);
                return NotFound(error);
            }

            var ret = FetchMainMonsters(_context)
                .Where(c =>
                    (
                        (c.atk == card1.atk && c.def != card1.def && c.level != card1.level && c.race != card1.race && c.attribute != card1.attribute && card1.atk != -2) ||
                        (c.atk != card1.atk && c.def == card1.def && c.level != card1.level && c.race != card1.race && c.attribute != card1.attribute && card1.def != -2) ||
                        (c.atk != card1.atk && c.def != card1.def && c.level == card1.level && c.race != card1.race && c.attribute != card1.attribute) ||
                        (c.atk != card1.atk && c.def != card1.def && c.level != card1.level && c.race == card1.race && c.attribute != card1.attribute) ||
                        (c.atk != card1.atk && c.def != card1.def && c.level != card1.level && c.race != card1.race && c.attribute == card1.attribute)
                    )
                    &&
                    (
                        (c.atk == card2.atk && c.def != card2.def && c.level != card2.level && c.race != card2.race && c.attribute != card2.attribute && card2.atk != -2) ||
                        (c.atk != card2.atk && c.def == card2.def && c.level != card2.level && c.race != card2.race && c.attribute != card2.attribute && card2.def != -2) ||
                        (c.atk != card2.atk && c.def != card2.def && c.level == card2.level && c.race != card2.race && c.attribute != card2.attribute) ||
                        (c.atk != card2.atk && c.def != card2.def && c.level != card2.level && c.race == card2.race && c.attribute != card2.attribute) ||
                        (c.atk != card2.atk && c.def != card2.def && c.level != card2.level && c.race != card2.race && c.attribute == card2.attribute)
                    )
                )
                .ToCards(_context)
                .OrderBy(c => c.id)
                .ToArray();

            return Ok(ret);
        }

        private IQueryable<Datum> FetchMainMonsters(CardContext context)
        {
            return context
                .Datas
                .Where(d =>
                    (d.type & 0x40) == 0L &&        // Not Fusion monsters
                    (d.type & 0x2000) == 0L &&      // Not Synchro monsters
                    (d.type & 0x800000) == 0L &&    // Not Xyz monsters
                    (d.type & 0x4000000) == 0L);    // Not Link monsters
        }
    }
}
