using Umbraco.Core;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.media;
using umbraco.cms.businesslogic.web;

namespace Dascoba.Umb.ImageResizer
{
    /// <summary>
    /// AfterSaveHandler
    /// 
    /// Used when media is created by some other means besides the standard backoffice method
    /// that creates a single media at a time. (i.e. via the API)
    /// 
    /// Doesn't currently work since Umbraco only hits the AfterSave event on single items in the backoffice
    /// 
    /// </summary>
    public class AfterSaveHandler : IApplicationEventHandler
    {
#pragma warning disable 612,618
        /// <summary>
        /// Initializes a new instance of the <see cref="AfterSaveHandler"/> class.
        /// </summary>
        public AfterSaveHandler()
        {
            Document.AfterSave += Document_AfterSave;
            Media.AfterSave += Media_AfterSave;
        }

        /// <summary>
        /// Handles the AfterSave event for Document
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="umbraco.cms.businesslogic.SaveEventArgs"/> instance containing the event data.</param>
        private static void Document_AfterSave(Document sender, SaveEventArgs e)
        {
            ProcessProperties(sender);
        }

        /// <summary>
        /// Handles the AfterSave event for Media
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="umbraco.cms.businesslogic.SaveEventArgs"/> instance containing the event data.</param>
        private static void Media_AfterSave(Media sender, SaveEventArgs e)
        {
            ProcessProperties(sender);
        }

        /// <summary>
        /// Processes the properties.
        /// Will resize images when ImageResizer property is found
        /// </summary>
        /// <param name="node">The node.</param>
        private static void ProcessProperties(Content node)
        {
            var nodeId = node.Id;
            var properties = node.GenericProperties;

            foreach (var property in properties)
            {
                // Check if this is an ImageResizer property
                if (property.PropertyType.DataTypeDefinition.DataType.Id != ImageResizerHelper.DataTypeId) continue;

                // Resize the image
                var prevalueEditor = (ImageResizerPrevalueEditor) property.PropertyType.DataTypeDefinition.DataType.PrevalueEditor;

                ImageResizerHelper.ResizeImage(ImageResizerHelper.GetOriginalUrl(nodeId, prevalueEditor), prevalueEditor.MaxHeight, prevalueEditor.MaxHeight);
            }
        }
#pragma warning restore 612,618

        #region Implementation of IApplicationEventHandler

        /// <summary>
        /// ApplicationContext is created and other static objects that require initialization have been setup
        /// </summary>
        /// <param name="umbracoApplication"/><param name="applicationContext"/>
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// All resolvers have been initialized but resolution is not frozen so they can be modified in this method
        /// </summary>
        /// <param name="umbracoApplication"/><param name="applicationContext"/>
        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Bootup is completed, this allows you to perform any other bootup logic required for the application.
        ///             Resolution is frozen so now they can be used to resolve instances.
        /// </summary>
        /// <param name="umbracoApplication"/><param name="applicationContext"/>
        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //throw new System.NotImplementedException();
        }

        #endregion
    }
}