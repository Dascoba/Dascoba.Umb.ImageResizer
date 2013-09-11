using System.IO;
using System.Web;
using System.Xml.Linq;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.propertytype;
using umbraco.cms.businesslogic.web;

namespace Dascoba.Umb.ImageResizer
{
    internal class DocumentHandlers : ApplicationBase
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "DocumentHandlers" /> class.
        /// </summary>
        public DocumentHandlers()
        {
            Document.AfterSave += Document_AfterSave;
        }

        void Document_AfterSave(Document sender, SaveEventArgs e)
        {
            // Get list of property types
            var propertyTypes = sender.ContentType.PropertyTypes;

            // loop through property types
            foreach (var propertyType in propertyTypes)
            {
                try
                {
                    // try to find an image property
                    var property = sender.getProperty(propertyType);

                    if (property.Value == null) continue;

                    if (property.)

                }
                catch
                {
                    continue;
                }
            }
        }
 
    }
}