using System;
using System.Collections.Generic;
using FubuCore.Descriptions;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Core.Resources.Conneg;

namespace FubuMVC.Export
{
    public class ExcelWriterNode<T> : WriterNode
    {
        private readonly Type _mappingType;

        public ExcelWriterNode(Type mappingType)
        {
            _mappingType = mappingType;
        }

        public Type MappingType
        {
            get { return _mappingType; }
        }

        protected override ObjectDef toWriterDef()
        {
            var def = new ObjectDef(typeof (ExcelMediaWriter<T>));
            configureWriterDependency(def);
            return def;
        }

        private void configureWriterDependency(ObjectDef def)
        {
            def.DependencyByType(typeof (IExcelMapping), _mappingType);
        }

        protected override void createDescription(Description description)
        {
            description.ShortDescription = "Writer for " + typeof (T).Name;
        }

        public override Type ResourceType
        {
            get { return typeof (T); }
        }

        public override IEnumerable<string> Mimetypes
        {
            get
            {
                yield return "application/xlsx";
            }
        }
    }
}