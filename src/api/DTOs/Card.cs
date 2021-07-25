using System;

namespace Netsoft.SmallWorld.Api.DTOs
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006")]
    public class Card
    {
        public int id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int level { get; set; }
        public int race { get; set; }
        public int attribute { get; set; }
    }
}
