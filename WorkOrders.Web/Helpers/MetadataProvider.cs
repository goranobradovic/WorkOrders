using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WorkOrders.Domain.Models.Common;

namespace WorkOrders.Web.Helpers
{
    using Resource = Resources.Localize;

    public class MetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected static List<String> ReadOnlyProps = new List<String>() { "Id" };

        protected static Dictionary<String, String> TemplateHints = new Dictionary<string, string>()
        {
            {"Id", ""}
        };

        protected static Dictionary<Type, String> TemplatesForTypes = new Dictionary<Type, string>()
            {
                {typeof (int), "string"},
                {typeof (int?), "string"},
                {typeof (long), "string"},
                {typeof (long?), "string"},
            };

        protected static List<String> IgnoredProps = new List<string>() { "JournalingId" };

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

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            var metadata = base.GetMetadataForType(modelAccessor, modelType);

            return metadata;
        }

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            CustomizeMetadata(containerType, propertyName, metadata);
            return metadata;
        }
        private static void CustomizeMetadata(Type containerType, string propertyName, ModelMetadata metadata)
        {
            if (propertyName == null)
            {
                return;
            }
            metadata.DisplayName = Resource.ResourceManager.GetString(propertyName) ?? propertyName;
            metadata.IsReadOnly = !containerType.GetProperty(propertyName).CanWrite;
            if (containerType.IsSubclassOf(typeof(BaseModel)))
            {
                if (ReadOnlyProps.Contains(propertyName))
                {
                    metadata.IsReadOnly = true;
                }
                if (TemplatesForTypes.ContainsKey(metadata.ModelType))
                {
                    metadata.TemplateHint = TemplatesForTypes[metadata.ModelType];
                }
                if (TemplateHints.ContainsKey(propertyName))
                {
                    metadata.TemplateHint = TemplateHints[propertyName];
                    metadata.HideSurroundingHtml = true;
                }
                if (IgnoredProps.Contains(propertyName))
                {
                    metadata.ShowForDisplay = false;
                    metadata.ShowForEdit = false;
                }
            }
            var order = Resource.ResourceManager.GetString("FieldOrder_" + propertyName);
            int orderParsed = 100;
            if (int.TryParse(order, out orderParsed))
            {
                metadata.Order = orderParsed;
            }
        }
    }
}