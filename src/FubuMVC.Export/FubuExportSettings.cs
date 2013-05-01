using System.Collections.Generic;

namespace FubuMVC.Export
{
    public class FubuExportSettings
    {
        private readonly IList<IExportGraphModifier> _modifiers = new List<IExportGraphModifier>();

        public IEnumerable<IExportGraphModifier> Modifiers()
        {
            return _modifiers;
        }

        public void ModifyWith(IExportGraphModifier modifier)
        {
            _modifiers.Add(modifier);
        }
    }
}