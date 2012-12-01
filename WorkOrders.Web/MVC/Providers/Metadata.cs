using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkOrders.Web.MVC.Providers
{
    public class Metadata : DataAnnotationsModelMetadataProvider
    {
        protected static List<String> ReadOnlyProps = new List<String>() { "Id", "LanguageLCID" };
        protected static Dictionary<String, String> TemplateHints = new Dictionary<string, string>()
        {
            {"Id", "HiddenInput"},
            {"LanguageLCID", "LanguageLCID"}
        };

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            CustomizeMetadata(containerType, propertyName, metadata);
            return metadata;
        }

        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            var metadata = base.GetMetadataForProperties(container, containerType);

            return metadata;
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            var metadata = base.GetMetadataForProperty(modelAccessor, containerType, propertyName);
            CustomizeMetadata(containerType, propertyName, metadata);
            return metadata;
        }

        private static void CustomizeMetadata(Type containerType, string propertyName, ModelMetadata metadata)
        {
            //if (propertyName == null)
            //{
            //    return;
            //}
            //metadata.DisplayName = Resources.Fields.ResourceManager.GetString(propertyName) ?? propertyName;
            //metadata.IsReadOnly = !containerType.GetProperty(propertyName).CanWrite;
            //if (containerType.IsSubclassOf(typeof(BaseModel)))
            //{
            //    if (ReadOnlyProps.Contains(propertyName))
            //    {
            //        metadata.IsReadOnly = true;
            //    }
            //    if (TemplateHints.ContainsKey(propertyName))
            //    {
            //        metadata.TemplateHint = TemplateHints[propertyName];
            //        metadata.HideSurroundingHtml = true;
            //    }
            //    if (metadata.ModelType.IsGenericType && metadata.ModelType.GetGenericArguments().Any(p => p.IsSubclassOf(typeof(BaseWebEntity))))
            //    {
            //        metadata.ShowForEdit = false;
            //        metadata.ShowForDisplay = false;
            //    }
            //}
            //if (containerType == typeof(GeoPosition))
            //{
            //    metadata.DisplayFormatString = "{0:##.######}";
            //    metadata.EditFormatString = "{0:##.######}";
            //}
            //var order = Resources.FieldOrder.ResourceManager.GetString(propertyName);
            //int orderParsed = 100;
            //if (int.TryParse(order, out orderParsed))
            //{
            //    metadata.Order = orderParsed;
            //}
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            var metadata = base.GetMetadataForType(modelAccessor, modelType);

            return metadata;
        }
    }
}