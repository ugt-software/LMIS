﻿using System;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class MainLinksControl : BaseExtendedControl<LinksModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var videosModel = model as LinksModel;
            if (videosModel == null)
                return;

            rptItems.DataSource = videosModel.List;
            rptItems.DataBind();
        }

        protected string GetSubLinksUrl(object eval)
        {
            var model = eval as LinkModel;
            if (model == null)
                return "#";

            Uri targetUrl;
            if (Uri.TryCreate(model.Url, UriKind.RelativeOrAbsolute, out targetUrl))
                return targetUrl.ToString();

            var url = String.Format("~/Pages/User/Links.aspx?ID={0}", model.ID);
            return url;
        }

        protected String GetUrlTarget(object dataItem)
        {
            var model = dataItem as LinkModel;
            if (model == null)
                return null;

            Uri targetUrl;
            if (Uri.TryCreate(model.Url, UriKind.RelativeOrAbsolute, out targetUrl))
                return "_target";

            return String.Empty;
        }

        protected String GetImageUrl(object eval)
        {
            var url = String.Format("~/Handlers/GetImage.ashx?Type=Link&ID={0}", eval);
            return url;
        }

        protected object GetPanelStyle(object dataItem)
        {
            var model = dataItem as LinkModel;
            if (model == null)
                return null;

            var format = "width:182px;height:128px;background-image: url({0});background-size: 182px 128px;background-repeat: no-repeat;";
            var imageUrl = GetImageUrl(model.ID);
            var absUrl = ConvertToAbsoluteUrl(imageUrl);

            var style = String.Format(format, absUrl);
            return style;
        }

        public String ConvertToAbsoluteUrl(String relativeUrl)
        {
            var protocol = (Request.IsSecureConnection ? "https" : "http");
            return String.Format("{0}://{1}{2}", protocol, Request.Url.Host, Page.ResolveUrl(relativeUrl));
        }
    }
}