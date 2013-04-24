using System;
using System.Collections.Generic;

namespace FubuMVC.SlickGrid.Export
{
    public class SlickGridExcelExportSettings
    {
        public SlickGridExcelExportSettings()
        {
            GridDefinitions = new List<Type>();
        }

        public List<Type> GridDefinitions { get; set; }
    }
}
