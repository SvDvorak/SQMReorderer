using System.Collections.Generic;
using SQMImportExport.Import.ResultObjects;

namespace SQMImportExport.Import
{
    public abstract class MissionStateBase
    {
        public IEnumerable<VehicleBase> Groups { get; set; }
    }
}