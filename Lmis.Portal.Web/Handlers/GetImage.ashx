﻿<%@ WebHandler Language="C#" Class="GetImage" %>

using System;
using System.IO;
using System.Linq;
using System.Web;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.DAL.DAL;

public class GetImage : IHttpHandler
{
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public void ProcessRequest(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;
        var server = context.Server;

        response.ContentType = "image/png";

        var itemID = DataConverter.ToNullableGuid(request["ID"]);
        var imagePath = server.MapPath("~/App_Themes/Default/Images/transparent.png");
        var fileBytes = File.ReadAllBytes(imagePath);
        var itemType = request["Type"];

        using (var db = DcFactory.Create<PortalDataContext>())
        {
            switch (itemType)
            {
                case "Link":
                    {
                        var item = db.LP_Links.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.Image != null && item.Image.Length > 0)
                            fileBytes = item.Image.ToArray();
                    }
                    break;
                case "Project":
                    {
                        var item = db.LP_Projects.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.Image != null && item.Image.Length > 0)
                            fileBytes = item.Image.ToArray();
                    }
                    break;
                case "Legislation":
                    {
                        var item = db.LP_Legislations.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.Image != null && item.Image.Length > 0)
                            fileBytes = item.Image.ToArray();
                    }
                    break;
                case "Career":
                    {
                        var item = db.LP_Careers.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.Image != null && item.Image.Length > 0)
                            fileBytes = item.Image.ToArray();
                    }
                    break;
                case "Survey":
                    {
                        var item = db.LP_Surveys.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.Image != null && item.Image.Length > 0)
                            fileBytes = item.Image.ToArray();
                    }
                    break;
                case "News":
                    {
                        var item = db.LP_News.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.Image != null && item.Image.Length > 0)
                            fileBytes = item.Image.ToArray();
                    }
                    break;
                case "UserProject":
                    {
                        var item = db.LP_UserReports.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.Image != null && item.Image.Length > 0)
                            fileBytes = item.Image.ToArray();
                    }
                    break;
                case "Category":
                    {
                        var item = db.LP_Categories.FirstOrDefault(n => n.ID == itemID);
                        if (item != null && item.Image != null && item.Image.Length > 0)
                            fileBytes = item.Image.ToArray();
                    }
                    break;
            }
        }

        response.BinaryWrite(fileBytes);
    }
}