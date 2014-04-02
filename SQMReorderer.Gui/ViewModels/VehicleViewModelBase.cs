using SQMImportExport.Common;

namespace SQMReorderer.Gui.ViewModels
{
    public abstract class VehicleViewModelBase : ViewModelBase
    {
        public VehicleBase Vehicle { get; protected set; }
    }
}