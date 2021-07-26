using System;

namespace Netsoft.SmallWorld.Api.DTOs
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006")]
    public class CardInfo
    {
        public int id { get; set; }
        public string type { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int level { get; set; }
        public string race { get; set; }
        public string attribute { get; set; }
        public CardImage[] card_images { get; set; }
    }
}
