using System;

namespace Netsoft.SmallWorld.Api.DTOs
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006")]
    public class Datum
    {
        public int id { get; set; }
        public int ot { get; set; }
        public int alias { get; set; }
        public int setcode { get; set; }
        public int type { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int level { get; set; }
        public int race { get; set; }
        public int attribute { get; set; }
        public int category { get; set; }
    }
}
