using System.Collections.Generic;

namespace SQMImportExport.Import.ResultObjects
{
    public abstract class VehicleBase
    {
        public int Number { get; set; }
        public Vector Position { get; set; }
        public IEnumerable<VehicleBase> Vehicles { get; set; }
    }
}